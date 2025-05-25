using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_1_2
{
    class Timer
    {
        private readonly Action _action;
        private readonly Random _random = new Random();
        private bool _running;

        public Timer(Action action)
        {
            _action = action;
        }

        public void Start()
        {
            _running = true;
            Task.Run(() =>
            {
                while (_running)
                {
                    int delay = _random.Next(1000, 5000);
                    Thread.Sleep(delay);
                    if (_running)
                    {
                        _action();
                    }
                }
            });
        }

        public void Stop() => _running = false;
    }

    class Program
    {
        static void Main()
        {
            Random rnd = new Random();
            Timer[] timers = new Timer[]
            {
                new Timer(() => Console.WriteLine($"[{DateTime.Now:T}] Таймер 1!")),
                new Timer(() => Console.WriteLine($"[{DateTime.Now:T}] Таймер 2!")),
                new Timer(() => Console.WriteLine($"[{DateTime.Now:T}] Таймер 3!"))
            };

            for (int i = 0; i < 3; i++)
            {
                timers[rnd.Next(3)].Start();
            }

            Thread.Sleep(20000);

            foreach (var timer in timers)
            {
                timer.Stop();
            }

        }
    }
}
