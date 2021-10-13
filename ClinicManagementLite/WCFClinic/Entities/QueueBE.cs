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
        private Boolean active;

        public static QueueBE Create(Queue tbQueue)
        {
            QueueBE objQueueBE = new QueueBE();

            objQueueBE.Id = Convert.ToInt16(tbQueue.id);
            objQueueBE.IdPatient = Convert.ToInt16(tbQueue.id_patient);
            objQueueBE.IdUser = Convert.ToInt16(tbQueue.id_user);
            objQueueBE.StartDate = tbQueue.start_date;
            objQueueBE.StartTime = tbQueue.start_time;
            objQueueBE.State = tbQueue.state;
            objQueueBE.CreatedAt = tbQueue.created_at;
            objQueueBE.Active = tbQueue.active;

            return objQueueBE;
        }

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

        [DataMember]
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}
