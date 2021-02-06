using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Peixi
{
    public interface ITimeSystem 
    {
        int day { get; }
        int time { get; }
        bool isDay { get; }
        void init();
        IObservable<int> onDayStart { get; }
        IObservable<int> onDayEnd { get; }
        IObservable<int> onTimeChanged { get; }

    }
}
