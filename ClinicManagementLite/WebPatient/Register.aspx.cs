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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                btnRegister.Enabled = false;

                if (txtFirstName.Text.Trim().Length == 0)
                {
                    throw new Exception("El campo nombres es obligatorio.");
                }

                if (txtLastName.Text.Trim().Length == 0)
                {
                    throw new Exception("El campo apellidos es obligatorio.");
                }

                if (!Regex.IsMatch(txtDni.Text.Trim(), @"^[\d]{8}$"))
                {
                    throw new Exception("Ingrese un DNI valido.");
                }

                if (!Regex.IsMatch(txtPhone.Text.Trim(), @"^[9]{1}\d{8}$"))
                {
                    throw new Exception("Ingrese un celular valido.");
                }

                if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new Exception("Ingrese un email valido.");
                }

                if (txtBirthday.Text.Trim().Length <= 0)
                {
                    throw new Exception("Ingrese una fecha de nacimiento valida.");
                }

                if (txtPassword.Text.Trim().Length <= 0)
                {
                    throw new Exception("Ingrese una contraseña valida.");
                }

                if (!txtPassword2.Text.Trim().Equals(txtPassword.Text.Trim()))
                {
                    throw new Exception("Las contraseñas no coinciden.");
                }

                PatientBE patientBE = new PatientBE();
                patientBE.FirstName = txtFirstName.Text.Trim();
                patientBE.LastName = txtLastName.Text.Trim();
                patientBE.Dni = txtDni.Text.Trim();
                patientBE.Phone = txtPhone.Text.Trim();
                patientBE.Email = txtEmail.Text.Trim();
                patientBE.Birthday = Convert.ToDateTime(txtBirthday.Text.Trim());
                patientBE.Password = txtPassword2.Text.Trim();
                
                ServicePatientClient proxyPatient = new ServicePatientClient();

                if (proxyPatient.CreatePatient(patientBE))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Registro exitoso!";
                }

                proxyPatient.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnRegister.Enabled = true;
            }
        }
    }
}