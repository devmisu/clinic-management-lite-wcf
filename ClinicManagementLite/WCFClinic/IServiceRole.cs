using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFClinic.Entities;

namespace WCFClinic
{
    [ServiceContract]
    public interface IServiceRole
    {
        [OperationContract]
        Boolean CreateRole(RoleBE objRoleBE);

        [OperationContract]
        List<RoleBE> GetAllRoles();

        [OperationContract]
        RoleBE GetOneRole(Int16 id);

        [OperationContract]
        Boolean UpdateRole(RoleBE objRoleBE);

        [OperationContract]
        Boolean DeleteRole(Int16 id);
    }
}
