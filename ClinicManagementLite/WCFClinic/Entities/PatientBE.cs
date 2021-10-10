using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFClinic.Entities
{
    [DataContract]
    [Serializable]
    public class PatientBE
    {
        private Int16 id;
        private String first_name;
        private String last_name;
        private String birthday;
        private String phone;
        private String photo;
        private String email;
        private String dni;
        private String password;
        private Boolean active;
        private DateTime created_at;

        [DataMember]
        public Int16 Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public String FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }

        [DataMember]
        public String LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }

        [DataMember]
        public String Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        [DataMember]
        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        [DataMember]
        public String Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        [DataMember]
        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public String Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        [DataMember]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public Boolean Active
        {
            get { return active; }
            set { active = value; }
        }

        [DataMember]
        public DateTime CreatedAt
        {
            get { return created_at; }
            set { created_at = value; }
        }
    }
}
