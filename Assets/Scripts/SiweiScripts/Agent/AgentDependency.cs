using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using System;
using UniRx;

namespace Peixi
{
    public class AgentDependency 
    {
        public float speed;
        public IObservable<Unit> onRainDay;
        public IObservable<bool> isDay;
        public IPlayerPropertySystem playerPropertySystem;
        public Subject<Unit> onGameEnd = new Subject<Unit>();
    }
}
