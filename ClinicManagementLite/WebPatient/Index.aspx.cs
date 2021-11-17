using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Data;
using WebPatient.ProxyPatient;
using WebPatient.ProxyAppointment;
using WebPatient.ProxyMedicalRecords;

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

                    ProxyPatient.ServicePatientClient proxyPatient = new ProxyPatient.ServicePatientClient();
                    ProxyPatient.PatientBE patientBE = proxyPatient.GetPatient(patientId);
                    proxyPatient.Close();

                    lblWelcome.Text = "Hola! " + patientBE.FirstName;

                    ServiceAppointmentClient proxyAppointment = new ServiceAppointmentClient();
                    List<ProxyAppointment.AppointmentBE> appointmentBEs = proxyAppointment.GetPatientAppointments(patientId).ToList();
                    proxyAppointment.Close();

                    CommandField cField = new CommandField();

                    cField.ButtonType = ButtonType.Image;
                    cField.ShowSelectButton = true;
                    cField.SelectImageUrl = "~/www/img/icon_view_16.png";
                    cField.SelectText = "Ver";

                    gridView.Columns.Add(cField);

                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("Id");
                    dataTable.Columns.Add("Especialidad");
                    dataTable.Columns.Add("Doctor");
                    dataTable.Columns.Add("Fecha");
                    dataTable.Columns.Add("Hora");
                    dataTable.Columns.Add("Estado");
                    dataTable.Columns.Add("Fecha de creacion");

                    foreach (ProxyAppointment.AppointmentBE appointmentBE in appointmentBEs)
                    {
                        DataRow row = dataTable.NewRow();

                        row[0] = appointmentBE.Id.ToString();
                        row[1] = appointmentBE.User.Area.Name;
                        row[2] = appointmentBE.User.FirstName + " " + appointmentBE.User.LastName;
                        row[3] = appointmentBE.Date.ToString("dd/MM/yyyy");
                        row[4] = appointmentBE.StartHour.ToString(@"hh\:mm") + " - " + appointmentBE.EndHour.ToString(@"hh\:mm");
                        row[5] = appointmentBE.State == "1" ? "Pendiente" : "Finalizado";
                        row[6] = appointmentBE.CreatedAt.ToString("dd/MM/yyyy");

                        dataTable.Rows.Add(row);
                    }

                    gridView.DataSource = dataTable;
                    gridView.DataBind();
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
            GridViewRow row = gridView.SelectedRow;
            String appointmentId = row.Cells[1].Text;

            Session["appointmentId"] = appointmentId;
            Response.Redirect("Appointment.aspx");
        }

        protected void btnCreateAppointment_Click(object sender, EventArgs e)
        {
            Response.Redirect("ScheduleAppointment.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnQueue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Queues.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void btnMedicalRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("MedicalRecords.aspx");
        }

        protected void btnQueueAppointment_Click(object sender, EventArgs e)
        {
            Response.Redirect("QueueAppointment.aspx");
        }
    }
}