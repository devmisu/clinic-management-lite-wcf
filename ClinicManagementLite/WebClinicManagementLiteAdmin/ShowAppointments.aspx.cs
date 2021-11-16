using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxyAppointment;
using WebClinicManagementLiteAdmin.ProxyUser;

namespace WebClinicManagementLiteAdmin
{
    public partial class ShowAppointments : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setupDoctorName();
                    gdvAppointments.DataSource = GetAppointmentsTable();
                    CommandField editBtn = new CommandField();
                    editBtn.ButtonType = ButtonType.Image;
                    editBtn.SelectImageUrl = "~/assets/icon_edit_16.png";
                    editBtn.ShowSelectButton = true;
                    gdvAppointments.Columns.Add(editBtn);
                    gdvAppointments.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                    divContainerNoAppointments.Visible = true;
                    gdvAppointments.Visible = false;
                }
            }
        }

        private DataTable GetAppointmentsTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Fecha");
            dataTable.Columns.Add("Hora de inicio");
            dataTable.Columns.Add("Hora fin");
            dataTable.Columns.Add("Hora de llegada del paciente");
            dataTable.Columns.Add("Hora de salida del paciente");
            dataTable.Columns.Add("Paciente");
            dataTable.Columns.Add("Estado de la cita");

            try
            {
                ServiceAppointmentClient proxyAppoinment = new ServiceAppointmentClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                List<AppointmentBE> arrayAppointments = proxyAppoinment.GetUserAppointments(Convert.ToInt16(idUser)).ToList();
                proxyAppoinment.Close();

                if (arrayAppointments.Count == 0)
                {
                    divContainerNoAppointments.Visible = true;
                }

                foreach (AppointmentBE appointment in arrayAppointments)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = appointment.Date.ToString("dd/MM/yyyy");
                    row[1] = appointment.StartHour.ToString(@"hh\:mm");
                    row[2] = appointment.EndHour.ToString(@"hh\:mm");
                    row[3] = appointment.ArrivalHour == null ? "" : appointment.ArrivalHour?.ToString(@"hh\:mm");
                    row[4] = appointment.DepartureHour == null ? "" : appointment.DepartureHour?.ToString(@"hh\:mm");
                    row[5] = appointment.Patient.LastName + " " + appointment.Patient.FirstName;
                    var name = (appointment.State == "1") ? "Activa" : "Finalizada";
                    row[6] = name;

                    dataTable.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return dataTable;
        }

        protected void gdvAppointments_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = gdvAppointments.SelectedRow.RowIndex;
                ServiceAppointmentClient proxyAppoinment = new ServiceAppointmentClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                List<AppointmentBE> arrayAppointments = proxyAppoinment.GetUserAppointments(Convert.ToInt16(idUser)).ToList();
                proxyAppoinment.Close();

                Session["appointmentId"] = arrayAppointments[selectedIndex].Id;
                Response.Redirect("UpdateAppointment.aspx");
            } 
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnCreateAppointment_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAppointment.aspx");
        }

        protected void setupDoctorName()
        {
            try
            {
                Int16 doctorId = Convert.ToInt16(User.Identity.Name);

                ServiceUserClient proxyArea = new ServiceUserClient();
                ProxyUser.UserBE userBE = proxyArea.GetUser(doctorId);
                proxyArea.Close();

                //Set Doctor name
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Value");
                DataRow dr = dt.NewRow();
                dr[0] = userBE.LastName + " " + userBE.FirstName;
                dr[1] = userBE.Id;
                dt.Rows.Add(dr);
                ddlDoctor.DataTextField = "Name";
                ddlDoctor.DataValueField = "Value";
                ddlDoctor.DataSource = dt;
                ddlDoctor.DataBind();
                ddlDoctor.SelectedIndex = 0;
                ddlDoctor.Enabled = false;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                throw ex;
            }
        }
    }
}