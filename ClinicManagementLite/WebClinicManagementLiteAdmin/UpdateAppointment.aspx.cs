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
                Int16 appointmentId = Convert.ToInt16(Session["appointmentId"].ToString());

                ProxyAppointment.ServiceAppointmentClient proxyAppointment = new ProxyAppointment.ServiceAppointmentClient();
                ProxyAppointment.AppointmentBE appointmentBE = proxyAppointment.GetOneAppointment(appointmentId);
                proxyAppointment.Close();

                txtArea.Text = appointmentBE.User.Area.Name;
                txtState.Text = appointmentBE.State == "1" ? "Pendiente" : "Finalizado";
                txtDate.Text = appointmentBE.Date.ToString("dd/MM/yyyy");
                txtStartHour.Text = appointmentBE.StartHour.ToString(@"hh\:mm");
                txtEndHour.Text = appointmentBE.EndHour.ToString(@"hh\:mm");
                txtDoctor.Text = appointmentBE.User.FirstName + " " + appointmentBE.User.LastName;
                txtDoctorDni.Text = appointmentBE.User.Dni;
                txtDoctorPhone.Text = appointmentBE.User.Phone;
                txtDoctorEmail.Text = appointmentBE.User.Email;
            }
        }

        protected void btnDeleteAppointment_Click(object sender, EventArgs e)
        {

        }
    }
}