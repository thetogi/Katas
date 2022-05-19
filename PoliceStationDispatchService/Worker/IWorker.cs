using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationDispatchService
{
    public interface IWorker
    {
        Guid Id { get; set; }
        List<Guid> workedCalls { get; set; }
        ILogger Logger { get; set; }

        event EventHandler evReady;
        void HandleCall(ICall call);        
    }
}
