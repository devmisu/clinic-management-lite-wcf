using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;

namespace WCFClinic.Services
{
    public class ServicePatient : IServicePatient
    {
        public bool CreatePatient(PatientBE objPatientBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Patient tbPatient = new Patient();

                tbPatient.first_name = objPatientBE.FirstName;
                tbPatient.last_name = objPatientBE.LastName;
                tbPatient.birthday = objPatientBE.Birthday; //validate type
                tbPatient.phone = objPatientBE.Phone;
                tbPatient.photo = objPatientBE.Photo;
                tbPatient.email = objPatientBE.Email;
                tbPatient.dni = objPatientBE.Dni;
                tbPatient.password = objPatientBE.Password;
                tbPatient.active = objPatientBE.Active;
                tbPatient.created_at = DateTime.Now;

                db.Patients.Add(tbPatient);
                db.SaveChanges();

                return true;
            }
            catch(EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeletePatient(short id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Patient tbPatient = (from patient in db.Patients where patient.id == id select patient);

                db.Patients.Remove(tbPatient);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdatePatient(PatientBE objPatientBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Patient tbPatient = (from patient in db.Patients where patient.id == objPatientBE.Id select patient).FirstOrDefault();

                tbPatient.first_name = objPatientBE.FirstName;
                tbPatient.last_name = objPatientBE.LastName;
                tbPatient.birthday = objPatientBE.Birthday; //validate type
                tbPatient.phone = objPatientBE.Phone;
                tbPatient.photo = objPatientBE.Photo;
                tbPatient.email = objPatientBE.Email;
                tbPatient.dni = objPatientBE.Dni;
                tbPatient.password = objPatientBE.Password;
                tbPatient.active = objPatientBE.Active;

                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PatientBE> GetAllPatients()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<PatientBE> listPatients = new List<PatientBE>();

                var query = (from patient in db.Patients orderby patient.last_name select patient);

                foreach (var tbPatient in query)
                {
                    PatientBE objPatientBE = new PatientBE();

                    objPatientBE.Id = Convert.ToInt16(tbPatient.id);
                    objPatientBE.FirstName = tbPatient.first_name;
                    objPatientBE.LastName = tbPatient.last_name;
                    objPatientBE.Birthday = tbPatient.birthday;
                    objPatientBE.Phone = tbPatient.phone;
                    objPatientBE.Photo = tbPatient.photo;
                    objPatientBE.Email = tbPatient.email;
                    objPatientBE.Dni = tbPatient.dni;
                    objPatientBE.Password = tbPatient.password;
                    objPatientBE.Active = tbPatient.active;
                    objPatientBE.CreatedAt = tbPatient.created_at;

                    listPatients.Add(objPatientBE);
                }

                return listPatients;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PatientBE GetPatient(short id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Patient tbPatient = (from patient in db.Patients where patient.id == id select patient).FirstOrDefault();

                PatientBE objPatientBE = new PatientBE();

                objPatientBE.Id = Convert.ToInt16(tbPatient.id);
                objPatientBE.FirstName = tbPatient.first_name;
                objPatientBE.LastName = tbPatient.last_name;
                objPatientBE.Birthday = tbPatient.birthday;
                objPatientBE.Phone = tbPatient.phone;
                objPatientBE.Photo = tbPatient.photo;
                objPatientBE.Email = tbPatient.email;
                objPatientBE.Dni = tbPatient.dni;
                objPatientBE.Password = tbPatient.password;
                objPatientBE.Active = tbPatient.active;
                objPatientBE.CreatedAt = tbPatient.created_at;

                return objPatientBE;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
