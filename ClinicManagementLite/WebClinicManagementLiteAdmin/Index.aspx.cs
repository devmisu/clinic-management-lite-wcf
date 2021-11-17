using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxyUser;

namespace WebClinicManagementLiteAdmin
{
    public partial class Index : System.Web.UI.Page
    {

        UserBE user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                getUser();
                tvWelcomeMsg.Text = "Bienvenido " + user.FirstName + " " + user.LastName + " a su página de Inicio";
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        private void getUser()
        {
            try
            {
                ServiceUserClient proxyUser = new ServiceUserClient();
                String idUser = HttpContext.Current.User.Identity.Name;
                user = proxyUser.GetUser(Convert.ToInt16(idUser));
                proxyUser.Close();
            } 
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnAppointments_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowAppointments.aspx");
        }

        protected void btnRoles_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowRoles.aspx");
        }

        protected void btnQueue_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowQueues.aspx");
        }

        protected void btnUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowUsers.aspx");
        }
    }
}