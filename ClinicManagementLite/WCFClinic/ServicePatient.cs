using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;

namespace WCFClinic
{
    public class ServicePatient : IServicePatient
    {
        public bool CreatePatient(PatientBE objPatientBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (objPatientBE.FirstName == null ||
                    objPatientBE.LastName == null ||
                    objPatientBE.Birthday == null ||
                    objPatientBE.Phone == null || objPatientBE.Phone.Length != 9 ||
                    objPatientBE.Email == null ||
                    objPatientBE.Dni == null || objPatientBE.Dni.Length != 8 ||
                    objPatientBE.Password == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                if ((from patient in db.Patients where patient.active && patient.dni == objPatientBE.Dni select patient).FirstOrDefault() != null)
                {
                    throw new Exception("Ya existe un paciente registrado con ese DNI.");
                }

                Patient tbPatient = new Patient();

                tbPatient.first_name = objPatientBE.FirstName;
                tbPatient.last_name = objPatientBE.LastName;
                tbPatient.birthday = objPatientBE.Birthday;
                tbPatient.phone = objPatientBE.Phone;
                tbPatient.photo = objPatientBE.Photo;
                tbPatient.email = objPatientBE.Email;
                tbPatient.dni = objPatientBE.Dni;
                tbPatient.password = objPatientBE.Password;
                tbPatient.active = true;
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

        public bool DeletePatient(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Patient tbPatient = (from patient in db.Patients where patient.active && patient.id == id select patient).FirstOrDefault();

                if (tbPatient == null)
                {
                    throw new Exception("No se encontro paciente.");
                }

                if (new ServiceAppointment().GetPatientAppointments(id).Count != 0)
                {
                    throw new Exception("No se puede eliminar, hay citas asignadas al paciente.");
                }

                if ((from queue in db.Queues where queue.active && queue.Patient.active && queue.Patient.id == id select queue).Count() != 0)
                {
                    throw new Exception("No se puede eliminar, hay citas en cola asignadas al paciente.");
                }

                tbPatient.active = false;
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
                Patient tbPatient = (from patient in db.Patients where patient.active && patient.id == objPatientBE.Id select patient).FirstOrDefault();

                if (tbPatient == null)
                {
                    throw new Exception("No se encontro paciente.");
                }

                if (objPatientBE.FirstName != null) tbPatient.first_name = objPatientBE.FirstName;
                if (objPatientBE.LastName != null) tbPatient.last_name = objPatientBE.LastName;
                if (objPatientBE.Birthday != null) tbPatient.birthday = objPatientBE.Birthday;
                if (objPatientBE.Phone != null && objPatientBE.Phone.Length == 9) tbPatient.phone = objPatientBE.Phone;
                if (objPatientBE.Email != null) tbPatient.email = objPatientBE.Email;
                if (objPatientBE.Password != null) tbPatient.password = objPatientBE.Password;
                tbPatient.photo = objPatientBE.Photo;

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

                var query = (from patient in db.Patients orderby patient.first_name where patient.active select patient);

                foreach (var tbPatient in query)
                {
                    listPatients.Add(PatientBE.Create(tbPatient));
                }

                return listPatients;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PatientBE GetPatient(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Patient tbPatient = (from patient in db.Patients where patient.active && patient.id == id select patient).FirstOrDefault();

                if (tbPatient == null)
                {
                    throw new Exception("No se encontro paciente.");
                }

                return PatientBE.Create(tbPatient);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PatientBE Login(String dni, String password)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (dni == null || dni.Length != 8 || password == null)
                {
                    throw new Exception("DNI o password invalido.");
                }

                Patient tbPatient = (from patient in db.Patients where patient.active && patient.dni == dni && patient.password == password select patient).FirstOrDefault();

                if (tbPatient == null)
                {
                    throw new Exception("No estas registrado en el sistema.");
                }

                return PatientBE.Create(tbPatient);
            }
            catch(EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
