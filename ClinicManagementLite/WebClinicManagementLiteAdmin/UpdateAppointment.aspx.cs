using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class UpdateAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["appointmentId"] != null) {
                try
                {
                    Int16 appointmentId = Convert.ToInt16(Session["appointmentId"].ToString());

                    ProxyAppointment.ServiceAppointmentClient proxyAppointment = new ProxyAppointment.ServiceAppointmentClient();
                    ProxyAppointment.AppointmentBE appointmentBE = proxyAppointment.GetOneAppointment(appointmentId);
                    proxyAppointment.Close();

                    txtArea.Text = appointmentBE.User.Area.Name;
                    txtState.Text = appointmentBE.State == "1" ? "Pendiente" : "Finalizado";
                    btnDeleteAppointment.Enabled = appointmentBE.State == "1";
                    btnFinishAppointment.Enabled = appointmentBE.State == "1";
                    txtDate.Text = appointmentBE.Date.ToString("dd/MM/yyyy");
                    txtStartHour.Text = appointmentBE.StartHour.ToString(@"hh\:mm");
                    txtEndHour.Text = appointmentBE.EndHour.ToString(@"hh\:mm");
                    txtDoctor.Text = appointmentBE.User.FirstName + " " + appointmentBE.User.LastName;
                    txtDoctorDni.Text = appointmentBE.User.Dni;
                    txtDoctorPhone.Text = appointmentBE.User.Phone;
                    txtDoctorEmail.Text = appointmentBE.User.Email;
                } 
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        protected void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 appointmentId = Convert.ToInt16(Session["appointmentId"].ToString());
                ProxyAppointment.ServiceAppointmentClient proxyAppointment = new ProxyAppointment.ServiceAppointmentClient();
                proxyAppointment.DeleteAppointment(appointmentId);
                proxyAppointment.Close();
                viewSuccess.Visible = true;
                lblSuccessMessage.Text = "Cita eliminada correctamente!";
                divContainerInfo.Visible = false;
            } 
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void btnFinishAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 appointmentId = Convert.ToInt16(Session["appointmentId"].ToString());
                Session["appointmentId"] = appointmentId;
                Response.Redirect("FinishAppointment.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

    }
}