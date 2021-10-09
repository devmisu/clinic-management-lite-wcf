using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFClinic.Entities;

namespace WCFClinic
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceArea" in both code and config file together.
    [ServiceContract]
    public interface IServiceArea
    {
        [OperationContract]
        Boolean CreateArea(AreaBE objAreaBE);

        [OperationContract]
        List<AreaBE> GetAllAreas();

        [OperationContract]
        AreaBE GetOneArea(Int16 id);

        [OperationContract]
        Boolean UpdateArea(AreaBE objAreaBE);

        [OperationContract]
        Boolean DeleteArea(Int16 id);
    }
}
