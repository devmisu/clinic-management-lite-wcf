using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPatient.ProxyArea;
using WebPatient.ProxyUser;
using WebPatient.ProxySchedule;
using WebPatient.ProxyAppointment;

namespace WebPatient
{
    public partial class ScheduleAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    setupAreaSelector();
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int16 areaId = Convert.ToInt16(ddlArea.SelectedItem.Value);
                setupDoctorSelector(areaId);
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
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

                Int16 doctorId = Convert.ToInt16(ddlDoctor.SelectedItem.Value); ;
                DateTime date = Convert.ToDateTime(txtDate.Text);
                TimeSpan startTime = TimeSpan.Parse(ddlSchedule.SelectedItem.Value);

                if (createAppointment(doctorId, date, startTime))
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

        // Methods

        protected void setupAreaSelector()
        {
            try
            {
                viewError.Visible = false;

                ServiceAreaClient proxyArea = new ServiceAreaClient();
                List<ProxyArea.AreaBE> areaBEs = proxyArea.GetAllAreas().ToList();
                proxyArea.Close();

                DataTable dt = new DataTable();

                dt.Columns.Add("Name");
                dt.Columns.Add("Value");

                DataRow placeholder = dt.NewRow();

                placeholder[0] = "Seleccione especialidad";
                placeholder[1] = 0;

                dt.Rows.Add(placeholder);

                foreach (ProxyArea.AreaBE areaBE in areaBEs)
                {
                    DataRow dr = dt.NewRow();

                    dr[0] = areaBE.Name;
                    dr[1] = areaBE.Id;

                    dt.Rows.Add(dr);
                }

                ddlArea.DataTextField = "Name";
                ddlArea.DataValueField = "Value";
                
                ddlArea.DataSource = dt;
                ddlArea.DataBind();
                ddlArea.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void setupDoctorSelector(Int16 areaId)
        {
            try
            {
                viewError.Visible = false;

                ServiceUserClient proxyArea = new ServiceUserClient();
                List<ProxyUser.UserBE> userBEs = proxyArea.GetUsersByArea(areaId).ToList();
                proxyArea.Close();

                DataTable dt = new DataTable();

                dt.Columns.Add("Name");
                dt.Columns.Add("Value");

                DataRow placeholder = dt.NewRow();

                placeholder[0] = "Seleccione doctor";
                placeholder[1] = 0;

                dt.Rows.Add(placeholder);

                foreach (ProxyUser.UserBE userBE in userBEs)
                {
                    DataRow dr = dt.NewRow();

                    dr[0] = userBE.FirstName + " " + userBE.LastName;
                    dr[1] = userBE.Id;

                    dt.Rows.Add(dr);
                }

                ddlDoctor.DataTextField = "Name";
                ddlDoctor.DataValueField = "Value";

                ddlDoctor.DataSource = dt;
                ddlDoctor.DataBind();
                ddlDoctor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        protected bool createAppointment(Int16 doctorId, DateTime date, TimeSpan startHour)
        {
            try
            {
                AppointmentBE appointmentBE = new AppointmentBE();
                appointmentBE.IdPatient = Convert.ToInt16(User.Identity.Name);
                appointmentBE.IdUser = doctorId;
                appointmentBE.Date = date;
                appointmentBE.StartHour = startHour;

                ServiceAppointmentClient proxyAppointment = new ServiceAppointmentClient();
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