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
    public interface IServicePatient
    {

        // CRUD SERVICES

        [OperationContract]
        Boolean CreatePatient(PatientBE objPatientBE);

        [OperationContract]
        List<PatientBE> GetAllPatients();

        [OperationContract]
        PatientBE GetPatient(Int16 id);

        [OperationContract]
        Boolean UpdatePatient(PatientBE objPatientBE);

        [OperationContract]
        Boolean DeletePatient(Int16 id);

        [OperationContract]
        PatientBE Login(String dni, String password);
    }
}
