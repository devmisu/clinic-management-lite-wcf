using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPatient.ProxyPatient;
using WebPatient.ProxyQueue;
using System.Data;
using System.Web.Security;

namespace WebPatient
{
    public partial class Queues : System.Web.UI.Page
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

                    ServiceQueueClient proxyQueue = new ServiceQueueClient();
                    List<QueueBE> queueBEs = proxyQueue.GetPatientQueues(patientId).ToList();
                    proxyQueue.Close();

                    CommandField cField = new CommandField();

                    cField.ButtonType = ButtonType.Image;
                    cField.ShowSelectButton = true;
                    cField.SelectImageUrl = "~/www/img/icon_view_16.png";
                    cField.SelectText = "Ver";

                    gridViewQueue.Columns.Add(cField);

                    DataTable dataTableQueue = new DataTable();

                    dataTableQueue.Columns.Add("Id");
                    dataTableQueue.Columns.Add("Especialidad");
                    dataTableQueue.Columns.Add("Doctor");
                    dataTableQueue.Columns.Add("Fecha");
                    dataTableQueue.Columns.Add("Hora");
                    dataTableQueue.Columns.Add("Estado");
                    dataTableQueue.Columns.Add("Fecha de creacion");

                    foreach (QueueBE queueBE in queueBEs)
                    {
                        var state = "";

                        switch (queueBE.State)
                        {
                            case "1":
                                state = "Solicitado";
                                break;
                            case "2":
                                state = "Aceptado";
                                break;
                            case "3":
                                state = "Cancelado";
                                break;
                            case "4":
                                state = "Rechazado";
                                break;
                        }

                        DataRow row = dataTableQueue.NewRow();

                        row[0] = queueBE.Id.ToString();
                        row[1] = queueBE.User.Area.Name;
                        row[2] = queueBE.User.FirstName + " " + queueBE.User.LastName;
                        row[3] = queueBE.StartDate.ToString("dd/MM/yyyy");
                        row[4] = queueBE.StartTime.ToString(@"hh\:mm");
                        row[5] = state;
                        row[6] = queueBE.CreatedAt.ToString("dd/MM/yyyy");

                        dataTableQueue.Rows.Add(row);
                    }

                    gridViewQueue.DataSource = dataTableQueue;
                    gridViewQueue.DataBind();
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

        protected void gridViewQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gridViewQueue.SelectedRow;
            String queueId = row.Cells[1].Text;

            Session["queueId"] = queueId;
            Response.Redirect("Appointment.aspx");
        }

        protected void btnCreateQueue_Click(object sender, EventArgs e)
        {
            Response.Redirect("QueueAppointment.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnQueue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Queues.aspx");
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