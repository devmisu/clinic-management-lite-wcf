using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;

namespace WCFClinic
{
    public class ServiceUser : IServiceUser
    {
        public bool CreateUser(UserBE objUserBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (objUserBE.IdRole == 0 ||
                    objUserBE.IdArea == 0 ||
                    objUserBE.FirstName == null ||
                    objUserBE.LastName == null ||
                    objUserBE.Phone == null || objUserBE.Phone.Length != 9 ||
                    objUserBE.Email == null ||
                    objUserBE.Dni == null || objUserBE.Dni.Length != 8 ||
                    objUserBE.Password == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                if ((from user in db.Users where user.active && user.dni == objUserBE.Dni select user).FirstOrDefault() != null)
                {
                    throw new Exception("Ya existe un usuario registrado con ese DNI.");
                }

                User tbUser = new User();

                tbUser.id_role = objUserBE.IdRole;
                tbUser.id_area = objUserBE.IdArea;
                tbUser.first_name = objUserBE.FirstName;
                tbUser.last_name = objUserBE.LastName;
                tbUser.phone = objUserBE.Phone;
                tbUser.photo = objUserBE.Photo;
                tbUser.email = objUserBE.Email;
                tbUser.dni = objUserBE.Dni;
                tbUser.password = objUserBE.Password;
                tbUser.specialization = objUserBE.Specialization;
                tbUser.active = true;
                tbUser.created_at = DateTime.Now;

                db.Users.Add(tbUser);
                db.SaveChanges();

                return true;
            }
            catch(EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteUser(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                User tbUser = (from user in db.Users where user.active && user.id == id select user).FirstOrDefault();

                if (tbUser == null)
                {
                    throw new Exception("No se encontro usuario.");
                }

                if (new ServiceAppointment().GetUserAppointments(id).Count != 0)
                {
                    throw new Exception("No se puede eliminar, hay citas asignadas al usuario.");
                }

                if ((from queue in db.Queues where queue.active && queue.User.active && queue.User.id == id select queue).Count() != 0)
                {
                    throw new Exception("No se puede eliminar, hay citas en cola asignadas al usuario.");
                }

                if ((from schedule in db.Schedules where schedule.active && schedule.User.active && schedule.User.id == id select schedule).Count() != 0)
                {
                    throw new Exception("No se puede eliminar, hay horarios asignados al usuario.");
                }

                tbUser.active = false;
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateUser(UserBE objUserBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                User tbUser = (from user in db.Users where user.active && user.id == objUserBE.Id select user).FirstOrDefault();

                if (tbUser == null)
                {
                    throw new Exception("No se encontro usuario.");
                }

                if (objUserBE.IdRole != 0) tbUser.id_role = objUserBE.IdRole;
                if (objUserBE.IdArea != 0) tbUser.id_area = objUserBE.IdArea;
                if (objUserBE.FirstName != null) tbUser.first_name = objUserBE.FirstName;
                if (objUserBE.LastName != null) tbUser.last_name = objUserBE.LastName;
                if (objUserBE.Password != null) tbUser.password = objUserBE.Password;
                if (objUserBE.Email != null) tbUser.email = objUserBE.Email;
                if (objUserBE.Phone != null && objUserBE.Phone.Length == 9) tbUser.phone = objUserBE.Phone;
                tbUser.photo = objUserBE.Photo;
                tbUser.specialization = objUserBE.Specialization;

                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserBE> GetAllUsers()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<UserBE> listUsers = new List<UserBE>();

                var query = (from user in db.Users orderby user.first_name where user.active select user);

                foreach (var tbUser in query)
                {
                    listUsers.Add(UserBE.Create(tbUser));
                }

                return listUsers;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserBE GetUser(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                User tbUser = (from user in db.Users where user.active && user.id == id select user).FirstOrDefault();

                if (tbUser == null)
                {
                    throw new Exception("No se encontro usuario.");
                }

                return UserBE.Create(tbUser);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserBE> GetUsersByArea(Int16 areaId)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<UserBE> listUsers = new List<UserBE>();

                var query = (from user in db.Users orderby user.first_name where user.active && user.Area.active && user.Area.id == areaId select user);

                foreach (var tbUser in query)
                {
                    listUsers.Add(UserBE.Create(tbUser));
                }

                return listUsers;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserBE Login(String dni, String password)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (dni == null || dni.Length != 8 || password == null)
                {
                    throw new Exception("DNI o password invalido.");
                }

                User tbUser = (from user in db.Users where user.active && user.dni == dni && user.password == password select user).FirstOrDefault();

                if (tbUser == null)
                {
                    throw new Exception("No estas registrado en el sistema.");
                }

                return UserBE.Create(tbUser);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
