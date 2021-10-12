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
    public interface IServiceUser
    {
        [OperationContract]
        Boolean CreateUser(UserBE objUserBE);

        [OperationContract]
        List<UserBE> GetAllUsers();

        [OperationContract]
        UserBE GetUser(Int16 id);

        [OperationContract]
        Boolean UpdateUser(UserBE objUserBE);

        [OperationContract]
        Boolean DeleteUser(Int16 id);

        [OperationContract]
        UserBE Login(String email, String password);
    }
}
