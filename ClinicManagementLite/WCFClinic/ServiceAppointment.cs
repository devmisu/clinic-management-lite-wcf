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
    public class ServiceAppointment : IServiceAppointment
    {
        public bool CreateAppointment(AppointmentBE objAppointmentBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (objAppointmentBE.IdPatient == 0 || objAppointmentBE.IdUser == 0 || objAppointmentBE.Date == null || objAppointmentBE.StartHour == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                if ((from appointment in db.Appointments where appointment.active && appointment.id_patient == objAppointmentBE.IdPatient && appointment.id_user == objAppointmentBE.IdUser &&
                     appointment.date == objAppointmentBE.Date && appointment.start_hour == objAppointmentBE.StartHour select appointment).FirstOrDefault() != null)
                {
                    throw new Exception("Ya existe un cita registrada.");
                }

                Appointment tbAppointment = new Appointment();

                tbAppointment.id_patient = objAppointmentBE.IdPatient;
                tbAppointment.id_user = objAppointmentBE.IdUser;
                tbAppointment.date = objAppointmentBE.Date;
                tbAppointment.start_hour = objAppointmentBE.StartHour;
                tbAppointment.end_hour = new TimeSpan(objAppointmentBE.StartHour.Hours + 1, 0, 0);
                tbAppointment.state = ((char)AppointmentState.STARTED).ToString();
                tbAppointment.active = true;
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
                Appointment tbAppointment = (from appointment in db.Appointments where appointment.active && appointment.id == id select appointment).FirstOrDefault();

                if (tbAppointment == null)
                {
                    throw new Exception("No se encontro cita.");
                }

                if ((from medicalRecord in db.Medical_Record where medicalRecord.active && medicalRecord.Appointment.active && medicalRecord.Appointment.id == id select medicalRecord).Count() != 0)
                {
                    throw new Exception("No se puede eliminar, hay historias clinicas asignadas a la cita.");
                }

                tbAppointment.active = false;
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
                if (objAppointmentBE.State == ((char)AppointmentState.STARTED).ToString())
                {
                    throw new Exception("Estado invalido.");
                }

                Appointment tbAppointment = (from appointment in db.Appointments where appointment.active && appointment.id == objAppointmentBE.Id select appointment).FirstOrDefault();

                if (tbAppointment == null)
                {
                    throw new Exception("No se encontro cita.");
                }

                if (tbAppointment.state == ((char)AppointmentState.FINISHED).ToString())
                {
                    throw new Exception("La cita ha finalizado, no se puede actualizar.");
                }

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

                var query = (from appointments in db.Appointments orderby appointments.date where appointments.active select appointments);

                foreach (var tbAppointment in query)
                {
                    listAppointments.Add(AppointmentBE.Create(tbAppointment));
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
                Appointment tbAppointment = (from appointment in db.Appointments where appointment.active && appointment.id == id select appointment).FirstOrDefault();

                if (tbAppointment == null)
                {
                    throw new Exception("No se encontro cita.");
                }

                return AppointmentBE.Create(tbAppointment);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AppointmentBE> GetPatientAppointments(Int16 patientId)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<AppointmentBE> listAppointments = new List<AppointmentBE>();

                var query = (from appointments in db.Appointments orderby appointments.date where appointments.active && appointments.id_patient == patientId select appointments);

                foreach (var tbAppointment in query)
                {
                    listAppointments.Add(AppointmentBE.Create(tbAppointment));
                }

                return listAppointments;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AppointmentBE> GetUserAppointments(Int16 userId)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<AppointmentBE> listAppointments = new List<AppointmentBE>();

                var query = (from appointments in db.Appointments orderby appointments.date where appointments.active && appointments.id_user == userId select appointments);

                foreach (var tbAppointment in query)
                {
                    listAppointments.Add(AppointmentBE.Create(tbAppointment));
                }

                return listAppointments;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AppointmentBE> GetUserAppointmentsByDate(Int16 userId, DateTime date)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<AppointmentBE> listAppointments = new List<AppointmentBE>();

                var query = (from appointments in db.Appointments where appointments.active && appointments.id_user == userId && appointments.date == date select appointments);

                foreach (var tbAppointment in query)
                {
                    listAppointments.Add(AppointmentBE.Create(tbAppointment));
                }

                return listAppointments;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
