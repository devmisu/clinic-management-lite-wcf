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
    public interface IServiceQueue
    {
        [OperationContract]
        Boolean CreateQueue(QueueBE objQueueBE);

        [OperationContract]
        List<QueueBE> GetAllQueues();

        [OperationContract]
        QueueBE GetOneQueue(Int16 id);

        [OperationContract]
        Boolean UpdateQueue(QueueBE objQueueBE);

        [OperationContract]
        Boolean DeleteQueue(Int16 id);

        [OperationContract]
        List<QueueBE> GetUserQueues(Int16 userId);

        [OperationContract]
        List<QueueBE> GetPatientQueues(Int16 patientId);
    }
}
