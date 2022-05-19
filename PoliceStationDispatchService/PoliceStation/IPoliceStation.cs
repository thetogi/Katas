using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationDispatchService
{
    public interface IPoliceStation
    {
        Queue<IWorker> PatrolCars { get; set; }
        void RegisterPatrolCar(IWorker patrolCar);
        void DispatchPatrolCar();
    }
}
