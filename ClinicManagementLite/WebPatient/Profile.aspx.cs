using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                    ServicePatientClient proxyPatient = new ServicePatientClient();
                    PatientBE patientBE = proxyPatient.GetPatient(patientId);
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
    }
}