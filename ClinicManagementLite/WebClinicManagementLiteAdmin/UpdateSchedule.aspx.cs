using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class UpdateSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["scheduleId"] != null)
            {
                Int16 scheduleId = Convert.ToInt16(Session["scheduleId"].ToString());

                ProxySchedule.ServiceScheduleClient proxySchedule = new ProxySchedule.ServiceScheduleClient();
                ProxySchedule.ScheduleBE scheduleBE = proxySchedule.GetOneSchedule(scheduleId);

                setupStartTimeSelector();
                setupEndTimeSelector();
                validateCheck(scheduleBE.Days);

                ddlStartTimeCurrent.Text = scheduleBE.StartTime.ToString(@"hh\:mm");
                ddlEndTimeCurrent.Text = scheduleBE.EndTime.ToString(@"hh\:mm");
            }
        }

        protected void setupStartTimeSelector()
        {
            try
            {
                viewError.Visible = false;

                List<String> hours = new List<string>();
                hours.Add("06:00:00");
                hours.Add("07:00:00");
                hours.Add("08:00:00");
                hours.Add("09:00:00");
                hours.Add("10:00:00");
                hours.Add("11:00:00");
                hours.Add("12:00:00");
                hours.Add("13:00:00");
                hours.Add("14:00:00");
                hours.Add("15:00:00");
                hours.Add("16:00:00");
                hours.Add("17:00:00");
                hours.Add("18:00:00");

                DataTable dt = new DataTable();

                dt.Columns.Add("Name");
                dt.Columns.Add("Value");

                DataRow placeholder = dt.NewRow();

                placeholder[0] = "Seleccione horario";
                placeholder[1] = 0;

                dt.Rows.Add(placeholder);

                foreach (String hour in hours)
                {
                    DataRow dr = dt.NewRow();

                    dr[0] = hour;
                    dr[1] = TimeSpan.Parse(hour);

                    dt.Rows.Add(dr);
                }

                ddlStartTime.DataTextField = "Name";
                ddlStartTime.DataValueField = "Value";

                ddlStartTime.DataSource = dt;
                ddlStartTime.DataBind();
                ddlStartTime.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void setupEndTimeSelector()
        {
            try
            {
                viewError.Visible = false;

                List<String> hours = new List<string>();
                hours.Add("07:00:00");
                hours.Add("08:00:00");
                hours.Add("09:00:00");
                hours.Add("10:00:00");
                hours.Add("11:00:00");
                hours.Add("12:00:00");
                hours.Add("13:00:00");
                hours.Add("14:00:00");
                hours.Add("15:00:00");
                hours.Add("16:00:00");
                hours.Add("17:00:00");
                hours.Add("18:00:00");

                DataTable dt = new DataTable();

                dt.Columns.Add("Name");
                dt.Columns.Add("Value");

                DataRow placeholder = dt.NewRow();

                placeholder[0] = "Seleccione horario";
                placeholder[1] = 0;

                dt.Rows.Add(placeholder);

                foreach (String hour in hours)
                {
                    DataRow dr = dt.NewRow();

                    dr[0] = hour;
                    dr[1] = TimeSpan.Parse(hour);

                    dt.Rows.Add(dr);
                }

                ddlEndTime.DataTextField = "Name";
                ddlEndTime.DataValueField = "Value";

                ddlEndTime.DataSource = dt;
                ddlEndTime.DataBind();
                ddlEndTime.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected String getDaysSelected()
        {
            String daysConcatList = "";

            if (cbxMon.Checked)
            {
                daysConcatList += Util.Constants.MONDAY + ",";
            }
            if (cbxTue.Checked)
            {
                daysConcatList += Util.Constants.TUESDAY + ",";
            }
            if (cbxWed.Checked)
            {
                daysConcatList += Util.Constants.WEDNESDAY + ",";
            }
            if (cbxThu.Checked)
            {
                daysConcatList += Util.Constants.THURSDAY + ",";
            }
            if (cbxFri.Checked)
            {
                daysConcatList += Util.Constants.FRIDAY + ",";
            }
            if (cbxSat.Checked)
            {
                daysConcatList += Util.Constants.SATURDAY + ",";
            }
            if (cbxSun.Checked)
            {
                daysConcatList += Util.Constants.SUNDAY + ",";
            }
            if (daysConcatList == String.Empty)
            {
                throw new Exception("Debe seleccionar al menos un día.");
            }

            return daysConcatList.Remove(daysConcatList.Length - 1);
        }

        protected void btnCreateSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                btnCreateSchedule.Enabled = false;

                if (ddlStartTime.SelectedItem.Value != "0")
                {
                    if (ddlEndTime.SelectedItem.Value == "0")
                    {
                        throw new Exception("Debes seleccionar una hora de salida.");
                    }
                }

                if (ddlEndTime.SelectedItem.Value != "0")
                {
                    if (ddlStartTime.SelectedItem.Value == "0")
                    {
                        throw new Exception("Debes seleccionar una hora de inicio.");
                    }
                }

                if (ddlStartTime.SelectedItem.Value != "0" && ddlEndTime.SelectedItem.Value != "0")
                {
                    if (ddlEndTime.SelectedIndex < ddlStartTime.SelectedIndex)
                    {
                        throw new Exception("La hora de salida no puede ser menor o igual a la de inicio.");
                    }
                }


                String days = getDaysSelected();

                Int16 scheduleId = Convert.ToInt16(Session["scheduleId"].ToString());

                ProxySchedule.ServiceScheduleClient proxySchedule = new ProxySchedule.ServiceScheduleClient();
                ProxySchedule.ScheduleBE scheduleBE = proxySchedule.GetOneSchedule(scheduleId);

                if (ddlStartTime.SelectedItem.Value != "0" && ddlEndTime.SelectedItem.Value != "0")
                {
                    scheduleBE.StartTime = TimeSpan.Parse(ddlStartTime.SelectedValue);
                    scheduleBE.EndTime = TimeSpan.Parse(ddlEndTime.SelectedValue);
                }

                scheduleBE.Days = days;
                proxySchedule.UpdateSchedule(scheduleBE);
                proxySchedule.Close();

                viewSuccess.Visible = true;
                lblErrorMessage.Text = "Horario actualizado exitosamente!";
                viewError.Visible = false;
            }
            catch (Exception ex)
            {
                btnCreateSchedule.Enabled = true;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void validateCheck(String list)
        {
            cbxMon.Checked = list.Contains(Util.Constants.MONDAY);
            cbxTue.Checked = list.Contains(Util.Constants.TUESDAY);
            cbxWed.Checked = list.Contains(Util.Constants.WEDNESDAY);
            cbxThu.Checked = list.Contains(Util.Constants.THURSDAY);
            cbxFri.Checked = list.Contains(Util.Constants.FRIDAY);
            cbxSat.Checked = list.Contains(Util.Constants.SATURDAY);
            cbxSun.Checked = list.Contains(Util.Constants.SUNDAY);
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                btnCreateSchedule.Enabled = false;

                Int16 scheduleId = Convert.ToInt16(Session["scheduleId"].ToString());

                ProxySchedule.ServiceScheduleClient proxySchedule = new ProxySchedule.ServiceScheduleClient();

                if (proxySchedule.DeleteSchedule(scheduleId))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Se elimino el horario!";
                }

                proxySchedule.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnCreateSchedule.Enabled = true;
            }
        }

    }
}