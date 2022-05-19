using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using PoliceStationDispatchService.Dispatch;

namespace PoliceStationDispatchService
{
    public class Worker : IWorker

    {
        public Guid Id { get; set; }
        public List<Guid> workedCalls { get; set; }
        public ILogger Logger { get; set; }
        protected bool IsIdle
        {
            get { return _call == null; }
        }
        protected ICall _call { get; set; }

        public event EventHandler evReady;
        public Worker()
        {
            Id = Guid.NewGuid();
            workedCalls = new List<Guid>();
            Run();
        }

        public void HandleCall(ICall incomingCall)
        {
            _call = incomingCall;
        }

        private void Run()
        {
            var isRunning = true;
            Task.Run(async () =>
           {
               while (isRunning)
               {
                   if (!IsIdle)
                   {
                       Work();
                   }
                   await Task.Delay(1000);
               }
           }
           );
        }

        protected void Work()
        {
            if (_call != null)
            {
                Logger?.Log($"Worker ({Id}) started call ({_call.Id}).");
                workedCalls.Add(_call.Id);
                Logger?.Log($"Worker ({Id}) completed call ({_call.Id}).");
                _call = null;
                evReady?.Invoke(this, EventArgs.Empty);
                Logger?.Log($"Worker ({Id}) is awaiting work...");
            }
        }
    }

    //Add priorities (starting to add the complex object)
    //Add logging/logger
    //Add register worker tests
}
