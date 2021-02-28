using UnityEngine;
using UnityEngine.AI;
using System;
using UniRx;

namespace Peixi
{
    public struct AndroidraStateControllerModel
    {
        public IObservable<ValueTuple<string, Vector2Int>> OnBuildMsgReceived;
        public IPlayerSystem playerSystem;
    }
}
