using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;

namespace WCFClinic
{
    public class ServiceQueue : IServiceQueue
    {
        public bool CreateQueue(QueueBE objQueueBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Queue tbQueue = new Queue();

                tbQueue.id_patient = objQueueBE.IdPatient;
                tbQueue.id_user = objQueueBE.IdUser;
                tbQueue.start_date = objQueueBE.StartDate;
                tbQueue.start_time = objQueueBE.StartTime;
                tbQueue.state = objQueueBE.State;
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
                Queue tbQueue = (from queue in db.Queues where queue.id == id select queue).FirstOrDefault();

                db.Queues.Remove(tbQueue);
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
                Queue tbQueue = (from queue in db.Queues where queue.id == objQueueBE.Id select queue).FirstOrDefault();

                tbQueue.id_patient = objQueueBE.IdPatient;
                tbQueue.id_user = objQueueBE.IdUser;
                tbQueue.start_date = objQueueBE.StartDate;
                tbQueue.start_time = objQueueBE.StartTime;
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

                var query = (from queues in db.Queues orderby queues.id select queues);

                foreach (var tbQueue in query)
                {
                    QueueBE objQueueBE = new QueueBE();

                    objQueueBE.Id = Convert.ToInt16(tbQueue.id);
                    objQueueBE.IdPatient = Convert.ToInt16(tbQueue.id_patient);
                    objQueueBE.IdUser = Convert.ToInt16(tbQueue.id_user);
                    objQueueBE.StartDate = tbQueue.start_date;
                    objQueueBE.StartTime = tbQueue.start_time;
                    objQueueBE.State = tbQueue.state;
                    objQueueBE.CreatedAt = tbQueue.created_at;

                    listQueues.Add(objQueueBE);
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
                Queue tbQueue = (from queue in db.Queues where queue.id == id select queue).FirstOrDefault();

                QueueBE objQueueBE = new QueueBE();

                objQueueBE.Id = Convert.ToInt16(tbQueue.id);
                objQueueBE.IdPatient = Convert.ToInt16(tbQueue.id_patient);
                objQueueBE.IdUser = Convert.ToInt16(tbQueue.id_user);
                objQueueBE.StartDate = tbQueue.start_date;
                objQueueBE.StartTime = tbQueue.start_time;
                objQueueBE.State = tbQueue.state;
                objQueueBE.CreatedAt = tbQueue.created_at;

                return objQueueBE;
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

                var query = (from queues in db.Queues where queues.id_user == userId orderby queues.id select queues);

                foreach (var tbQueue in query)
                {
                    QueueBE objQueueBE = new QueueBE();

                    objQueueBE.Id = Convert.ToInt16(tbQueue.id);
                    objQueueBE.IdPatient = Convert.ToInt16(tbQueue.id_patient);
                    objQueueBE.IdUser = Convert.ToInt16(tbQueue.id_user);
                    objQueueBE.StartDate = tbQueue.start_date;
                    objQueueBE.StartTime = tbQueue.start_time;
                    objQueueBE.State = tbQueue.state;
                    objQueueBE.CreatedAt = tbQueue.created_at;

                    listQueues.Add(objQueueBE);
                }

                if (listQueues.Count == 0)
                {
                    throw new Exception("No se encontraron solicitudes de citas.");
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
