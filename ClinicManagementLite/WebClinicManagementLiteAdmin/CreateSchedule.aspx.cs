using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class CreateSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setupStartTimeSelector();
                    setupEndTimeSelector();
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
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

                if (ddlStartTime.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar una hora de inicio.");
                }

                if (ddlEndTime.SelectedItem.Value == "0")
                {
                    throw new Exception("Debes seleccionar una hora de salida.");
                }

                if (ddlEndTime.SelectedIndex < ddlStartTime.SelectedIndex)
                {
                    throw new Exception("La hora de salida no puede ser menor o igual a la de inicio.");
                }

                String days = getDaysSelected();

                ProxySchedule.ServiceScheduleClient proxySchedule = new ProxySchedule.ServiceScheduleClient();
                ProxySchedule.ScheduleBE scheduleBE = new ProxySchedule.ScheduleBE();
                String idUser = HttpContext.Current.User.Identity.Name;
                scheduleBE.IdUser = Convert.ToInt16(idUser);
                scheduleBE.StartTime = TimeSpan.Parse(ddlStartTime.SelectedValue);
                scheduleBE.EndTime = TimeSpan.Parse(ddlEndTime.SelectedValue);
                scheduleBE.Days = days;
                proxySchedule.CreateSchedule(scheduleBE);
                proxySchedule.Close();

                viewSuccess.Visible = true;
                lblErrorMessage.Text = "Horario creado exitosamente!";
                viewError.Visible = false;
            }
            catch (Exception ex)
            {
                btnCreateSchedule.Enabled = true;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

    }
}