using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using WebPatient.ProxyAppointment;
using WebPatient.ProxyPatient;

namespace WebPatient
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Int16 patientId = Convert.ToInt16(User.Identity.Name);

                try
                {
                    ProxyPatient.ServicePatientClient proxyPatient = new ProxyPatient.ServicePatientClient();
                    ProxyPatient.PatientBE patientBE = proxyPatient.GetPatient(patientId);
                    proxyPatient.Close();

                    txtDni.Text = patientBE.Dni;
                    txtPassword.Text = patientBE.Password;
                    txtFirstName.Text = patientBE.FirstName;
                    txtLastName.Text = patientBE.LastName;
                    txtBirthday.Text = patientBE.Birthday.ToString("dd/MM/yyyy");
                    txtPhone.Text = patientBE.Phone;
                    txtEmail.Text = patientBE.Email;
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Attributes["type"] = txtPassword.Attributes["type"] == "password" ? "text" : "password";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdate.Enabled = false;

                if (txtFirstName.Text.Trim().Length == 0)
                {
                    throw new Exception("El campo nombres es obligatorio.");
                }

                if (txtLastName.Text.Trim().Length == 0)
                {
                    throw new Exception("El campo apellidos es obligatorio.");
                }

                if (!Regex.IsMatch(txtPhone.Text.Trim(), @"^[9]{1}\d{8}$"))
                {
                    throw new Exception("Ingrese un celular valido.");
                }

                if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new Exception("Ingrese un email valido.");
                }

                if (txtPassword.Text.Trim().Length <= 0)
                {
                    throw new Exception("Ingrese una contraseña valida.");
                }

                ProxyPatient.PatientBE patientBE = new ProxyPatient.PatientBE();
                patientBE.Id = Convert.ToInt16(User.Identity.Name);
                patientBE.FirstName = txtFirstName.Text.Trim();
                patientBE.LastName = txtLastName.Text.Trim();
                patientBE.Dni = txtDni.Text.Trim();
                patientBE.Phone = txtPhone.Text.Trim();
                patientBE.Email = txtEmail.Text.Trim();
                patientBE.Birthday = DateTime.ParseExact(txtBirthday.Text.Trim(), "dd/MM/yyyy", null);
                patientBE.Password = txtPassword.Text.Trim();

                ServicePatientClient proxyPatient = new ServicePatientClient();

                if (proxyPatient.UpdatePatient(patientBE))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Actualizacion exitosa!";
                }

                proxyPatient.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnUpdate.Enabled = true;
            }
        }

        protected void switchEdit_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.Enabled = switchEdit.Checked;
            txtFirstName.Enabled = switchEdit.Checked;
            txtLastName.Enabled = switchEdit.Checked;
            txtPhone.Enabled = switchEdit.Checked;
            txtEmail.Enabled = switchEdit.Checked;
            btnUpdate.Enabled = switchEdit.Checked;
        }
    }
}