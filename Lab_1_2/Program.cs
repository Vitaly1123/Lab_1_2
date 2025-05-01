using System;


namespace Lab_1_2
{
    class Timer
    { 
        private readonly Action _action;
        private readonly int _interval;
        private bool _running;

        public Timer(Action action, int interval) 
        {
            _action = action;
            _interval = interval;
        }
        
        public void Start() 
        {
            _running = true;
            Task.Run(() =>
            {
                while (_running)
                {
                    _action();
                    Thread.Sleep(_interval * 1000);
                }
            });
        }
        public void Stop() => _running = false;

        class Program 
        {
            static void Main()
            {
                var timer1 = new Timer(() => Console.WriteLine($"[{DateTime.Now:T}] Таймер 1!"), 2);
                var timer2 = new Timer(() => Console.WriteLine($"[{DateTime.Now:T}] Таймер 2!"), 3);

                timer1.Start();
                timer2.Start();

                Thread.Sleep(10000);

                timer1.Stop();
                timer2.Stop();
            }
        }
    }
}