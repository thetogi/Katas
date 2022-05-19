using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationDispatchService
{
    public class PoliceStation : IPoliceStation
    {
        public Queue<IWorker> PatrolCars { get; set; }
        private Dispatch.IDispatch _dispatcher { get; set; }
        private ILogger _logger;

        public PoliceStation(ILogger logger, Dispatch.IDispatch dispatcher)
        {
            PatrolCars = new Queue<IWorker>() { };
            _logger = logger;
            _dispatcher = dispatcher;
        }
        public void RegisterPatrolCar(IWorker patrolCar)
        {
            PatrolCars.Enqueue(patrolCar);
            patrolCar.Logger = _logger;
            _logger.Log($"Registered new patrol car created with ID: {patrolCar.Id}");
            patrolCar.evReady += PatrolCar_evReady;
        }

        private void PatrolCar_evReady(object sender, EventArgs e)
        {
            PatrolCars.Enqueue(sender as IWorker);
        }

        public void DispatchPatrolCar()
        {
            _logger.Log("Attempting to dispatch patrol car");
          
            if (shouldDispatchPatrolCar())
            {
               var car = PatrolCars.Dequeue();
               _logger.Log($"patrol car {car.Id} responding to call");

               car.HandleCall(_dispatcher.DequeueCall());
            }
            else {
                _logger.Log("Dispatch did not occur, but not necessarily for the wrong reasons.");
            }                    
        }

        private bool shouldDispatchPatrolCar()
        {
            return PatrolCars.Count > 1 && _dispatcher.callCount() > 0;
        }
    }
}
