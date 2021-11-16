using System;
using System.Web;

namespace WebPatient
{
    public partial class MedicalRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["medicalRecordId"] != null)
            {
                try
                {
                    Int16 medicalRecordId = Convert.ToInt16(Session["medicalRecordId"].ToString());

                    ProxyMedicalRecords.ServiceMedicalRecordClient proxyMedicalRecord = new ProxyMedicalRecords.ServiceMedicalRecordClient();
                    ProxyMedicalRecords.MedicalRecordBE medicalRecordBE = proxyMedicalRecord.GetMedicalRecord(medicalRecordId);
                    proxyMedicalRecord.Close();

                    txtReason.Text = medicalRecordBE.Reason;
                    txtPrescription.Text = medicalRecordBE.Prescription;
                    txtDiseases.Text = medicalRecordBE.Diseases;
                    txtAllergies.Text = medicalRecordBE.Allergies;
                    txtMedicines.Text = medicalRecordBE.Medicines;
                    txtSurgeries.Text = medicalRecordBE.Surgeries;
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }
    }
}