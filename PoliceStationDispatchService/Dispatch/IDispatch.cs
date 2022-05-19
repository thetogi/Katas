using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationDispatchService.Dispatch
{
    public interface IDispatch
    {
        Call DequeueCall();
        int callCount();
        void queueCall(Call call);
    }
}
