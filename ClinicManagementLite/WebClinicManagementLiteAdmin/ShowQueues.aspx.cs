using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxyUser;
using System.Data;
using WebClinicManagementLiteAdmin.ProxyQueue;

namespace WebClinicManagementLiteAdmin
{
    public partial class ShowQueues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setupDoctorName();
                    gdvQueues.DataSource = GetQueuesTable();
                    CommandField editBtn = new CommandField();
                    editBtn.ButtonType = ButtonType.Image;
                    editBtn.SelectImageUrl = "~/assets/icon_edit_16.png";
                    editBtn.ShowSelectButton = true;
                    gdvQueues.Columns.Add(editBtn);
                    gdvQueues.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                    divContainerNoQueues.Visible = true;
                    gdvQueues.Visible = false;
                }
            }
        }

        protected void gdvQueues_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = gdvQueues.SelectedRow.RowIndex;
                ServiceQueueClient proxyQueue = new ServiceQueueClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                List<QueueBE> arrayQueues = proxyQueue.GetUserQueues(Convert.ToInt16(idUser)).ToList();
                proxyQueue.Close();

                Session["queueId"] = arrayQueues[selectedIndex].Id;
                Response.Redirect("UpdateQueue.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
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

        private DataTable GetQueuesTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Especialidad");
            dataTable.Columns.Add("Paciente");
            dataTable.Columns.Add("Fecha");
            dataTable.Columns.Add("Hora");
            dataTable.Columns.Add("Estado");

            try
            {
                ServiceQueueClient proxyQueue = new ServiceQueueClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                List<QueueBE> arrayQueues = proxyQueue.GetUserQueues(Convert.ToInt16(idUser)).ToList();
                proxyQueue.Close();

                if (arrayQueues.Count == 0)
                {
                    divContainerNoQueues.Visible = true;
                }

                foreach (QueueBE queue in arrayQueues)
                {
                    var state = "";

                    switch (queue.State)
                    {
                        case "1":
                            state = "Solicitado";
                            break;
                        case "2":
                            state = "Aceptado";
                            break;
                        case "3":
                            state = "Cancelado";
                            break;
                        case "4":
                            state = "Rechazado";
                            break;
                    }

                    DataRow row = dataTable.NewRow();
                    row[0] = queue.User.Area.Name;
                    row[1] = queue.Patient.FirstName + " " + queue.Patient.LastName;
                    row[2] = queue.StartDate.ToString("dd/MM/yyyy");
                    row[3] = queue.StartTime.ToString(@"hh\:mm");
                    row[4] = state;

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