using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class ShowRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    gdvRoles.DataSource = GetRolesTable();
                    CommandField editBtn = new CommandField();
                    editBtn.ButtonType = ButtonType.Image;
                    editBtn.SelectImageUrl = "~/assets/icon_edit_16.png";
                    editBtn.ShowSelectButton = true;
                    gdvRoles.Columns.Add(editBtn);
                    gdvRoles.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        private DataTable GetRolesTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Nombre");
            dataTable.Columns.Add("Atributos");
            dataTable.Columns.Add("Fecha de creación");

            try
            {
                ProxyRole.ServiceRoleClient proxyRole = new ProxyRole.ServiceRoleClient();
                List<ProxyRole.RoleBE> arrayRoles = proxyRole.GetAllRoles().ToList();
                proxyRole.Close();

                foreach (ProxyRole.RoleBE role in arrayRoles)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = role.Name;
                    row[1] = formatAttributes(role.Attributes);
                    row[2] = role.CreatedAt.ToString("dd/MM/yyyy");

                    dataTable.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return dataTable;
        }

        protected String formatAttributes(String list)
        {
            try
            {
                String formatedAttributes = "";

                if (list.Contains(Util.Constants.All))
                {
                    formatedAttributes += Util.Constants.FormatAll + ", ";
                }
                if (list.Contains(Util.Constants.Schedule))
                {
                    formatedAttributes += Util.Constants.FormatSchedule + ", ";
                }
                if (list.Contains(Util.Constants.Appointment))
                {
                    formatedAttributes += Util.Constants.FormatAppointment + ", ";
                }
                if (list.Contains(Util.Constants.Queue))
                {
                    formatedAttributes += Util.Constants.FormatQueue + ", ";
                }
                if (list.Contains(Util.Constants.Doctors))
                {
                    formatedAttributes += Util.Constants.FormatDoctors + ", ";
                }
                if (list.Contains(Util.Constants.Rol))
                {
                    formatedAttributes += Util.Constants.FormatRol + ", ";
                }
                if (list.Contains(Util.Constants.Patient))
                {
                    formatedAttributes += Util.Constants.FormatPatient + ", ";
                }
                if (list.Contains(Util.Constants.Area))
                {
                    formatedAttributes += Util.Constants.FormatArea + ", ";
                }

                String finalRoles = "";

                if (formatedAttributes != String.Empty)
                {
                     finalRoles = formatedAttributes.Remove(formatedAttributes.Length - 2);
                }

                return finalRoles;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gdvRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = gdvRoles.SelectedRow.RowIndex;
                ProxyRole.ServiceRoleClient proxyRole = new ProxyRole.ServiceRoleClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                List<ProxyRole.RoleBE> arrayRoles = proxyRole.GetAllRoles().ToList();
                proxyRole.Close();

                Session["roleId"] = arrayRoles[selectedIndex].Id;
                Response.Redirect("UpdateRol.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnCreateRol_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateRol.aspx");
        }
    }
}