using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPatient.ProxyQueue;

namespace WebPatient
{
    public partial class Queue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated && Session["queueId"] != null)
            {
                try
                {
                    Int16 queueId = Convert.ToInt16(Session["queueId"].ToString());

                    ServiceQueueClient proxyQueue = new ServiceQueueClient();
                    QueueBE queueBE = proxyQueue.GetOneQueue(queueId);
                    proxyQueue.Close();

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

                    txtArea.Text = queueBE.User.Area.Name;
                    txtState.Text = state;
                    txtDate.Text = queueBE.StartDate.ToString("dd/MM/yyyy");
                    txtStartHour.Text = queueBE.StartTime.ToString(@"hh\:mm");
                    txtDoctor.Text = queueBE.User.FirstName + " " + queueBE.User.LastName;
                    txtDoctorDni.Text = queueBE.User.Dni;
                    txtDoctorPhone.Text = queueBE.User.Phone;
                    txtDoctorEmail.Text = queueBE.User.Email;
                }
                catch (Exception ex)
                {
                    viewError.Visible = true;
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                btnCancel.Enabled = false;

                QueueBE queueBE = new QueueBE();
                queueBE.Id = Convert.ToInt16(Session["queueId"].ToString());
                queueBE.State = "3";

                ServiceQueueClient proxyQueue = new ServiceQueueClient();
                
                if (proxyQueue.UpdateQueue(queueBE))
                {
                    viewError.Visible = false;
                    viewSuccess.Visible = true;
                    lblSuccessMessage.Text = "Se cancelo la solicitud!";
                }

                proxyQueue.Close();
            }
            catch (Exception ex)
            {
                viewSuccess.Visible = false;
                viewError.Visible = true;
                lblErrorMessage.Text = ex.Message;

                btnCancel.Enabled = true;
            }
        }
    }
}