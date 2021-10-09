﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFClinic.Entities
{
    [DataContract]
    [Serializable]
    public class AreaBE
    {
        private Int16 id;
        private String name;
        private String description;
        private Boolean active;
        private DateTime created_at;

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
        public String Description
        {
            get { return description; }
            set { description = value; }
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
