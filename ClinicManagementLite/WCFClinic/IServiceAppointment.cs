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
    public interface IServiceAppointment
    {
        [OperationContract]
        Boolean CreateAppointment(AppointmentBE objAppointmentBE);

        [OperationContract]
        List<AppointmentBE> GetAllAppointments();

        [OperationContract]
        AppointmentBE GetOneAppointment(Int16 id);

        [OperationContract]
        Boolean UpdateAppointment(AppointmentBE objAppointmentBE);

        [OperationContract]
        Boolean DeleteAppointment(Int16 id);

        [OperationContract]
        List<AppointmentBE> GetPatientAppointments(Int16 patientId);

        [OperationContract]
        List<AppointmentBE> GetUserAppointments(Int16 userId);
    }
}
