using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class UpdateRol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["roleId"] != null)
            {
                try
                {
                    viewError.Visible = false;

                    Int16 roleId = Convert.ToInt16(Session["roleId"].ToString());
                    ProxyRole.ServiceRoleClient proxyRole = new ProxyRole.ServiceRoleClient();
                    ProxyRole.RoleBE roleBE = proxyRole.GetOneRole(roleId);
                    proxyRole.Close();
                    validateCheck(roleBE.Attributes);
                    txtRolName.Text = roleBE.Name;
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    viewSuccess.Visible = false;
                    lblErrorMessage.Text = ex.Message;
                }
            }
            
        }

        protected void validateCheck(String list)
        {
            cbxSchedule.Checked = list.Contains(Util.Constants.Schedule);
            cbxAppointment.Checked = list.Contains(Util.Constants.Appointment);
            cbxQueue.Checked = list.Contains(Util.Constants.Queue);
            cbxDoctors.Checked = list.Contains(Util.Constants.Doctors);
            cbxRol.Checked = list.Contains(Util.Constants.Rol);
            cbxPatient.Checked = list.Contains(Util.Constants.Patient);
            cbxArea.Checked = list.Contains(Util.Constants.Area);

        }

        protected void btnUpdateRol_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateRol.Enabled = false;

                if (txtRolName.Text == String.Empty)
                {
                    throw new Exception("Debe asignar un nombre al nuevo rol.");
                }

                Int16 roleId = Convert.ToInt16(Session["roleId"].ToString());
                ProxyRole.ServiceRoleClient proxyRole = new ProxyRole.ServiceRoleClient();
                ProxyRole.RoleBE roleBE = proxyRole.GetOneRole(roleId);
                roleBE.Name = txtRolName.Text.Trim();
                roleBE.Attributes = getRolesSelected();
                proxyRole.UpdateRole(roleBE);
                proxyRole.Close();

                lblSuccessMessage.Text = "Rol actualizado!";
                viewError.Visible = false;
                viewSuccess.Visible = true;
            } 
            catch (Exception ex)
            {
                btnUpdateRol.Enabled = true;
                viewError.Visible = true;
                viewSuccess.Visible = false;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected String getRolesSelected()
        {
            String rolesConcatList = "";

            if (cbxSchedule.Checked)
            {
                rolesConcatList += Util.Constants.Schedule + ",";
            }
            if (cbxAppointment.Checked)
            {
                rolesConcatList += Util.Constants.Appointment + ",";
            }
            if (cbxQueue.Checked)
            {
                rolesConcatList += Util.Constants.Queue + ",";
            }
            if (cbxDoctors.Checked)
            {
                rolesConcatList += Util.Constants.Doctors + ",";
            }
            if (cbxRol.Checked)
            {
                rolesConcatList += Util.Constants.Rol + ",";
            }
            if (cbxPatient.Checked)
            {
                rolesConcatList += Util.Constants.Patient + ",";
            }
            if (cbxArea.Checked)
            {
                rolesConcatList += Util.Constants.Area + ",";
            }
            if (rolesConcatList == String.Empty)
            {
                throw new Exception("Debe seleccionar al menos un valor.");
            }

            return rolesConcatList.Remove(rolesConcatList.Length - 1);
        }
    }
}