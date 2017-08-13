using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rios
{
    public class Fisher : IObserver<Fish>
    {
        private readonly string _name;

        public Fisher(string name)
        {
            _name = name;
        }
        public void OnNext(Fish value)
        {
            Console.WriteLine($"{_name}: atrapé un {value.Species} de color {value.Color} a las {DateTime.Now:HH:mm:ss.ffff}");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"{_name}: Oops, algo pasó {error.Message}");
        }

        public void OnCompleted()
        {
            Console.WriteLine($"{_name}: ¡Terminó la pesca!");
        }
    }
}
