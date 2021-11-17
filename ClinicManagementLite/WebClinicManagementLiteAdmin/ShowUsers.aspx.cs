using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxyUser;
using System.Data;

namespace WebClinicManagementLiteAdmin
{
    public partial class ShowUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setupDoctorName();
                    gdvUsers.DataSource = GetUsersTable();
                    CommandField editBtn = new CommandField();
                    editBtn.ButtonType = ButtonType.Image;
                    editBtn.SelectImageUrl = "~/assets/icon_edit_16.png";
                    editBtn.ShowSelectButton = true;
                    gdvUsers.Columns.Add(editBtn);
                    gdvUsers.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                    divContainerNoQueues.Visible = true;
                    gdvUsers.Visible = false;
                }
            }
        }

        protected void gdvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = gdvUsers.SelectedRow.RowIndex;
                ServiceUserClient proxyUser = new ServiceUserClient();
                List<UserBE> arrayUsers = proxyUser.GetAllUsers().ToList();
                proxyUser.Close();

                Session["userId"] = arrayUsers[selectedIndex].Id;
                Response.Redirect("UpdateUser.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateUser.aspx");
        }

        protected void setupDoctorName()
        {
            try
            {
                Int16 doctorId = Convert.ToInt16(User.Identity.Name);

                ServiceUserClient proxyArea = new ServiceUserClient();
                UserBE userBE = proxyArea.GetUser(doctorId);
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

        private DataTable GetUsersTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Dni");
            dataTable.Columns.Add("Nombres");
            dataTable.Columns.Add("Apellidos");
            dataTable.Columns.Add("Especialidad");
            dataTable.Columns.Add("Rol");
            dataTable.Columns.Add("Fecha de creacion");

            try
            {
                ServiceUserClient proxyUser = new ServiceUserClient();
                List<UserBE> arrayUsers = proxyUser.GetAllUsers().ToList();
                proxyUser.Close();

                if (arrayUsers.Count == 0)
                {
                    divContainerNoQueues.Visible = true;
                }

                foreach (UserBE user in arrayUsers)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = user.Dni;
                    row[1] = user.FirstName;
                    row[2] = user.LastName;
                    row[3] = user.Area.Name;
                    row[4] = user.Role.Name;
                    row[5] = user.CreatedAt.ToString("dd/MM/yyyy");

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