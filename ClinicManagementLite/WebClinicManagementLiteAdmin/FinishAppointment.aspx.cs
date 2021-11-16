using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class FinishAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["appointmentId"] != null)
            {
                try
                {
                    Int16 appointmentId = Convert.ToInt16(Session["appointmentId"].ToString());

                    ProxyAppointment.ServiceAppointmentClient proxyAppointment = new ProxyAppointment.ServiceAppointmentClient();
                    ProxyAppointment.AppointmentBE appointmentBE = proxyAppointment.GetOneAppointment(appointmentId);
                    proxyAppointment.Close();

                    txtArea.Text = appointmentBE.User.Area.Name;
                    txtState.Text = appointmentBE.State == "1" ? "Pendiente" : "Finalizado";
                    txtDate.Text = appointmentBE.Date.ToString("dd/MM/yyyy");
                    txtStartHour.Text = appointmentBE.StartHour.ToString(@"hh\:mm");
                    txtEndHour.Text = appointmentBE.EndHour.ToString(@"hh\:mm");
                    txtRealStartHour.Text = appointmentBE.ArrivalHour == null ? "" : appointmentBE.ArrivalHour?.ToString(@"hh\:mm");
                    txtRealEndHour.Text = appointmentBE.DepartureHour == null ? "" : appointmentBE.DepartureHour?.ToString(@"hh\:mm");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        protected void btnFinishAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                btnFinishAppointment.Enabled = false;

                if (txtRealStartHour.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar la hora de llegada del paciente.");
                }

                if (txtRealEndHour.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar la hora de salida del paciente.");
                }

                if (txtReason.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar todos los datos de la prescripción.");
                }

                if (txtPrescription.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar todos los datos de la prescripción.");
                }

                if (txtAllergies.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar todos los datos de la prescripción.");
                }

                if (txtDiseases.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar todos los datos de la prescripción.");
                }

                if (txtMedicines.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar todos los datos de la prescripción.");
                }

                if (txtSurgeries.Text == String.Empty)
                {
                    throw new Exception("Debes ingresar todos los datos de la prescripción.");
                }

                Int16 appointmentId = Convert.ToInt16(Session["appointmentId"].ToString());

                TimeSpan arrivalHour = formatHour(txtRealStartHour.Text);
                TimeSpan departureHour = formatHour(txtRealEndHour.Text);

                ProxyAppointment.ServiceAppointmentClient proxyAppointment = new ProxyAppointment.ServiceAppointmentClient();
                ProxyAppointment.AppointmentBE appointmentBE = proxyAppointment.GetOneAppointment(appointmentId);
                appointmentBE.State = "2";
                appointmentBE.ArrivalHour = arrivalHour;
                appointmentBE.DepartureHour = departureHour;
                proxyAppointment.UpdateAppointment(appointmentBE);
                proxyAppointment.Close();

                ProxyMedicalRecords.ServiceMedicalRecordClient proxyMedRecords = new ProxyMedicalRecords.ServiceMedicalRecordClient();
                ProxyMedicalRecords.MedicalRecordBE medicalRecordBE = new ProxyMedicalRecords.MedicalRecordBE();
                medicalRecordBE.IdAppointment = appointmentId;
                medicalRecordBE.Reason = txtReason.Text;
                medicalRecordBE.Prescription = txtPrescription.Text;
                medicalRecordBE.Diseases = txtDiseases.Text;
                medicalRecordBE.Allergies = txtAllergies.Text;
                medicalRecordBE.Medicines = txtMedicines.Text;
                medicalRecordBE.Surgeries = txtSurgeries.Text;
                proxyMedRecords.CreateMedicalRecord(medicalRecordBE);
                proxyMedRecords.Close();

                viewError.Visible = false;
                viewSuccess.Visible = true;
                lblSuccessMessage.Text = "Consulta finalizada con éxito.";
            }
            catch (Exception ex)
            {
                btnFinishAppointment.Enabled = true;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected TimeSpan formatHour(String hour)
        {
            try
            {
                return TimeSpan.Parse(hour);
            }
            catch (Exception ex)
            {
                throw new Exception("Ingrese la hora en un formato valido. Ej: 12:30");
            }
        }
    }
}