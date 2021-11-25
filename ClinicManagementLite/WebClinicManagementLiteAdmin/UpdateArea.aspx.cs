using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
	public partial class UpdateArea : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["areaId"] != null)
            {
                try
                {
                    viewError.Visible = false;

                    Int16 areaId = Convert.ToInt16(Session["areaId"].ToString());
                    ProxyArea.ServiceAreaClient proxyArea = new ProxyArea.ServiceAreaClient();
                    ProxyArea.AreaBE areaBE = proxyArea.GetOneArea(areaId);
                    proxyArea.Close();
                    txtAreaName.Text = areaBE.Name;
                    txtAreaDescription.Text = areaBE.Description;

                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    viewSuccess.Visible = false;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void btnUpdateArea_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateArea.Enabled = false;

                if (txtAreaName.Text == String.Empty)
                {
                    throw new Exception("Debe asignar un nombre a la nueva área.");
                }

                Int16 areaId = Convert.ToInt16(Session["areaId"].ToString());
                ProxyArea.ServiceAreaClient proxyArea = new ProxyArea.ServiceAreaClient();
                ProxyArea.AreaBE areaBE = proxyArea.GetOneArea(areaId);
                areaBE.Name = txtAreaName.Text; ;
                areaBE.Description = txtAreaDescription.Text;
                proxyArea.UpdateArea(areaBE);
                proxyArea.Close();

                lblSuccessMessage.Text = "Área actualizada!";
                viewError.Visible = false;
                viewSuccess.Visible = true;
            }
            catch (Exception ex)
            {
                btnUpdateArea.Enabled = true;
                viewError.Visible = true;
                viewSuccess.Visible = false;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateArea.Enabled = false;

                Int16 areaId = Convert.ToInt16(Session["areaId"].ToString());

                ProxyArea.ServiceAreaClient proxy = new ProxyArea.ServiceAreaClient();

                if (proxy.DeleteArea(areaId))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Se elimino el area!";
                }

                proxy.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnUpdateArea.Enabled = true;
            }
        }

    }
}