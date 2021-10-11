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
    public class QueueBE
    {

        private Int16 id;
        private Int16 id_patient;
        private Int16 id_user;
        private DateTime start_date;
        private TimeSpan start_time;
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
        public DateTime StartDate
        {
            get { return start_date; }
            set { start_date = value; }
        }

        [DataMember]
        public TimeSpan StartTime
        {
            get { return start_time; }
            set { start_time = value; }
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
