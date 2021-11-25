using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class CreatePatient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                btnCreateUser.Enabled = false;

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

                if (txtDate.Text.Trim().Length == 0)
                {
                    throw new Exception("Debes ingresar una fecha.");
                }

                if (txtPassword.Text.Trim().Length <= 0)
                {
                    throw new Exception("Ingrese una contraseña valida.");
                }

                if (!txtPassword2.Text.Trim().Equals(txtPassword.Text.Trim()))
                {
                    throw new Exception("Las contraseñas no coinciden.");
                }


                ProxyPatient.PatientBE userBE = new ProxyPatient.PatientBE();
                userBE.FirstName = txtFirstName.Text.Trim();
                userBE.LastName = txtLastName.Text.Trim();
                userBE.Dni = txtDni.Text.Trim();
                userBE.Phone = txtPhone.Text.Trim();
                userBE.Email = txtEmail.Text.Trim();
                DateTime date = Convert.ToDateTime(txtDate.Text);
                userBE.Birthday = date;
                userBE.Password = txtPassword2.Text.Trim();

                ProxyPatient.ServicePatientClient proxyUser = new ProxyPatient.ServicePatientClient();

                if (proxyUser.CreatePatient(userBE))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Creacion de usuario exitosa!";
                }

                proxyUser.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnCreateUser.Enabled = true;
            }
        }

    }
}