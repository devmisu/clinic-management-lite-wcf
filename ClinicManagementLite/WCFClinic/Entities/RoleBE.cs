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
    public class RoleBE
    {

        private Int16 id;
        private String name;
        private String attributes;
        private Boolean active;
        private DateTime created_at;

        public static RoleBE Create(Role tbRole)
        {
            RoleBE objRoleBE = new RoleBE();

            objRoleBE.Id = Convert.ToInt16(tbRole.id);
            objRoleBE.Name = tbRole.name;
            objRoleBE.Attributes = tbRole.attributes;
            objRoleBE.Active = tbRole.active;
            objRoleBE.CreatedAt = tbRole.created_at;

            return objRoleBE;
        }

        [DataMember]
        public Int16 Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public String Attributes
        {
            get { return attributes; }
            set { attributes = value; }
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
