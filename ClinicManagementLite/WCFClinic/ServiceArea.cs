using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity.Core;
using WCFClinic.Entities;

namespace WCFClinic
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceArea" in both code and config file together.
    public class ServiceArea : IServiceArea
    {
        public Boolean CreateArea(AreaBE objAreaBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                if (objAreaBE.Name == null)
                {
                    throw new Exception("Hay uno o mas valores invalidos.");
                }

                if ((from area in db.Areas where area.active && area.name == objAreaBE.Name select area).FirstOrDefault() != null)
                {
                    throw new Exception("Ya existe un area registrada con ese nombre.");
                }

                Area tbArea = new Area();

                tbArea.name = objAreaBE.Name;
                tbArea.description = objAreaBE.Description;
                tbArea.active = true;
                tbArea.created_at = DateTime.Now;

                db.Areas.Add(tbArea);
                db.SaveChanges();

                return true;
            }
            catch(EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AreaBE> GetAllAreas()
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                List<AreaBE> listAreas = new List<AreaBE>();

                var query = (from area in db.Areas orderby area.name where area.active select area);

                foreach(var tbArea in query)
                {
                    listAreas.Add(AreaBE.Create(tbArea));
                }

                return listAreas;
            }
            catch(EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AreaBE GetOneArea(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Area tbArea = (from area in db.Areas where area.active && area.id == id select area).FirstOrDefault();

                if (tbArea == null)
                {
                    throw new Exception("No se encontro area.");
                }

                return AreaBE.Create(tbArea);
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean UpdateArea(AreaBE objAreaBE)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Area tbArea = (from area in db.Areas where area.active && area.id == objAreaBE.Id select area).FirstOrDefault();

                if (tbArea == null)
                {
                    throw new Exception("No se encontro area.");
                }

                if (objAreaBE.Name != null) tbArea.name = objAreaBE.Name;
                tbArea.description = objAreaBE.Description;

                db.SaveChanges();

                return true;
            }
            catch(EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean DeleteArea(Int16 id)
        {
            ClinicManagementLiteEntities db = new ClinicManagementLiteEntities();
            try
            {
                Area tbArea = (from area in db.Areas where area.active && area.id == id select area).FirstOrDefault();

                if (tbArea == null)
                {
                    throw new Exception("No se encontro area.");
                }

                if (new ServiceUser().GetUsersByArea(id).Count != 0)
                {
                    throw new Exception("No se puede eliminar, hay usuarios asignados al area.");
                }

                tbArea.active = false;
                db.SaveChanges();

                return true;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
