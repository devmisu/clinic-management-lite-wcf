using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text.RegularExpressions;
using WebPatient.ProxyPatient;

namespace WebPatient
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;

                if (!Regex.IsMatch(txtDni.Text.Trim(), @"^[\d]{8}$"))
                {
                    throw new Exception("Ingrese un DNI valido.");
                }

                if (txtPassword.Text.Trim().Length <= 0)
                {
                    throw new Exception("Ingrese una contraseña valida.");
                }

                ServicePatientClient proxyPatient = new ServicePatientClient();
                PatientBE patientBE = proxyPatient.Login(txtDni.Text.Trim(), txtPassword.Text.Trim());
                proxyPatient.Close();

                FormsAuthentication.RedirectFromLoginPage(patientBE.Id.ToString(), true);
            }
            catch (Exception ex)
            {
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnLogin.Enabled = true;
            }
        }
    }
}