using System;
using System.Reactive.Linq;
using System.Threading;

namespace Pizzeria
{
    class MainClass
    {
        public static void Main()
        {
            Random rand = new Random();

            var doughSource = Observable.Interval(TimeSpan.FromSeconds(3))
                                        .Select(_ => new Dough())
                                        .Do(dough => Console.WriteLine("The dough is ready"));
                                        //.Publish()
                                        //.RefCount();

            var sauceSource = Observable.Interval(TimeSpan.FromSeconds(5))
                                        .Select(_ => new Sauce())
                                        .Do(sauce => Console.WriteLine("Prepared sauce"));
                                        //.Publish()
                                        //.RefCount();

            var toppingSource = Observable.Interval(TimeSpan.FromSeconds(2))
                                          .Select(_ => new Topping())
                                          .Do(top => Console.WriteLine("New topping"));
                                          //.Publish()
                                          //.RefCount();

            var pizzaSource = Observable.Zip(doughSource, sauceSource, toppingSource,
                                             resultSelector: (dough, sauce, topping) => new Pizza
                                             {
                                                 Base = dough,
                                                 Sauce = sauce,
                                                 Topping = topping
                                             })
                                        .Do(pizza => Console.WriteLine("\tPizza lista!"));
                                        //.Publish()
                                        //.RefCount();

            Console.WriteLine("Pizzería! presiona ctrl + c para terminar.");

            pizzaSource.Subscribe();

            Thread.Sleep(Timeout.InfiniteTimeSpan);
        }

        public class Pizza
        {
            public Topping Topping { get; set; }
            public Sauce Sauce { get; set; }
            public Dough Base { get; set; }
        }

        public class Topping
        {
        }

        public class Sauce
        {
        }

        public class Dough
        {
        }
    }
}
