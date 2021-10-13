using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFClinic.Entities
{
    public class UserBE
    {
        private Int16 id;
        private Int16 id_role;
        private Int16 id_area;
        private String first_name;
        private String last_name;
        private String phone;
        private String photo;
        private String email;
        private String dni;
        private String password;
        private String specialization;
        private Boolean active;
        private DateTime created_at;

        public static UserBE Create(User tbUser)
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

            return objUserBE;
        }

        public Int16 Id 
        {
            get { return id; }
            set { id = value; }
        }
        public Int16 IdRole 
        {
            get { return id_role; }
            set { id_role = value; }
        }
        public Int16 IdArea 
        {
            get { return id_area; }
            set { id_area = value; }
        }
        public String FirstName 
        {
            get { return first_name; }
            set { first_name = value; }
        }
        public String LastName 
        {
            get { return last_name; }
            set { last_name = value; }
        }
        public String Phone 
        {
            get { return phone; }
            set { phone = value; }
        }
        public String Photo 
        {
            get { return photo; }
            set { photo = value; }
        }
        public String Email 
        {
            get { return email; }
            set { email = value; }
        }
        public String Dni 
        {
            get { return dni; }
            set { dni = value; }
        }
        public String Password 
        {
            get { return password; }
            set { password = value; }
        }
        public String Specialization 
        {
            get { return specialization; }
            set { specialization = value; }
        }
        public Boolean Active 
        {
            get { return active; }
            set { active = value; }
        }
        public DateTime CreatedAt 
        {
            get { return created_at; }
            set { created_at = value; }
        }
    }
}
