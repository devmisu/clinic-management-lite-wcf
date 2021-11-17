using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClinicManagementLiteAdmin.ProxyUser;
using System.Text.RegularExpressions;

namespace WebClinicManagementLiteAdmin
{
    public partial class UpdateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["userId"] != null)
            {
                PopulateAreas();
                PopulateRole();
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

                if (txtPassword.Text.Trim().Length <= 0)
                {
                    throw new Exception("Ingrese una contraseña valida.");
                }

                if (ddlArea.SelectedItem.Text.Length == 0)
                {
                    throw new Exception("Escoja un área válida.");
                }

                if (ddlRole.SelectedItem.Text.Length == 0)
                {
                    throw new Exception("Escoja un rol válido.");
                }

                UserBE userBE = new UserBE();
                userBE.Id = Convert.ToInt16(Session["userId"].ToString());
                userBE.FirstName = txtFirstName.Text.Trim();
                userBE.LastName = txtLastName.Text.Trim();
                userBE.Phone = txtPhone.Text.Trim();
                userBE.Email = txtEmail.Text.Trim();
                userBE.Specialization = txtSpecialization.Text.Trim();
                userBE.Password = txtPassword.Text.Trim();
                userBE.IdArea = Convert.ToInt16(ddlArea.SelectedItem.Value);
                userBE.IdRole = Convert.ToInt16(ddlRole.SelectedItem.Value);

                ServiceUserClient proxyUser = new ServiceUserClient();

                if (proxyUser.UpdateUser(userBE))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Actualizacion de usuario exitosa!";
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

        private void PopulateAreas()
        {
            try
            {
                ProxyArea.ServiceAreaClient proxyArea = new ProxyArea.ServiceAreaClient();
                List<ProxyArea.AreaBE> areas = proxyArea.GetAllAreas().ToList();
                ddlArea.DataSource = areas;
                ddlArea.DataTextField = "name";
                ddlArea.DataValueField = "id";
                ddlArea.DataBind();
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
                ddlRole.DataSource = roles;
                ddlRole.DataTextField = "name";
                ddlRole.DataValueField = "id";
                ddlRole.DataBind();
                proxyRole.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        private void PopulateUser()
        {
            try
            {
                Int16 userId = Convert.ToInt16(Session["userId"].ToString());

                ServiceUserClient proxyUser = new ServiceUserClient();
                UserBE userBE = proxyUser.GetUser(userId);
                proxyUser.Close();

                ddlArea.SelectedValue = userBE.IdArea.ToString();
                ddlRole.SelectedValue = userBE.IdRole.ToString();
                txtFirstName.Text = userBE.FirstName;
                txtLastName.Text = userBE.LastName;
                txtDni.Text = userBE.Dni;
                txtPhone.Text = userBE.Phone;
                txtEmail.Text = userBE.Email;
                txtSpecialization.Text = userBE.Specialization;
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
    }
}