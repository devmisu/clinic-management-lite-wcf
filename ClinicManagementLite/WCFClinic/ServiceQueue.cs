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
    public class ServiceQueue : IServiceQueue
    {
        public bool CreateQueue(QueueBE objQueueBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (objQueueBE.IdPatient == 0 || objQueueBE.IdUser == 0 || objQueueBE.StartDate == null || objQueueBE.StartTime == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                if ((from queue in db.Queues where queue.active && queue.start_date == objQueueBE.StartDate && queue.start_time == objQueueBE.StartTime select queue).FirstOrDefault() != null)
                {
                    throw new Exception("Ya existe un cita en cola registrada.");
                }

                Queue tbQueue = new Queue();

                tbQueue.id_patient = objQueueBE.IdPatient;
                tbQueue.id_user = objQueueBE.IdUser;
                tbQueue.start_date = objQueueBE.StartDate;
                tbQueue.start_time = objQueueBE.StartTime;
                tbQueue.state = ((char)QueueState.REQUESTED).ToString();
                tbQueue.active = true;
                tbQueue.created_at = DateTime.Now;

                db.Queues.Add(tbQueue);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteQueue(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Queue tbQueue = (from queue in db.Queues where queue.active && queue.id == id select queue).FirstOrDefault();

                if (tbQueue == null)
                {
                    throw new Exception("No se encontro cita en cola.");
                }

                tbQueue.active = false;
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateQueue(QueueBE objQueueBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                var listStates = new[] { (char)QueueState.ACCEPTED, (char)QueueState.CANCELLED, (char)QueueState.REJECTED };
                
                if (!listStates.Contains(Convert.ToChar(objQueueBE.State)))
                {
                    throw new Exception("Estado invalido.");
                }

                Queue tbQueue = (from queue in db.Queues where queue.active && queue.id == objQueueBE.Id select queue).FirstOrDefault();

                if (tbQueue == null)
                {
                    throw new Exception("No se encontro cita en cola.");
                }

                if (listStates.Contains(Convert.ToChar(tbQueue.state)))
                {
                    throw new Exception("No se puede actualizar, la cita en cola ya tiene un estado final.");
                }

                if (objQueueBE.State == ((char)QueueState.ACCEPTED).ToString())
                {
                    AppointmentBE objAppointmentBE = new AppointmentBE();

                    objAppointmentBE.IdPatient = Convert.ToInt16(tbQueue.id_patient);
                    objAppointmentBE.IdUser = Convert.ToInt16(tbQueue.id_user);
                    objAppointmentBE.Date = tbQueue.start_date;
                    objAppointmentBE.StartHour = tbQueue.start_time;
                    objAppointmentBE.EndHour = new TimeSpan(tbQueue.start_time.Hours + 1, 0, 0);

                    new ServiceAppointment().CreateAppointment(objAppointmentBE);
                }

                tbQueue.state = objQueueBE.State;

                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<QueueBE> GetAllQueues()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<QueueBE> listQueues = new List<QueueBE>();

                var query = (from queues in db.Queues orderby queues.start_date where queues.active select queues);

                foreach (var tbQueue in query)
                {
                    listQueues.Add(QueueBE.Create(tbQueue));
                }

                return listQueues;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public QueueBE GetOneQueue(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Queue tbQueue = (from queue in db.Queues where queue.active && queue.id == id select queue).FirstOrDefault();

                if (tbQueue == null)
                {
                    throw new Exception("No se encontro cita en cola.");
                }

                return QueueBE.Create(tbQueue);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<QueueBE> GetUserQueues(Int16 userId)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<QueueBE> listQueues = new List<QueueBE>();

                var query = (from queues in db.Queues orderby queues.start_date where queues.active && queues.User.active && queues.User.id == userId select queues);

                foreach (var tbQueue in query)
                {
                    listQueues.Add(QueueBE.Create(tbQueue));
                }

                return listQueues;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<QueueBE> GetPatientQueues(Int16 patientId)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<QueueBE> listQueues = new List<QueueBE>();

                var query = (from queues in db.Queues orderby queues.start_date where queues.active && queues.User.active && queues.Patient.id == patientId select queues);

                foreach (var tbQueue in query)
                {
                    listQueues.Add(QueueBE.Create(tbQueue));
                }

                return listQueues;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
