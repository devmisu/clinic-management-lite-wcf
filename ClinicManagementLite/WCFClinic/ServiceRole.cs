using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;
using System.Data.Entity.Core;

namespace WCFClinic
{
    public class ServiceRole : IServiceRole
    {
        public bool CreateRole(RoleBE objRoleBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Role tbRole = new Role();

                tbRole.name = objRoleBE.Name;
                tbRole.attributes = objRoleBE.Attributes;
                tbRole.active = objRoleBE.Active;
                tbRole.created_at = DateTime.Now;

                db.Roles.Add(tbRole);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteRole(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Role tbRole = (from role in db.Roles where role.id == id select role).FirstOrDefault();

                db.Roles.Remove(tbRole);
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateRole(RoleBE objRoleBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Role tbRole = (from role in db.Roles where role.id == objRoleBE.Id select role).FirstOrDefault();

                tbRole.name = objRoleBE.Name;
                tbRole.attributes = objRoleBE.Attributes;
                tbRole.active = objRoleBE.Active;

                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RoleBE> GetAllRoles()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<RoleBE> listRoles = new List<RoleBE>();

                var query = (from role in db.Roles orderby role.name select role);

                foreach (var tbRole in query)
                {
                    RoleBE objRoleBE = new RoleBE();

                    objRoleBE.Id = Convert.ToInt16(tbRole.id);
                    objRoleBE.Name = tbRole.name;
                    objRoleBE.Attributes = tbRole.attributes;
                    objRoleBE.Active = tbRole.active;
                    objRoleBE.CreatedAt = tbRole.created_at;

                    listRoles.Add(objRoleBE);
                }

                return listRoles;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RoleBE GetOneRole(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Role tbRole = (from role in db.Roles where role.id == id select role).FirstOrDefault();

                RoleBE objRoleBE = new RoleBE();

                objRoleBE.Id = Convert.ToInt16(tbRole.id);
                objRoleBE.Name = tbRole.name;
                objRoleBE.Attributes = tbRole.attributes;
                objRoleBE.Active = tbRole.active;
                objRoleBE.CreatedAt = tbRole.created_at;

                return objRoleBE;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
