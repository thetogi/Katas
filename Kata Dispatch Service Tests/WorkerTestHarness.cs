using PoliceStationDispatchService;
using PoliceStationDispatchService.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata_Dispatch_Service_Tests
{
    internal class WorkerTestHarness: Worker
    {
        public WorkerTestHarness(): base() {}

        public bool GetIsIdle()
        {
            return IsIdle;
        }

        public void ForceWork()
        {        
            Work();
        }
    }
}
