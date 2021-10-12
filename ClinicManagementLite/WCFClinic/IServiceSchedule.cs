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
    public interface IServiceSchedule
    {
        [OperationContract]
        Boolean CreateSchedule(ScheduleBE objScheduleBE);

        [OperationContract]
        List<ScheduleBE> GetAllSchedules();

        [OperationContract]
        ScheduleBE GetOneSchedule(Int16 id);

        [OperationContract]
        Boolean UpdateSchedule(ScheduleBE objScheduleBE);

        [OperationContract]
        Boolean DeleteSchedule(Int16 id);

        [OperationContract]
        List<ScheduleBE> GetAvailableSchedulesByUser(Int16 userId);
    }
}
