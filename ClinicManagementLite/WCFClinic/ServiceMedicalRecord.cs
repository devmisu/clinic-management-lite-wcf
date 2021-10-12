using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;

namespace WCFClinic
{
    public class ServiceMedicalRecord : IServiceMedicalRecord
    {
        public bool CreateMedicalRecord(MedicalRecordBE objMedicalRecordBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Medical_Record tbMedicalRecord = new Medical_Record();

                tbMedicalRecord.id_appointment = objMedicalRecordBE.IdAppointment;
                tbMedicalRecord.reason = objMedicalRecordBE.Reason;
                tbMedicalRecord.prescription = objMedicalRecordBE.Prescription;
                tbMedicalRecord.diseases = objMedicalRecordBE.Diseases;
                tbMedicalRecord.allergies = objMedicalRecordBE.Allergies;
                tbMedicalRecord.medicines = objMedicalRecordBE.Medicines;
                tbMedicalRecord.surgeries = objMedicalRecordBE.Surgeries;
                tbMedicalRecord.created_at = DateTime.Now;

                db.Medical_Record.Add(tbMedicalRecord);
                db.SaveChanges();

                return true;
            }
            catch(EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteMedicalRecord(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Medical_Record tbMedicalRecord = (from medicalRecord in db.Medical_Record where medicalRecord.id == id select medicalRecord).FirstOrDefault();

                db.Medical_Record.Remove(tbMedicalRecord);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMedicalRecord(MedicalRecordBE objMedicalRecordBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Medical_Record tbMedicalRecord = (from medicalRecord in db.Medical_Record where medicalRecord.id == objMedicalRecordBE.Id select medicalRecord).FirstOrDefault();

                tbMedicalRecord.id_appointment = objMedicalRecordBE.IdAppointment;
                tbMedicalRecord.reason = objMedicalRecordBE.Reason;
                tbMedicalRecord.prescription = objMedicalRecordBE.Prescription;
                tbMedicalRecord.diseases = objMedicalRecordBE.Diseases;
                tbMedicalRecord.allergies = objMedicalRecordBE.Allergies;
                tbMedicalRecord.medicines = objMedicalRecordBE.Medicines;
                tbMedicalRecord.surgeries = objMedicalRecordBE.Surgeries;

                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MedicalRecordBE> GetAllMedicalRecords()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<MedicalRecordBE> listMedicalRecords = new List<MedicalRecordBE>();

                var query = (from medicalRecord in db.Medical_Record orderby medicalRecord.id select medicalRecord);

                foreach (var tbMedicalRecord in query)
                {
                    MedicalRecordBE objMedicalRecordBE = new MedicalRecordBE();

                    objMedicalRecordBE.Id = Convert.ToInt16(tbMedicalRecord.id);
                    objMedicalRecordBE.IdAppointment = Convert.ToInt16(tbMedicalRecord.id_appointment);
                    objMedicalRecordBE.Reason = tbMedicalRecord.reason;
                    objMedicalRecordBE.Prescription = tbMedicalRecord.prescription;
                    objMedicalRecordBE.Diseases = tbMedicalRecord.diseases;
                    objMedicalRecordBE.Allergies = tbMedicalRecord.allergies;
                    objMedicalRecordBE.Medicines = tbMedicalRecord.medicines;
                    objMedicalRecordBE.Surgeries = tbMedicalRecord.surgeries;
                    objMedicalRecordBE.CreatedAt = tbMedicalRecord.created_at;

                    listMedicalRecords.Add(objMedicalRecordBE);
                }

                return listMedicalRecords;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MedicalRecordBE GetMedicalRecord(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Medical_Record tbMedicalRecord = (from medicalRecord in db.Medical_Record where medicalRecord.id == id select medicalRecord).FirstOrDefault();

                MedicalRecordBE objMedicalRecordBE = new MedicalRecordBE();

                objMedicalRecordBE.Id = Convert.ToInt16(tbMedicalRecord.id);
                objMedicalRecordBE.IdAppointment = Convert.ToInt16(tbMedicalRecord.id_appointment);
                objMedicalRecordBE.Reason = tbMedicalRecord.reason;
                objMedicalRecordBE.Prescription = tbMedicalRecord.prescription;
                objMedicalRecordBE.Diseases = tbMedicalRecord.diseases;
                objMedicalRecordBE.Allergies = tbMedicalRecord.allergies;
                objMedicalRecordBE.Medicines = tbMedicalRecord.medicines;
                objMedicalRecordBE.Surgeries = tbMedicalRecord.surgeries;
                objMedicalRecordBE.CreatedAt = tbMedicalRecord.created_at;

                return objMedicalRecordBE;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MedicalRecordBE> GetPatientMedicalRecords(Int16 patientId)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<MedicalRecordBE> listMedicalRecords = new List<MedicalRecordBE>();

                var query = (from medicalRecord in db.Medical_Record orderby medicalRecord.created_at where medicalRecord.Appointment.id_patient == patientId select medicalRecord);

                foreach (var tbMedicalRecord in query)
                {
                    MedicalRecordBE objMedicalRecordBE = new MedicalRecordBE();

                    objMedicalRecordBE.Id = Convert.ToInt16(tbMedicalRecord.id);
                    objMedicalRecordBE.IdAppointment = Convert.ToInt16(tbMedicalRecord.id_appointment);
                    objMedicalRecordBE.Reason = tbMedicalRecord.reason;
                    objMedicalRecordBE.Prescription = tbMedicalRecord.prescription;
                    objMedicalRecordBE.Diseases = tbMedicalRecord.diseases;
                    objMedicalRecordBE.Allergies = tbMedicalRecord.allergies;
                    objMedicalRecordBE.Medicines = tbMedicalRecord.medicines;
                    objMedicalRecordBE.Surgeries = tbMedicalRecord.surgeries;
                    objMedicalRecordBE.CreatedAt = tbMedicalRecord.created_at;

                    listMedicalRecords.Add(objMedicalRecordBE);
                }

                return listMedicalRecords;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
