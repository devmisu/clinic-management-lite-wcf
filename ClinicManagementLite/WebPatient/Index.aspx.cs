using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using WebPatient.ProxyPatient;
using System.Collections;
using System.Data;
using WebPatient.ProxyAppointment;

namespace WebPatient
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    Int16 patientId = Convert.ToInt16(User.Identity.Name);
                    String option = Request.QueryString["option"] != null ? Request.QueryString["option"].ToString() : "appointments";

                    updateWelcomeText(patientId);

                    if (option == "medical_record")
                    {
                        //TODO: populate grid view with medical records
                    }
                    else if (option == "patient")
                    {
                        populatePatientGridView(patientId);
                    }
                    else
                    {
                        populateAppointmentsGridView(patientId);
                    }
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
            Int16 appointmentId = Convert.ToInt16(gridView.SelectedRow.Cells[0].Text);
            // TODO: Redirect to appointment detail
        }

        protected void updateWelcomeText(Int16 patientId)
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

        protected PatientBE getPatientBy(Int16 patientId)
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

        protected List<AppointmentBE> getAppointmentsForPatient(Int16 patientId)
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

        protected void populateAppointmentsGridView(Int16 patientId)
        {
            try
            {
                CommandField cField = new CommandField();
                cField.ButtonType = ButtonType.Image;
                cField.ShowSelectButton = true;
                cField.SelectImageUrl = "~/www/img/icon_view_16.png";
                cField.SelectText = "Ver";

                gridView.Columns.Add(cField);

                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Especialidad");
                dataTable.Columns.Add("Doctor");
                dataTable.Columns.Add("Fecha");
                dataTable.Columns.Add("Hora");
                dataTable.Columns.Add("Estado");

                foreach (AppointmentBE appointmentBE in getAppointmentsForPatient(patientId))
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

        protected void populatePatientGridView(Int16 patientId)
        {
            try
            {
                CommandField cField = new CommandField();
                cField.ButtonType = ButtonType.Image;
                cField.ShowSelectButton = true;
                cField.SelectImageUrl = "~/www/img/icon_view_16.png";
                cField.SelectText = "Ver";

                gridView.Columns.Add(cField);

                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Dni");
                dataTable.Columns.Add("Nombres");
                dataTable.Columns.Add("Apellidos");
                dataTable.Columns.Add("Fecha de creacion");

                PatientBE patientBE = getPatientBy(patientId);

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
    }
}