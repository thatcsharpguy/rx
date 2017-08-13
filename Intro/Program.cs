using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            var datos = new[] { 3, 4, 6, 8, 11, 13, 15, 15, 13, 10, 6, 4 };
            foreach (var dato in datos)
                Console.Write($"{dato}, ");
            Console.WriteLine("\nTerminé de leer los datos");

            Console.WriteLine();
            IObservable<int> flujoDatos = datos.ToObservable();
            flujoDatos.Subscribe(
                onNext: dato => { Console.Write($"{dato}, "); },
                onCompleted: () => { Console.WriteLine("\nTerminé de recibir los datos"); }
            );

            Console.WriteLine();
            flujoDatos
                .Where(dato => dato % 3 == 0)
                .Subscribe(
                    onNext: dato => { Console.Write($"{dato}, "); },
                    onCompleted: () => { Console.WriteLine("\nTerminé de recibir los múltiplos de 3"); }
                );

            Console.WriteLine();
            flujoDatos
                .Skip(2)
                .Take(3)
                .Subscribe(
                    onNext: dato => { Console.Write($"{dato}, "); },
                    onCompleted: () => { Console.WriteLine("\nTerminé de recibir los tres elementos a partir del 2do"); }
                );

            Console.WriteLine();
            flujoDatos
                .Min()
                .Subscribe(
                    onNext: dato => { Console.Write($"{dato}, "); },
                    onCompleted: () => { Console.WriteLine("\nTerminé de recibir los datos y presenté el menor"); }
                );

            Console.WriteLine();
            Console.WriteLine("Presiona una tecla para terminar");
            Console.Read();
        }
    }
}
