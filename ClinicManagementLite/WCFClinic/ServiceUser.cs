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
                tbUser.active = objUserBE.Active;
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
                User tbUser = (from user in db.Users where user.id == id select user).FirstOrDefault();

                db.Users.Remove(tbUser);
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
                User tbUser = (from user in db.Users where user.id == objUserBE.Id select user).FirstOrDefault();

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
                tbUser.active = objUserBE.Active;

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

                var query = (from user in db.Users orderby user.last_name select user);

                foreach (var tbUser in query)
                {
                    UserBE objUserBE = new UserBE();

                    objUserBE.Id = Convert.ToInt16(tbUser.id);
                    objUserBE.IdRole = Convert.ToInt16(tbUser.id_role);
                    objUserBE.IdArea = Convert.ToInt16(tbUser.id_area);
                    objUserBE.FirstName = tbUser.first_name;
                    objUserBE.LastName = tbUser.last_name;
                    objUserBE.Phone = tbUser.phone;
                    objUserBE.Photo = tbUser.photo;
                    objUserBE.Email = tbUser.email;
                    objUserBE.Dni = tbUser.dni;
                    objUserBE.Password = tbUser.password;
                    objUserBE.Specialization = tbUser.specialization;
                    objUserBE.Active = tbUser.active;
                    objUserBE.CreatedAt = tbUser.created_at;

                    listUsers.Add(objUserBE);
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
                User tbUser = (from user in db.Users where user.id == id select user).FirstOrDefault();

                UserBE objUserBE = new UserBE();

                objUserBE.Id = Convert.ToInt16(tbUser.id);
                objUserBE.IdRole = Convert.ToInt16(tbUser.id_role);
                objUserBE.IdArea = Convert.ToInt16(tbUser.id_area);
                objUserBE.FirstName = tbUser.first_name;
                objUserBE.LastName = tbUser.last_name;
                objUserBE.Phone = tbUser.phone;
                objUserBE.Photo = tbUser.photo;
                objUserBE.Email = tbUser.email;
                objUserBE.Dni = tbUser.dni;
                objUserBE.Password = tbUser.password;
                objUserBE.Specialization = tbUser.specialization;
                objUserBE.Active = tbUser.active;
                objUserBE.CreatedAt = tbUser.created_at;

                return objUserBE;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
