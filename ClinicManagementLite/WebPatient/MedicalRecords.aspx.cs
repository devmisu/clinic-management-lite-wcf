using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Data;
using WebPatient.ProxyPatient;
using WebPatient.ProxyMedicalRecords;

namespace WebPatient
{
    public partial class MedicalRecords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    Int16 patientId = Convert.ToInt16(User.Identity.Name);

                    ProxyPatient.ServicePatientClient proxyPatient = new ProxyPatient.ServicePatientClient();
                    ProxyPatient.PatientBE patientBE = proxyPatient.GetPatient(patientId);
                    proxyPatient.Close();

                    lblWelcome.Text = "Hola! " + patientBE.FirstName;

                    ProxyMedicalRecords.ServiceMedicalRecordClient proxyMedicalRecord = new ProxyMedicalRecords.ServiceMedicalRecordClient();
                    List<ProxyMedicalRecords.MedicalRecordBE> medicalRecordBEs = proxyMedicalRecord.GetPatientMedicalRecords(patientId).ToList();
                    proxyMedicalRecord.Close();

                    CommandField cField = new CommandField();

                    cField.ButtonType = ButtonType.Image;
                    cField.ShowSelectButton = true;
                    cField.SelectImageUrl = "~/www/img/icon_view_16.png";
                    cField.SelectText = "Ver";

                    gridView.Columns.Add(cField);

                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("Id");
                    dataTable.Columns.Add("Especialidad");
                    dataTable.Columns.Add("Razon");
                    dataTable.Columns.Add("Prescripcion");
                    dataTable.Columns.Add("Fecha de creacion");

                    foreach (ProxyMedicalRecords.MedicalRecordBE medicalRecordBE in medicalRecordBEs)
                    {
                        DataRow row = dataTable.NewRow();

                        row[0] = medicalRecordBE.Id.ToString();
                        row[1] = medicalRecordBE.Appointment.User.Area.Name;
                        row[2] = medicalRecordBE.Reason;
                        row[3] = medicalRecordBE.Prescription;
                        row[4] = medicalRecordBE.CreatedAt.ToString("dd/MM/yyyy");

                        dataTable.Rows.Add(row);
                    }

                    gridView.DataSource = dataTable;
                    gridView.DataBind();
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gridView.SelectedRow;
            String medicalRecordId = row.Cells[1].Text;

            Session["medicalRecordId"] = medicalRecordId;
            Response.Redirect("MedicalRecord.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void btnMedicalRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("MedicalRecords.aspx");
        }
    }
}