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
    public class ScheduleBE
    {

        private Int16 id;
        private Int16 id_user;
        private TimeSpan start_time;
        private TimeSpan end_time;
        private String days; // mon, tue, wed, thu, fri, sat, sun
        private Boolean active;
        private DateTime created_at;

        [DataMember]
        public Int16 Id 
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Int16 IdUser 
        {
            get { return id_user; }
            set { id_user = value; }
        }

        [DataMember]
        public TimeSpan StartTime 
        {
            get { return start_time; }
            set { start_time = value; }
        }

        [DataMember]
        public TimeSpan EndTime 
        {
            get { return end_time; }
            set { end_time = value; }
        }

        [DataMember]
        public String Days 
        {
            get { return days; }
            set { days = value; }
        }

        [DataMember]
        public Boolean Active 
        {
            get { return active; }
            set { active = value; }
        }

        [DataMember]
        public DateTime CreatedAt 
        { 
            get { return created_at; }
            set { created_at = value; }
        }
    }
}
