using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClinicManagementLiteAdmin
{
    public partial class ShowPatients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    gdvPatients.DataSource = GetPatientsTable();
                    CommandField editBtn = new CommandField();
                    editBtn.ButtonType = ButtonType.Image;
                    editBtn.SelectImageUrl = "~/assets/icon_edit_16.png";
                    editBtn.ShowSelectButton = true;
                    gdvPatients.Columns.Add(editBtn);
                    gdvPatients.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                    divContainerNoQueues.Visible = true;
                    gdvPatients.Visible = false;
                }
            }
        }

        protected void gdvPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = gdvPatients.SelectedRow.RowIndex;
                ProxyPatient.ServicePatientClient proxyUser = new ProxyPatient.ServicePatientClient();
                List<ProxyPatient.PatientBE> arrayUsers = proxyUser.GetAllPatients().ToList();
                proxyUser.Close();

                Session["patientId"] = arrayUsers[selectedIndex].Id;
                Response.Redirect("UpdatePatient.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnCreatePatient_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreatePatient.aspx");
        }


        private DataTable GetPatientsTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Dni");
            dataTable.Columns.Add("Nombres");
            dataTable.Columns.Add("Apellidos");
            dataTable.Columns.Add("Telefono contacto");
            dataTable.Columns.Add("Email contacto");
            dataTable.Columns.Add("Nacimiento");
            dataTable.Columns.Add("Fecha de creacion");

            try
            {
                ProxyPatient.ServicePatientClient proxy = new ProxyPatient.ServicePatientClient();
                List<ProxyPatient.PatientBE> array = proxy.GetAllPatients().ToList();
                proxy.Close();

                if (array.Count == 0)
                {
                    divContainerNoQueues.Visible = true;
                }

                foreach (ProxyPatient.PatientBE user in array)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = user.Dni;
                    row[1] = user.FirstName;
                    row[2] = user.LastName;
                    row[3] = user.Phone;
                    row[4] = user.Email;
                    row[5] = user.Birthday.ToString("dd/MM/yyyy");
                    row[6] = user.CreatedAt.ToString("dd/MM/yyyy");

                    dataTable.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return dataTable;
        }
    }
}