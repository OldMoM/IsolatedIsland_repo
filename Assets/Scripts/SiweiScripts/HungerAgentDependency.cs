using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using System;
using UniRx;

namespace Peixi
{
    public struct HungerAgentDependency 
    {
        public float speed;
        public IObservable<Unit> onRainDay;
        public IObservable<bool> isDay;
        public IPlayerPropertySystem playerPropertySystem;
    }
}
