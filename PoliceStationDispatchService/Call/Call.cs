using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PoliceStationDispatchService.CallPriority;

namespace PoliceStationDispatchService
{
    public class Call : ICall
    {
        public Guid Id { get; set; }

        public Priority Priority { get; set; }

        public Call()
        {
            Id = Guid.NewGuid();
        }
        public Call(Priority priority):this()
        {
            Priority = priority;
        }
    }
}
