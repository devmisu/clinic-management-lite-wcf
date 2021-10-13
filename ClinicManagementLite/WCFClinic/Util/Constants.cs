using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFClinic.Util
{
    enum QueueState
    {
        REQUESTED = '1',
        ACCEPTED = '2',
        CANCELLED = '3',
        REJECTED = '4'
    }

    enum AppointmentState
    {
        STARTED = '1',
        FINISHED = '2'
    }
}
