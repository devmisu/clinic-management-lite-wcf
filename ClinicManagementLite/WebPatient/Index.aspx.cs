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
using WebPatient.ProxyAppointment;
using WebPatient.ProxyMedicalRecords;

namespace WebPatient
{
    public partial class Index : System.Web.UI.Page
    {
        Int16 patientId = 0;
        String option = "appointments";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    patientId = Convert.ToInt16(User.Identity.Name);

                    if (Request.QueryString["option"] != null)
                    {
                        option = Request.QueryString["option"].ToString();
                    }

                    updateWelcomeText();
                    setupGridView();
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
            String id = row.Cells[1].Text;

            System.Diagnostics.Debug.WriteLine(id);

            if (option == "medical_record")
            {
                // TODO: Go to medical record detail
            }
            else if (option == "patient")
            {
                // TODO: Go to patient profile
            }
            else
            {
                // TODO: Go to appointment detail
            }
        }

        protected void btnCreateAppointment_Click(object sender, EventArgs e)
        {
            // TODO: Go to appointment creation form
        }

        // Methods

        protected void updateWelcomeText()
        {
            try
            {
                ServicePatientClient proxyPatient = new ServicePatientClient();
                PatientBE patientBE = proxyPatient.GetPatient(patientId);
                proxyPatient.Close();

                lblWelcome.Text = "Hola! " + patientBE.FirstName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void setupGridView()
        {
            try
            {
                CommandField cField = new CommandField();
                cField.ButtonType = ButtonType.Image;
                cField.ShowSelectButton = true;
                cField.SelectImageUrl = "~/www/img/icon_view_16.png";
                cField.SelectText = "Ver";

                gridView.Columns.Add(cField);

                if (option == "medical_record")
                {
                    lblTitle.Text = "Historia Clinica";
                    viewActions.Visible = false;

                    populateMedicalRecordsGridView();
                }
                else if (option == "patient")
                {
                    lblTitle.Text = "Mi Perfil";
                    viewActions.Visible = false;

                    populatePatientGridView();
                }
                else
                {
                    lblTitle.Text = "Mis Citas";
                    viewActions.Visible = true;

                    populateAppointmentsGridView();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected PatientBE getPatient()
        {
            try
            {
                ServicePatientClient proxyPatient = new ServicePatientClient();
                PatientBE patientBE = proxyPatient.GetPatient(patientId);
                proxyPatient.Close();

                return patientBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected List<AppointmentBE> getAppointmentsForPatient()
        {
            try
            {
                ServiceAppointmentClient proxyAppointment = new ServiceAppointmentClient();
                List<AppointmentBE> appointmentBEs = proxyAppointment.GetPatientAppointments(patientId).ToList();
                proxyAppointment.Close();

                return appointmentBEs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected List<MedicalRecordBE> getMedicalRecordsForPatient()
        {
            try
            {
                ServiceMedicalRecordClient proxyMedicalRecord = new ServiceMedicalRecordClient();
                List<MedicalRecordBE> medicalRecordBEs = proxyMedicalRecord.GetPatientMedicalRecords(patientId).ToList();
                proxyMedicalRecord.Close();

                return medicalRecordBEs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void populateAppointmentsGridView()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Especialidad");
                dataTable.Columns.Add("Doctor");
                dataTable.Columns.Add("Fecha");
                dataTable.Columns.Add("Hora");
                dataTable.Columns.Add("Estado");

                foreach (AppointmentBE appointmentBE in getAppointmentsForPatient())
                {
                    DataRow row = dataTable.NewRow();

                    row[0] = appointmentBE.Id.ToString();
                    row[1] = appointmentBE.User.Area.Name;
                    row[2] = appointmentBE.User.FirstName + " " + appointmentBE.User.LastName;
                    row[3] = appointmentBE.Date.ToString("dd/MM/yyyy");
                    row[4] = appointmentBE.StartHour.ToString(@"hh\:mm") + " - " + appointmentBE.EndHour.ToString(@"hh\:mm");
                    row[5] = appointmentBE.State == "1" ? "Pendiente" : "Finalizado";

                    dataTable.Rows.Add(row);
                }

                gridView.DataSource = dataTable;
                gridView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void populatePatientGridView()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Dni");
                dataTable.Columns.Add("Nombres");
                dataTable.Columns.Add("Apellidos");
                dataTable.Columns.Add("Fecha de creacion");

                PatientBE patientBE = getPatient();

                DataRow row = dataTable.NewRow();

                row[0] = patientBE.Id.ToString();
                row[1] = patientBE.Dni;
                row[2] = patientBE.FirstName;
                row[3] = patientBE.LastName;
                row[4] = patientBE.CreatedAt.ToString("dd/MM/yyyy");

                dataTable.Rows.Add(row);

                gridView.DataSource = dataTable;
                gridView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        protected void populateMedicalRecordsGridView()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Id");

                foreach (MedicalRecordBE medicalRecordBE in getMedicalRecordsForPatient())
                {
                    DataRow row = dataTable.NewRow();

                    row[0] = medicalRecordBE.Id.ToString();

                    dataTable.Rows.Add(row);
                }

                gridView.DataSource = dataTable;
                gridView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}