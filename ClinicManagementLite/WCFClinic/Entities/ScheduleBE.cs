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
        private String days; // MON,TUE,WED,THU,FRI,SAT,SUN
        private Boolean active;
        private DateTime created_at;

        public static ScheduleBE Create(Schedule tbSchedule)
        {
            ScheduleBE objScheduleBE = new ScheduleBE();

            objScheduleBE.Id = Convert.ToInt16(tbSchedule.id);
            objScheduleBE.IdUser = Convert.ToInt16(tbSchedule.id_user);
            objScheduleBE.StartTime = tbSchedule.start_time;
            objScheduleBE.EndTime = tbSchedule.end_time;
            objScheduleBE.Days = tbSchedule.days;
            objScheduleBE.Active = tbSchedule.active;
            objScheduleBE.CreatedAt = tbSchedule.created_at;

            return objScheduleBE;
        }

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
