using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;

namespace WCFClinic
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

        public bool DeleteSchedule(Int16 id)
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

                var query = (from schedules in db.Schedules orderby schedules.id select schedules);

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

        public ScheduleBE GetOneSchedule(Int16 id)
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

        public List<ScheduleBE> GetAllSchedulesOfUser(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<ScheduleBE> listSchedules = new List<ScheduleBE>();

                var query = (from schedules in db.Schedules where schedules.id_user == id orderby schedules.id select schedules);

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

                if (listSchedules.Count == 0)
                {
                    throw new Exception("No se encontraron horarios.");
                }

                return listSchedules;

            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ScheduleBE> GetAvailableSchedulesByUser(Int16 userId, DateTime date)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                var day = "";

                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        day = "MON";
                        break;
                    case DayOfWeek.Tuesday:
                        day = "TUE";
                        break;
                    case DayOfWeek.Wednesday:
                        day = "WED";
                        break;
                    case DayOfWeek.Thursday:
                        day = "THU";
                        break;
                    case DayOfWeek.Friday:
                        day = "FRI";
                        break;
                    case DayOfWeek.Saturday:
                        day = "SAT";
                        break;
                    case DayOfWeek.Sunday:
                        day = "SUN";
                        break;
                }

                // Obtener citas del dia seleccionado
                List<AppointmentBE> listAppointments = new ServiceAppointment().GetUserAppointmentsByDate(userId, date);

                // Obtener horas ocupadas por citas
                List<int> appointmentsHours = (from appointment in listAppointments select appointment.StartHour.Hours).ToList();

                // Obtener horarios del dia seleccionado
                List<ScheduleBE> listSchedules = new List<ScheduleBE>();

                var query = (from schedules in db.Schedules where schedules.id_user == userId && schedules.days.Contains(day) select schedules);

                foreach (var tbSchedule in query)
                {
                    // Crear horarios con rango de horas
                    for (int hour = tbSchedule.start_time.Hours; hour < tbSchedule.end_time.Hours; hour++)
                    {
                        // Filtrar horarios disponibles
                        if (!appointmentsHours.Contains(hour))
                        {
                            ScheduleBE objScheduleBE = new ScheduleBE();

                            objScheduleBE.StartTime = new TimeSpan(hour, 0, 0);
                            objScheduleBE.EndTime = new TimeSpan(hour + 1, 0, 0);

                            listSchedules.Add(objScheduleBE);
                        }
                    }
                }

                return listSchedules;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
