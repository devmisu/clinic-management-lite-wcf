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
                    row[0] = appointment.Date.ToString();
                    row[1] = appointment.StartHour;
                    row[2] = appointment.EndHour;
                    row[3] = appointment.ArrivalHour;
                    row[4] = appointment.DepartureHour;
                    row[5] = appointment.IdPatient;
                    row[6] = appointment.State;

                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return dataTable;
        }
    }
}