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
    public interface IServiceMedicalRecord
    {
        [OperationContract]
        Boolean CreateMedicalRecord(MedicalRecordBE objMedicalRecordBE);

        [OperationContract]
        List<MedicalRecordBE> GetAllMedicalRecords();

        [OperationContract]
        MedicalRecordBE GetMedicalRecord(Int16 id);

        [OperationContract]
        Boolean UpdateMedicalRecord(MedicalRecordBE objMedicalRecordBE);

        [OperationContract]
        Boolean DeleteMedicalRecord(Int16 id);
    }
}
