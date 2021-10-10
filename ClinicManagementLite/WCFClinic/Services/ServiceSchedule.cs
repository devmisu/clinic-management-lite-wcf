using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;

namespace WCFClinic.Services
{
    public class ServiceSchedule : IServiceSchedule
    {
        public bool CreateSchedule(ScheduleBE objScheduleBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Schedule tbSchedule = new Schedule();

                tbSchedule.id_user = objScheduleBE.IdUser;
                tbSchedule.start_time = objScheduleBE.StartTime;
                tbSchedule.end_time = objScheduleBE.EndTime;
                tbSchedule.days = objScheduleBE.Days;
                tbSchedule.active = objScheduleBE.Active;
                tbSchedule.created_at = DateTime.Now;

                db.Schedules.Add(tbSchedule);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteSchedule(short id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Schedule tbSchedule = (from schedule in db.Schedules where schedule.id == id select schedule).FirstOrDefault();

                db.Schedules.Remove(tbSchedule);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateSchedule(ScheduleBE objScheduleBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Schedule tbSchedule = (from schedule in db.Schedules where schedule.id == objScheduleBE.Id select schedule).FirstOrDefault();

                tbSchedule.id_user = objScheduleBE.IdUser;
                tbSchedule.start_time = objScheduleBE.StartTime;
                tbSchedule.end_time = objScheduleBE.EndTime;
                tbSchedule.days = objScheduleBE.Days;
                tbSchedule.active = objScheduleBE.Active;

                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ScheduleBE> GetAllSchedules()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<ScheduleBE> listSchedules = new List<ScheduleBE>();

                var query = (from schedules in db.Schedules orderby schedules.name select schedules);

                foreach (var tbSchedule in query)
                {
                    ScheduleBE objScheduleBE = new ScheduleBE();

                    objScheduleBE.Id = Convert.ToInt16(tbSchedule.id);
                    objScheduleBE.IdUser = Convert.ToInt16(tbSchedule.id_user);
                    objScheduleBE.StartTime = tbSchedule.start_time;
                    objScheduleBE.EndTime = tbSchedule.end_time;
                    objScheduleBE.Days = tbSchedule.days;
                    objScheduleBE.Active = tbSchedule.active;
                    objScheduleBE.CreatedAt = tbSchedule.created_at;

                    listSchedules.Add(objScheduleBE);
                }

                return listSchedules;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ScheduleBE GetOneSchedule(short id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Schedule tbSchedule = (from schedule in db.Schedules where schedule.id == id select schedule).FirstOrDefault();

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
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
