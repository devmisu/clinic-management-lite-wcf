using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxySchedule;
using WebClinicManagementLiteAdmin.ProxyUser;

namespace WebClinicManagementLiteAdmin
{
    public partial class CreateAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    Int16 doctorId = Convert.ToInt16(User.Identity.Name);

                    ServiceUserClient proxyArea = new ServiceUserClient();
                    ProxyUser.UserBE userBE = proxyArea.GetUser(doctorId);
                    proxyArea.Close();

                    setupPatientListSelector();
                    setupDoctorSpecialization(userBE);
                    setupDoctorName(userBE);
                }
                catch(Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void setupDoctorSpecialization(ProxyUser.UserBE userBE)
        {
            try
            {
                viewError.Visible = false;

                //Set Doctor Specialization
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Value");
                DataRow dr = dt.NewRow();
                dr[0] = userBE.Area.Name;
                dr[1] = userBE.Area.Id;
                dt.Rows.Add(dr);
                ddlArea.DataTextField = "Name";
                ddlArea.DataValueField = "Value";
                ddlArea.DataSource = dt;
                ddlArea.DataBind();
                ddlArea.SelectedIndex = 0;
                ddlArea.Enabled = false;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                throw ex;
            }
        }

        protected void setupDoctorName(ProxyUser.UserBE userBE)
        {
            try
            {
                viewError.Visible = false;

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

        protected void setupPatientListSelector()
        {
            ProxyPatient.ServicePatientClient proxyPatient = new ProxyPatient.ServicePatientClient();
            List<ProxyPatient.PatientBE> patientBEs = proxyPatient.GetAllPatients().ToList();
            proxyPatient.Close();

            DataTable dt = new DataTable();

            dt.Columns.Add("Name");
            dt.Columns.Add("Value");

            DataRow placeholder = dt.NewRow();

            placeholder[0] = "Seleccione paciente";
            placeholder[1] = 0;

            dt.Rows.Add(placeholder);

            foreach (ProxyPatient.PatientBE scheduleBE in patientBEs)
            {
                DataRow dr = dt.NewRow();

                dr[0] = scheduleBE.LastName + " " + scheduleBE.FirstName;
                dr[1] = scheduleBE.Id;

                dt.Rows.Add(dr);
            }

            ddlPatient.DataTextField = "Name";
            ddlPatient.DataValueField = "Value";

            ddlPatient.DataSource = dt;
            ddlPatient.DataBind();
            ddlPatient.SelectedIndex = 0;
        }

        protected void setupScheduleSelector(Int16 doctorId, DateTime date)
        {
            try
            {
                viewError.Visible = false;

                ServiceScheduleClient proxySchedule = new ServiceScheduleClient();
                List<ScheduleBE> scheduleBEs = proxySchedule.GetAvailableSchedulesByUser(doctorId, date).ToList();
                proxySchedule.Close();

                DataTable dt = new DataTable();

                dt.Columns.Add("Name");
                dt.Columns.Add("Value");

                DataRow placeholder = dt.NewRow();

                placeholder[0] = "Seleccione horario";
                placeholder[1] = 0;

                dt.Rows.Add(placeholder);

                foreach (ScheduleBE scheduleBE in scheduleBEs)
                {
                    DataRow dr = dt.NewRow();

                    dr[0] = scheduleBE.StartTime.ToString(@"hh\:mm") + " - " + scheduleBE.EndTime.ToString(@"hh\:mm");
                    dr[1] = scheduleBE.StartTime;

                    dt.Rows.Add(dr);
                }

                ddlSchedule.DataTextField = "Name";
                ddlSchedule.DataValueField = "Value";

                ddlSchedule.DataSource = dt;
                ddlSchedule.DataBind();
                ddlSchedule.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnShowAvailableSchedules_Click(object sender, EventArgs e)
        {
            try
            {
                viewError.Visible = false;

                if (ddlArea.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar una especialidad.");
                }

                if (ddlDoctor.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar un doctor.");
                }

                if (txtDate.Text.Trim().Length == 0)
                {
                    throw new Exception("Debes ingresar una fecha valida.");
                }

                Int16 doctorId = Convert.ToInt16(ddlDoctor.SelectedItem.Value); ;
                DateTime date = Convert.ToDateTime(txtDate.Text);

                setupScheduleSelector(doctorId, date);
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void btnScheduleAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                viewError.Visible = false;
                btnScheduleAppointment.Enabled = false;

                if (ddlArea.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar una especialidad.");
                }

                if (ddlDoctor.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar un doctor.");
                }

                if (txtDate.Text.Trim().Length == 0)
                {
                    throw new Exception("Debes ingresar una fecha.");
                }

                if (ddlSchedule.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar un horario.");
                }

                if (ddlPatient.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar un paciente.");
                }

                Int16 patientId = Convert.ToInt16(ddlPatient.SelectedItem.Value); ;
                DateTime date = Convert.ToDateTime(txtDate.Text);
                TimeSpan startTime = TimeSpan.Parse(ddlSchedule.SelectedItem.Value);

                if (createAppointment(patientId, date, startTime))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Cita agendada!";
                }
            }
            catch (Exception ex)
            {
                btnScheduleAppointment.Enabled = true;
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected bool createAppointment(Int16 patientId, DateTime date, TimeSpan startHour)
        {
            try
            {
                ProxyAppointment.AppointmentBE appointmentBE = new ProxyAppointment.AppointmentBE();
                appointmentBE.IdPatient = patientId;
                appointmentBE.IdUser = Convert.ToInt16(User.Identity.Name);
                appointmentBE.Date = date;
                appointmentBE.StartHour = startHour;

                ProxyAppointment.ServiceAppointmentClient proxyAppointment = new ProxyAppointment.ServiceAppointmentClient();
                bool result = proxyAppointment.CreateAppointment(appointmentBE);
                proxyAppointment.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}