using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxyUser;

namespace WebClinicManagementLiteAdmin.Login
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(txtDni.Text, @"^[\d]{8}$"))
                {
                    throw new Exception("Ingrese un DNI valido.");
                }

                if (txtPassword.Text.Length <= 0)
                {
                    throw new Exception("Ingrese una contraseña valida.");
                }

                ServiceUserClient proxyUser = new ServiceUserClient();
                UserBE userBE = proxyUser.Login(txtDni.Text, txtPassword.Text);
                proxyUser.Close();

                FormsAuthentication.RedirectFromLoginPage(userBE.Id.ToString(), true);
            }
            catch (Exception ex)
            {
                errorMessage.Visible = true;
                errorMessage.Text = ex.Message;
            }
        }
    }
}