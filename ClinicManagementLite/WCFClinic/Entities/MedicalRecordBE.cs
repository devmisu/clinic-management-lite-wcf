using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFClinic.Entities
{
    [DataContract]
    [Serializable]
    public class MedicalRecordBE
    {
        private Int16 id;
        private Int16 id_appointment;
        private String reason;
        private String prescription;
        private String diseases;
        private String allergies;
        private String medicines;
        private String surgeries;
        private Boolean active;
        private DateTime created_at;

        public static MedicalRecordBE Create(Medical_Record tbMedicalRecord)
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
            objMedicalRecordBE.Active = tbMedicalRecord.active;
            objMedicalRecordBE.CreatedAt = tbMedicalRecord.created_at;

            return objMedicalRecordBE;
        }

        [DataMember]
        public Int16 Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Int16 IdAppointment
        {
            get { return id_appointment; }
            set { id_appointment = value; }
        }

        [DataMember]
        public String Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        [DataMember]
        public String Prescription
        {
            get { return prescription; }
            set { prescription = value; }
        }

        [DataMember]
        public String Diseases
        {
            get { return diseases; }
            set { diseases = value; }
        }

        [DataMember]
        public String Allergies
        {
            get { return allergies; }
            set { allergies = value; }
        }

        [DataMember]
        public String Medicines
        {
            get { return medicines; }
            set { medicines = value; }
        }

        [DataMember]
        public String Surgeries
        {
            get { return surgeries; }
            set { surgeries = value; }
        }

        [DataMember]
        public DateTime CreatedAt
        {
            get { return created_at; }
            set { created_at = value; }
        }

        [DataMember]
        public Boolean Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}
