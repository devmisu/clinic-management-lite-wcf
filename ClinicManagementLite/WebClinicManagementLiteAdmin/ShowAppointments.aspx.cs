using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxyAppointment;

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

                foreach (AppointmentBE appointment in arrayAppointments)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = appointment.Date.ToString("dd/MM/yyyy");
                    row[1] = appointment.StartHour.ToString();
                    row[2] = appointment.EndHour.ToString();
                    row[3] = appointment.ArrivalHour.ToString();
                    row[4] = appointment.DepartureHour.ToString();
                    row[5] = appointment.User.FirstName;
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
                //System.Diagnostics.Debug.WriteLine(arrayAppointments[selectedIndex].StartHour);
            } 
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}