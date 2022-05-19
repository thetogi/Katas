using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var queue = new ThreadSafeQueue();
            var isRunning = true;
            Task.Run(async () =>            
            {
                var producer = new Producer("Producer 1", queue);

                while (isRunning)
                {
                    await Task.Delay(1000);

                    producer.CreateCommand();

                }
                Console.WriteLine("Producer is shutdown");
            });

            Task.Run(async () =>
            {
                var consumers = new List<Consumer>();

                for(int i =0; i < 4; i++)
                {
                    consumers.Add(new Consumer($"Consumer {i}", queue));
                }
       

                while (isRunning)
                {
                    await Task.Delay(1500);

                    foreach (var consumer in consumers)
                        consumer.Work();

                }
                Console.WriteLine("Consumer is shutdown");
            });

            Console.ReadKey();
            isRunning = false;
            Console.ReadKey();
        }

        public class Producer
        {
            private static int commandId;
            private string _name;
            private ThreadSafeQueue _queue;

            public Producer(string name, ThreadSafeQueue queue)
            {
                _queue = queue;
                _name = name;
            }

            public void CreateCommand()
            {
                var command = new Command()
                {
                    Producer = _name,
                    Id = ++commandId
                };

                _queue.Add(command);
            }
        }

        public class Consumer
        {
            private ThreadSafeQueue _queue;
            private string _name;

            public Consumer(string name, ThreadSafeQueue queue)
            {
                _name = name;
                _queue = queue;
            }

            public void Work()
            {
                var command = _queue.Pop();

                if(command == null)
                {
                    Console.WriteLine($"consumer {_name}, did not find a command");
                }
                else
                {
                    Console.WriteLine($"consumer {_name}, processed command {command.Id} created by {command.Producer}");
                }
            }

        }

        public class ProducerConsumer2
        {
        }

        public class ThreadSafeQueue
        {
            private Queue<Command> _queue;
            private static object _lock = new object();

            public ThreadSafeQueue()
            {
                _queue = new Queue<Command>();
            }

            public void Add(Command command)
            {
                lock (_lock)
                {
                    _queue.Enqueue(command);
                }

            }

            public Command Pop()
            {
                lock( _lock)
                {
                    if (!_queue.Any())
                        return null;
                return _queue.Dequeue();
                }
            }
        }

        public class Command
        {
        public string Producer { get; set; }
        public int Id { get; set; }

        }
    }
}
