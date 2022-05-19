using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Kata Dispatch Service Tests")]

namespace PoliceStationDispatchService.Dispatch
{
    internal class Dispatch: IDispatch
    {
        public List<Worker> RegisteredWorkers { get; set; }
        private Queue<Call> _queue;
        private Logger.ILogger _logger;

        public Dispatch(Logger.ILogger logger)
        {
            _queue = new Queue<Call>();
            RegisteredWorkers = new List<Worker>();
            _logger = logger;
        }

        public int callCount()
        {
            return _queue.Count;
        }

        public void queueCall(Call call)
        {
            _queue.Enqueue(call);
            _logger.Log($"Added 1 {call.Priority} priority call to queue.");
        }

        public Call DequeueCall() 
        {
            if (_queue.Any())
            {
                _logger.Log($"Removed call from queue.");
                return _queue.Dequeue();
                
            }

            return null;
        }   
    }
}
