using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PoliceStationDispatchService.CallPriority;

namespace PoliceStationDispatchService
{
    public interface ICall
    {
        Guid Id { get; set; }
        Priority Priority { get; set; }
    }
}
