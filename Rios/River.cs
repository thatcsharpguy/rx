using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rios
{
    public class River
    {
        private readonly int _fishAmount;
        private readonly int _waitTime;
        private readonly bool _dangerousRiver;


        public River(int fishAmount, int waitTime = 500,  bool dangerousRiver = false)
        {
            _fishAmount = fishAmount;
            _waitTime = waitTime;
            _dangerousRiver = dangerousRiver;
        }

        public IObservable<Fish> Stream()
        {
            var observable = Observable.Create<Fish>(observer =>
            {
                for (int i = 0; i < _fishAmount; i++)
                {
                    var fish = Fish.RandomFish();
                    observer.OnNext(fish);
                    Thread.Sleep(_waitTime);

                    if (_dangerousRiver && i == _fishAmount / 3)
                        throw new Exception("Uh, hubo un derrame en el río");
                }
                observer.OnCompleted();
                return () => { };
            });

            return observable;
        }
    }
}
