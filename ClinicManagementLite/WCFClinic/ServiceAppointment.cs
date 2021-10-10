using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;

namespace WCFClinic
{
    public class ServiceAppointment : IServiceAppointment
    {
        public bool CreateAppointment(AppointmentBE objAppointmentBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Appointment tbAppointment = new Appointment();

                tbAppointment.id_patient = objAppointmentBE.IdPatient;
                tbAppointment.id_user = objAppointmentBE.IdUser;
                tbAppointment.cancellation_reason = objAppointmentBE.CancellationReason;
                tbAppointment.date = objAppointmentBE.Date;
                tbAppointment.start_hour = objAppointmentBE.StartHour;
                tbAppointment.end_hour = objAppointmentBE.EndHour;
                tbAppointment.arrival_hour = objAppointmentBE.ArrivalHour;
                tbAppointment.departure_hour = objAppointmentBE.DepartureHour;
                tbAppointment.state = objAppointmentBE.State;
                tbAppointment.created_at = DateTime.Now;

                db.Appointments.Add(tbAppointment);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteAppointment(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Appointment tbAppointment = (from schedule in db.Appointments where schedule.id == id select schedule).FirstOrDefault();

                db.Appointments.Remove(tbAppointment);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateAppointment(AppointmentBE objAppointmentBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Appointment tbAppointment = (from schedule in db.Appointments where schedule.id == objAppointmentBE.Id select schedule).FirstOrDefault();

                tbAppointment.id_patient = objAppointmentBE.IdPatient;
                tbAppointment.id_user = objAppointmentBE.IdUser;
                tbAppointment.cancellation_reason = objAppointmentBE.CancellationReason;
                tbAppointment.date = objAppointmentBE.Date;
                tbAppointment.start_hour = objAppointmentBE.StartHour;
                tbAppointment.end_hour = objAppointmentBE.EndHour;
                tbAppointment.arrival_hour = objAppointmentBE.ArrivalHour;
                tbAppointment.departure_hour = objAppointmentBE.DepartureHour;
                tbAppointment.state = objAppointmentBE.State;

                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AppointmentBE> GetAllAppointments()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<AppointmentBE> listAppointments = new List<AppointmentBE>();

                var query = (from schedules in db.Appointments orderby schedules.id select schedules);

                foreach (var tbAppointment in query)
                {
                    AppointmentBE objAppointmentBE = new AppointmentBE();

                    objAppointmentBE.Id = Convert.ToInt16(tbAppointment.id);
                    objAppointmentBE.IdPatient = Convert.ToInt16(tbAppointment.id_patient);
                    objAppointmentBE.IdUser = Convert.ToInt16(tbAppointment.id_user);
                    objAppointmentBE.CancellationReason = tbAppointment.cancellation_reason;
                    objAppointmentBE.Date = tbAppointment.date;
                    objAppointmentBE.StartHour = tbAppointment.start_hour;
                    objAppointmentBE.EndHour = tbAppointment.end_hour;
                    objAppointmentBE.ArrivalHour = (TimeSpan)tbAppointment.arrival_hour; //Validar
                    objAppointmentBE.DepartureHour = (TimeSpan)tbAppointment.departure_hour; //Validar
                    objAppointmentBE.State = tbAppointment.state;
                    objAppointmentBE.CreatedAt = tbAppointment.created_at;

                    listAppointments.Add(objAppointmentBE);
                }

                return listAppointments;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AppointmentBE GetOneAppointment(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Appointment tbAppointment = (from schedule in db.Appointments where schedule.id == id select schedule).FirstOrDefault();

                AppointmentBE objAppointmentBE = new AppointmentBE();

                objAppointmentBE.Id = Convert.ToInt16(tbAppointment.id);
                objAppointmentBE.IdPatient = Convert.ToInt16(tbAppointment.id_patient);
                objAppointmentBE.IdUser = Convert.ToInt16(tbAppointment.id_user);
                objAppointmentBE.CancellationReason = tbAppointment.cancellation_reason;
                objAppointmentBE.Date = tbAppointment.date;
                objAppointmentBE.StartHour = tbAppointment.start_hour;
                objAppointmentBE.EndHour = tbAppointment.end_hour;
                objAppointmentBE.ArrivalHour = (TimeSpan)tbAppointment.arrival_hour; //Validar
                objAppointmentBE.DepartureHour = (TimeSpan)tbAppointment.departure_hour; //Validar
                objAppointmentBE.State = tbAppointment.state;
                objAppointmentBE.CreatedAt = tbAppointment.created_at;

                return objAppointmentBE;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
