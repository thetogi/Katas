using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PoliceStationDispatchService.CallPriority;
using Dispatcher = PoliceStationDispatchService.Dispatch.Dispatch;

namespace PoliceStationDispatchService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var logger = new Logger.Logger();
            var policeDispatchService = new Dispatcher(logger);
            var policeStation = new PoliceStation(logger, policeDispatchService);            
            var isRunning = true;
            logger.Log($"This is police station dispatch service, what is your emergency?");
            Console.WriteLine();
            while (isRunning)
            {
                if (policeStation.PatrolCars.Count <= 0)
                {
                    for (var i = 0; i<=3; i++)
                    {
                        policeStation.RegisterPatrolCar(new Worker());
                        System.Threading.Thread.Sleep(1000);
                    }
                    logger.Log($"4 patrol cars created for police station");                    
                }

                logger.Log($"Enter command...");
                var key = Console.ReadKey().Key;

                Console.WriteLine();
                if (key == ConsoleKey.L)
                {
                    policeDispatchService.queueCall(new Call(Priority.Low));
                }
                else if(key == ConsoleKey.M)
                {
                    policeDispatchService.queueCall(new Call(Priority.Medium));
                }
                else if(key == ConsoleKey.H)
                {
                    policeDispatchService.queueCall(new Call(Priority.High));                    
                }
                else if (key == ConsoleKey.W)
                {
                    policeStation.DispatchPatrolCar();
                }
                else if (key == ConsoleKey.X)
                {
                    logger.Log($"Exiting...");
                    System.Threading.Thread.Sleep(1000);
                    isRunning = false;
                }

            }

        }


    }
}

//Console.WriteLine();
//if (key == ConsoleKey.C)
//{
//    policeDispatchService.queueCall(new Call());
//    logger.Log($"Added 1 call to queue.");
//}

//else if (key == ConsoleKey.W)
//{
//    var newWorker = new Worker(policeDispatchService);
//    newWorker.Logger = logger;
//    workers.Add(newWorker);
//    newWorker.Run();
//    logger.Log($"new worker created with ID: {newWorker.Id}");
//}


//logger.Log($"Queue contains {policeDispatchService.callCount()} calls");