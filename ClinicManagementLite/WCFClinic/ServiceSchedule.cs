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
                if (objScheduleBE.IdUser == 0 || objScheduleBE.StartTime == null || objScheduleBE.EndTime == null || objScheduleBE.Days == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                Schedule tbSchedule = new Schedule();

                tbSchedule.id_user = objScheduleBE.IdUser;
                tbSchedule.start_time = objScheduleBE.StartTime;
                tbSchedule.end_time = objScheduleBE.EndTime;
                tbSchedule.days = objScheduleBE.Days;
                tbSchedule.active = true;
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
                Schedule tbSchedule = (from schedule in db.Schedules where schedule.active && schedule.id == id select schedule).FirstOrDefault();

                if (tbSchedule == null)
                {
                    throw new Exception("No se encontro horario.");
                }

                tbSchedule.active = false;
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
                Schedule tbSchedule = (from schedule in db.Schedules where schedule.active && schedule.id == objScheduleBE.Id select schedule).FirstOrDefault();

                if (tbSchedule == null)
                {
                    throw new Exception("No se encontro horario.");
                }

                if (objScheduleBE.StartTime != null) tbSchedule.start_time = objScheduleBE.StartTime;
                if (objScheduleBE.EndTime != null) tbSchedule.end_time = objScheduleBE.EndTime;
                if (objScheduleBE.Days != null) tbSchedule.days = objScheduleBE.Days;

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

                var query = (from schedules in db.Schedules where schedules.active select schedules);

                foreach (var tbSchedule in query)
                {
                    listSchedules.Add(ScheduleBE.Create(tbSchedule));
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
                Schedule tbSchedule = (from schedule in db.Schedules where schedule.active && schedule.id == id select schedule).FirstOrDefault();

                if (tbSchedule == null)
                {
                    throw new Exception("No se encontro horario.");
                }

                return ScheduleBE.Create(tbSchedule);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ScheduleBE> GetAllSchedulesOfUser(Int16 id, DateTime date)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                /*List<ScheduleBE> listSchedules = new List<ScheduleBE>();

                var query = (from schedules in db.Schedules where schedules.active && schedules.User.active && schedules.id_user == id select schedules);

                foreach (var tbSchedule in query)
                {
                    listSchedules.Add(ScheduleBE.Create(tbSchedule));
                }

                return listSchedules;*/

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

                List<ScheduleBE> listSchedules = new List<ScheduleBE>();

                var query = (from schedules in db.Schedules where schedules.active && schedules.id_user == id && schedules.days.Contains(day) select schedules);

                foreach (var tbSchedule in query)
                {
                    for (int hour = tbSchedule.start_time.Hours; hour < tbSchedule.end_time.Hours; hour++)
                    {
                        ScheduleBE objScheduleBE = new ScheduleBE();

                        objScheduleBE.StartTime = new TimeSpan(hour, 0, 0);
                        objScheduleBE.EndTime = new TimeSpan(hour + 1, 0, 0);

                        listSchedules.Add(objScheduleBE);
                    }
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

                var query = (from schedules in db.Schedules where schedules.active && schedules.id_user == userId && schedules.days.Contains(day) select schedules);

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
