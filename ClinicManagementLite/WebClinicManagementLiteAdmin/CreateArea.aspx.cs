using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class CreateArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    viewError.Visible = false;
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void btnCreateArea_Click(object sender, EventArgs e)
        {
            try
            {
                btnCreateArea.Enabled = false;

                if (txtAreaName.Text == String.Empty)
                {
                    throw new Exception("Debe ingresar un nombre para el area.");
                }

                ProxyArea.ServiceAreaClient proxyArea = new ProxyArea.ServiceAreaClient();
                ProxyArea.AreaBE areaBE = new ProxyArea.AreaBE();
                areaBE.Name = txtAreaName.Text;
                areaBE.Description = txtAreaDescription.Text;
                proxyArea.CreateArea(areaBE);
                proxyArea.Close();

                viewSuccess.Visible = true;
                lblSuccessMessage.Text = "Area creada exitosamente!";
                viewError.Visible = false;
            }
            catch (Exception ex)
            {
                btnCreateArea.Enabled = true;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}