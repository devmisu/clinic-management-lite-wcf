using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class ShowAreas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    gdvAreas.DataSource = GetAreasTable();
                    CommandField editBtn = new CommandField();
                    editBtn.ButtonType = ButtonType.Image;
                    editBtn.SelectImageUrl = "~/assets/icon_edit_16.png";
                    editBtn.ShowSelectButton = true;
                    gdvAreas.Columns.Add(editBtn);
                    gdvAreas.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        private DataTable GetAreasTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Nombre");
            dataTable.Columns.Add("Descripción");
            dataTable.Columns.Add("Fecha de creación");

            try
            {
                ProxyArea.ServiceAreaClient proxyArea = new ProxyArea.ServiceAreaClient();
                List<ProxyArea.AreaBE> arrayAreas = proxyArea.GetAllAreas().ToList();
                proxyArea.Close();

                foreach (ProxyArea.AreaBE area in arrayAreas)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = area.Name;
                    row[1] = area.Description;
                    row[2] = area.CreatedAt.ToString("dd/MM/yyyy");

                    dataTable.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return dataTable;
        }

        protected void gdvAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = gdvAreas.SelectedRow.RowIndex;
                ProxyArea.ServiceAreaClient proxyArea = new ProxyArea.ServiceAreaClient();
                List<ProxyArea.AreaBE> arrayAreas = proxyArea.GetAllAreas().ToList();
                proxyArea.Close();

                Session["areaId"] = arrayAreas[selectedIndex].Id;
                Response.Redirect("UpdateArea.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnCreateArea_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateArea.aspx");
        }
    }
}