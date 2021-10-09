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
                Area tbArea = new Area();

                tbArea.name = objAreaBE.Name;
                tbArea.description = objAreaBE.Description;
                tbArea.active = objAreaBE.Active;
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

                var query = (from area in db.Areas orderby area.name select area);

                foreach(var tbArea in query)
                {
                    AreaBE objAreaBE = new AreaBE();

                    objAreaBE.Id = Convert.ToInt16(tbArea.id);
                    objAreaBE.Name = tbArea.name;
                    objAreaBE.Description = tbArea.description;
                    objAreaBE.Active = tbArea.active;
                    objAreaBE.CreatedAt = tbArea.created_at;

                    listAreas.Add(objAreaBE);
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
                Area tbArea = (from area in db.Areas where area.id == id select area).FirstOrDefault();

                AreaBE objAreaBE = new AreaBE();

                objAreaBE.Id = Convert.ToInt16(tbArea.id);
                objAreaBE.Name = tbArea.name;
                objAreaBE.Description = tbArea.description;
                objAreaBE.Active = tbArea.active;
                objAreaBE.CreatedAt = tbArea.created_at;

                return objAreaBE;
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
                Area tbArea = (from area in db.Areas where area.id == objAreaBE.Id select area).FirstOrDefault();

                tbArea.name = objAreaBE.Name;
                tbArea.description = objAreaBE.Description;
                tbArea.active = objAreaBE.Active;

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
                Area tbArea = (from area in db.Areas where area.id == id select area).FirstOrDefault();

                db.Areas.Remove(tbArea);
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
