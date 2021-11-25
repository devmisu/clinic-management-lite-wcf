using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class UpdatePatient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["patientId"] != null)
            {
                PopulateUser();
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateUser.Enabled = false;

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


                ProxyPatient.PatientBE userBE = new ProxyPatient.PatientBE();

                userBE.Id = Convert.ToInt16(Session["patientId"].ToString());

                userBE.FirstName = txtFirstName.Text.Trim();
                userBE.LastName = txtLastName.Text.Trim();
                userBE.Phone = txtPhone.Text.Trim();
                userBE.Email = txtEmail.Text.Trim();
                userBE.Password = txtPassword.Text.Trim();

                ProxyPatient.ServicePatientClient proxyUser = new ProxyPatient.ServicePatientClient();

                if (proxyUser.UpdatePatient(userBE))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Actualizacion de paciente exitosa!";
                }

                proxyUser.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnUpdateUser.Enabled = true;
            }
        }

        private void PopulateUser()
        {
            try
            {
                Int16 patientID = Convert.ToInt16(Session["patientId"].ToString());

                ProxyPatient.ServicePatientClient proxyUser = new ProxyPatient.ServicePatientClient();
                ProxyPatient.PatientBE userBE = proxyUser.GetPatient(patientID);
                proxyUser.Close();

                txtFirstName.Text = userBE.FirstName;
                txtLastName.Text = userBE.LastName;
                txtDni.Text = userBE.Dni;
                txtDate.Text = userBE.Birthday.ToString("dd/MM/yyyy");
                txtPhone.Text = userBE.Phone;
                txtEmail.Text = userBE.Email;
                txtPassword.Text = userBE.Password;
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Attributes["type"] = txtPassword.Attributes["type"] == "password" ? "text" : "password";
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateUser.Enabled = false;

                Int16 patientID = Convert.ToInt16(Session["patientId"].ToString());

                ProxyPatient.ServicePatientClient proxyUser = new ProxyPatient.ServicePatientClient();

                if (proxyUser.DeletePatient(patientID))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Se eliminó el paciente!";
                }

                proxyUser.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnUpdateUser.Enabled = true;
            }
        }

    }
}