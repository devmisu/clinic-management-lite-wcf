using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class CreateRol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    viewError.Visible = false;
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void btnCreateRol_Click(object sender, EventArgs e)
        {
            try
            {
                btnCreateRol.Enabled = false;

                if (txtRolName.Text == String.Empty)
                {
                    throw new Exception("Debe asignar un nombre al nuevo rol.");
                }

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
                String finalRoles = rolesConcatList.Remove(rolesConcatList.Length - 1);

                ProxyRole.ServiceRoleClient proxyRole = new ProxyRole.ServiceRoleClient();
                ProxyRole.RoleBE roleBE = new ProxyRole.RoleBE();
                roleBE.Name = txtRolName.Text;
                roleBE.Attributes = finalRoles;
                proxyRole.CreateRole(roleBE);
                proxyRole.Close();

                viewError.Visible = false;
                viewSuccess.Visible = true;
                lblSuccessMessage.Text = "Rol creado correctamente!";
            }
            catch (Exception ex)
            {
                btnCreateRol.Enabled = true;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}