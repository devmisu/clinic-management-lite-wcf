using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using WebPatient.ProxyPatient;
using System.Collections;
using System.Data;
using WebPatient.ProxyAppointment;

namespace WebPatient
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    Int16 patientId = Convert.ToInt16(User.Identity.Name);

                    updateWelcomeText(patientId);
                    fetchAppointments(patientId);

                    //String option = Request.QueryString["option"].ToString();
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int16 appointmentId = Convert.ToInt16(gridView.SelectedRow.Cells[0].Text);
            // TODO: Redirect to appointment detail
        }

        protected void updateWelcomeText(Int16 patientId)
        {
            try
            {
                ServicePatientClient proxyPatient = new ServicePatientClient();
                PatientBE patientBE = proxyPatient.GetPatient(patientId);
                proxyPatient.Close();

                lblWelcome.Text = "Bienvenido " + patientBE.FirstName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void fetchAppointments(Int16 patientId)
        {
            try
            {
                ServiceAppointmentClient proxyAppointment = new ServiceAppointmentClient();
                List<AppointmentBE> appointmentBEs = proxyAppointment.GetPatientAppointments(patientId).ToList();
                proxyAppointment.Close();

                gridView.DataSource = appointmentBEs;
                gridView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}