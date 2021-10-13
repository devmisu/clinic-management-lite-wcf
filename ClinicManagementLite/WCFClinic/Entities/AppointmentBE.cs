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
    public class AppointmentBE
    {

        private Int16 id;
        private Int16 id_patient;
        private Int16 id_user;
        private String cancellation_reason;
        private DateTime date;
        private TimeSpan start_hour;
        private TimeSpan end_hour;
        private Nullable<TimeSpan> arrival_hour;
        private Nullable<TimeSpan> departure_hour;
        private String state;
        private DateTime created_at;

        [DataMember]
        public Int16 Id 
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Int16 IdPatient
        {
            get { return id_patient; }
            set { id_patient = value; }
        }

        [DataMember]
        public Int16 IdUser
        {
            get { return id_user; }
            set { id_user = value; }
        }

        [DataMember]
        public String CancellationReason
        {
            get { return cancellation_reason; }
            set { cancellation_reason = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        [DataMember]
        public TimeSpan StartHour 
        {
            get { return start_hour; }
            set { start_hour = value; }
        }

        [DataMember]
        public TimeSpan EndHour
        {
            get { return end_hour; }
            set { end_hour = value; }
        }

        [DataMember]
        public Nullable<TimeSpan> ArrivalHour
        {
            get { return arrival_hour; }
            set { arrival_hour = value; }
        }

        [DataMember]
        public Nullable<TimeSpan> DepartureHour
        {
            get { return departure_hour; }
            set { departure_hour = value; }
        }

        [DataMember]
        public String State
        {
            get { return state; }
            set { state = value; }
        }

        [DataMember]
        public DateTime CreatedAt 
        { 
            get { return created_at; }
            set { created_at = value; }
        }
    }
}
