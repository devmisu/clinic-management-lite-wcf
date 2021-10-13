using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;
using WCFClinic.Util;

namespace WCFClinic
{
    public class ServiceMedicalRecord : IServiceMedicalRecord
    {
        public bool CreateMedicalRecord(MedicalRecordBE objMedicalRecordBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (objMedicalRecordBE.IdAppointment == 0 || objMedicalRecordBE.Reason == null || objMedicalRecordBE.Prescription == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                if ((from medicalRecord in db.Medical_Record where medicalRecord.active && medicalRecord.id_appointment == objMedicalRecordBE.IdAppointment select medicalRecord).FirstOrDefault() != null)
                {
                    throw new Exception("Ya existe una historia clinica registrada para esta cita.");
                }

                Medical_Record tbMedicalRecord = new Medical_Record();

                tbMedicalRecord.id_appointment = objMedicalRecordBE.IdAppointment;
                tbMedicalRecord.reason = objMedicalRecordBE.Reason;
                tbMedicalRecord.prescription = objMedicalRecordBE.Prescription;
                tbMedicalRecord.diseases = objMedicalRecordBE.Diseases;
                tbMedicalRecord.allergies = objMedicalRecordBE.Allergies;
                tbMedicalRecord.medicines = objMedicalRecordBE.Medicines;
                tbMedicalRecord.surgeries = objMedicalRecordBE.Surgeries;
                tbMedicalRecord.active = true;
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
                Medical_Record tbMedicalRecord = (from medicalRecord in db.Medical_Record where medicalRecord.active && medicalRecord.id == id select medicalRecord).FirstOrDefault();

                if (tbMedicalRecord == null)
                {
                    throw new Exception("No se encontro historia clinica.");
                }

                tbMedicalRecord.active = false;
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
                Medical_Record tbMedicalRecord = (from medicalRecord in db.Medical_Record where medicalRecord.active && medicalRecord.Appointment.active && medicalRecord.id == objMedicalRecordBE.Id select medicalRecord).FirstOrDefault();

                if (tbMedicalRecord == null)
                {
                    throw new Exception("No se encontro historia clinica.");
                }

                if (tbMedicalRecord.Appointment.state == ((char)AppointmentState.FINISHED).ToString())
                {
                    throw new Exception("La cita ha finalizado, no se puede actualizar historia clinica.");
                }

                if (objMedicalRecordBE.Reason != null) tbMedicalRecord.reason = objMedicalRecordBE.Reason;
                if (objMedicalRecordBE.Prescription != null) tbMedicalRecord.prescription = objMedicalRecordBE.Prescription;
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

                var query = (from medicalRecord in db.Medical_Record orderby medicalRecord.Appointment.date where medicalRecord.active select medicalRecord);

                foreach (var tbMedicalRecord in query)
                {
                    listMedicalRecords.Add(MedicalRecordBE.Create(tbMedicalRecord));
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
                Medical_Record tbMedicalRecord = (from medicalRecord in db.Medical_Record where medicalRecord.active && medicalRecord.id == id select medicalRecord).FirstOrDefault();

                if (tbMedicalRecord == null)
                {
                    throw new Exception("No se encontro historia clinica.");
                }

                return MedicalRecordBE.Create(tbMedicalRecord);
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

                var query = (from medicalRecord in db.Medical_Record orderby medicalRecord.Appointment.date where medicalRecord.active && medicalRecord.Appointment.id_patient == patientId select medicalRecord);

                foreach (var tbMedicalRecord in query)
                {
                    listMedicalRecords.Add(MedicalRecordBE.Create(tbMedicalRecord));
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
