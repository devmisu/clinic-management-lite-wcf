using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class ShowSchedules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    gdvSchedules.DataSource = GetSchedulesTable();
                    CommandField editBtn = new CommandField();
                    editBtn.ButtonType = ButtonType.Image;
                    editBtn.SelectImageUrl = "~/assets/icon_edit_16.png";
                    editBtn.ShowSelectButton = true;
                    gdvSchedules.Columns.Add(editBtn);
                    gdvSchedules.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        protected DataTable GetSchedulesTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Hora de Inicio");
            dataTable.Columns.Add("Hora de Salida");
            dataTable.Columns.Add("Dias de atención");
            dataTable.Columns.Add("Fecha de creación");

            try
            {

                ProxySchedule.ServiceScheduleClient proxySchedule = new ProxySchedule.ServiceScheduleClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                List<ProxySchedule.ScheduleBE> array = proxySchedule.GetAllSchedulesOfUserOfAllTime(Convert.ToInt16(idUser)).ToList();
                proxySchedule.Close();

                if (array.Count == 0)
                {
                    
                }

                foreach (ProxySchedule.ScheduleBE schedule in array)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = schedule.StartTime.ToString(@"hh\:mm");
                    row[1] = schedule.EndTime.ToString(@"hh\:mm");
                    row[2] = schedule.Days;
                    row[3] = schedule.CreatedAt.ToString("dd/MM/yyyy");

                    dataTable.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return dataTable;
        }

        protected void gdvSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = gdvSchedules.SelectedRow.RowIndex;
                ProxySchedule.ServiceScheduleClient proxySchedule = new ProxySchedule.ServiceScheduleClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                List<ProxySchedule.ScheduleBE> array = proxySchedule.GetAllSchedulesOfUserOfAllTime(Convert.ToInt16(idUser)).ToList();
                proxySchedule.Close();

                Session["scheduleId"] = array[selectedIndex].Id;
                Response.Redirect("UpdateSchedule.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnCreateSchedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateSchedule.aspx");
        }

    }
}