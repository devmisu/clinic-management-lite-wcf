using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateAreas();
            PopulateRole();
        }

        private void PopulateAreas()
        {
            try
            {
                ProxyArea.ServiceAreaClient proxyArea = new ProxyArea.ServiceAreaClient();
                List<ProxyArea.AreaBE> areas = proxyArea.GetAllAreas().ToList();
                dropArea.DataSource = areas;
                dropArea.DataTextField = "name";
                dropArea.DataValueField = "id";
                dropArea.DataBind();
                proxyArea.Close();
            } 
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        private void PopulateRole()
        {
            try
            {
                ProxyRole.ServiceRoleClient proxyRole = new ProxyRole.ServiceRoleClient();
                List<ProxyRole.RoleBE> roles = proxyRole.GetAllRoles().ToList();
                dropRole.DataSource = roles;
                dropRole.DataTextField = "name";
                dropRole.DataValueField = "id";
                dropRole.DataBind();
                proxyRole.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
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

                if (!Regex.IsMatch(txtEmail.Text.Trim(), @"[a-z0 - 9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
                {
                    throw new Exception("Ingrese un email valido.");
                }

                if (txtSpecialization.Text.Trim().Length <= 0)
                {
                    throw new Exception("El campo especialización es obligatorio.");
                }

                if (txtPassword.Text.Trim().Length <= 0)
                {
                    throw new Exception("Ingrese una contraseña valida.");
                }

                if (!txtPassword2.Text.Trim().Equals(txtPassword.Text.Trim()))
                {
                    throw new Exception("Las contraseñas no coinciden.");
                }

                if (dropArea.SelectedItem.Text.Length == 0)
                {
                    throw new Exception("Escoja un área válida.");
                }

                if (dropRole.SelectedItem.Text.Length == 0)
                {
                    throw new Exception("Escoja un rol válido.");
                }



                ProxyUser.UserBE userBE = new ProxyUser.UserBE();
                userBE.FirstName = txtFirstName.Text.Trim();
                userBE.LastName = txtLastName.Text.Trim();
                userBE.Dni = txtDni.Text.Trim();
                userBE.Phone = txtPhone.Text.Trim();
                userBE.Email = txtEmail.Text.Trim();
                userBE.Specialization = txtSpecialization.Text.Trim();
                userBE.Password = txtPassword2.Text.Trim();
                userBE.IdArea = Convert.ToInt16(dropArea.SelectedItem.Value);
                userBE.IdRole = Convert.ToInt16(dropRole.SelectedItem.Value);

                ProxyUser.ServiceUserClient proxyUser = new ProxyUser.ServiceUserClient();

                if (proxyUser.CreateUser(userBE))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Registro exitoso!";
                }

                proxyUser.Close();
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