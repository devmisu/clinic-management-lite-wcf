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
                if (objRoleBE.Name == null || objRoleBE.Attributes == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                if ((from role in db.Roles where role.active && role.name == objRoleBE.Name select role).FirstOrDefault() != null)
                {
                    throw new Exception("Ya existe un rol registrado con ese nombre.");
                }

                Role tbRole = new Role();

                tbRole.name = objRoleBE.Name;
                tbRole.attributes = objRoleBE.Attributes;
                tbRole.active = true;
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
                Role tbRole = (from role in db.Roles where role.active && role.id == id select role).FirstOrDefault();

                if (tbRole == null)
                {
                    throw new Exception("No se encontro rol.");
                }

                if ((from user in db.Users where user.active && user.Role.active && user.Role.id == id select user).Count() != 0)
                {
                    throw new Exception("No se puede eliminar, hay usuarios asignados al rol.");
                }

                tbRole.active = false;
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
                Role tbRole = (from role in db.Roles where role.active && role.id == objRoleBE.Id select role).FirstOrDefault();

                if (tbRole == null)
                {
                    throw new Exception("No se encontro rol.");
                }

                if (objRoleBE.Name != null) tbRole.name = objRoleBE.Name;
                if (objRoleBE.Attributes != null) tbRole.attributes = objRoleBE.Attributes;

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

                var query = (from role in db.Roles orderby role.name where role.active select role);

                foreach (var tbRole in query)
                {
                    listRoles.Add(RoleBE.Create(tbRole));
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
                Role tbRole = (from role in db.Roles where role.active && role.id == id select role).FirstOrDefault();

                if (tbRole == null)
                {
                    throw new Exception("No se encontro rol.");
                }

                return RoleBE.Create(tbRole);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
