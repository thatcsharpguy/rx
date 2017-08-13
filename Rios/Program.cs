using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rios
{
    class Program
    {
        static void Main(string[] args)
        {
            var rioNormal = new River(10);
            var rioRapido = new River(10, waitTime: 10);
            var dangerousRiver = new River(fishAmount: 10, dangerousRiver: true);

            Console.WriteLine("Comenzando pesca:");

            rioNormal.Stream()
                .Subscribe(
                    onNext: fish => Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: {fish}"),
                    onCompleted: () => Console.WriteLine("¡Terminé de pescar!")
                );

            Console.WriteLine();
            Console.WriteLine("Comenzando pesca en un río peligroso:");
            dangerousRiver.Stream()
                .Subscribe(
                    onNext: fish => Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: {fish}"),
                    onError: ex => Console.WriteLine($"Ocurrió un problema en el río: {ex.Message}"),
                    onCompleted: () => Console.WriteLine("¡Terminé de pescar en el río peligroso!")
                );

            Console.WriteLine();
            Console.WriteLine("Comenzando pesca (solo peces verdes y grandes):");
            rioNormal.Stream()
                .Where(fish => fish.Color == Color.Green && fish.Weight > 3000) // Filtramos los elementos
                .Subscribe(
                    onNext: fish => Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: {fish}"),
                    onCompleted: () => Console.WriteLine("¡Terminé de pescar!")
                );

            Console.WriteLine();
            Console.WriteLine("Comenzando pesca (usando una red para 3 peces):");
            rioNormal.Stream()
                .Buffer(3) // Agrupamos 3 elementos
                .Subscribe(
                    onNext: fishCollection =>
                    {
                        var f = String.Join(", ", fishCollection.Select(fi => fi.Species));
                        Console.WriteLine($"Atrapé {fishCollection.Count} peces ({f})");
                    },
                    onCompleted: () => Console.WriteLine("¡Terminé de pescar usando una red para 3 peces!")
                );

            var erik = new Fisher("Erik");

            Console.WriteLine();
            Console.WriteLine("Erik va a pescar:");
            rioNormal.Stream()
                .Subscribe(erik);

            Console.WriteLine("Presiona una tecla para terminar");
            Console.Read();
        }
    }
}
