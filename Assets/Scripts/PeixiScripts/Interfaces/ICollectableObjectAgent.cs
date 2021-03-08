using System;
using UniRx;

namespace Peixi
{
    public interface ICollectableObjectAgent
    {
        IObservable<Unit> OnPlayerTouch { get; }
        IObservable<Unit> OnPlayerUntouch { get; }
        bool PlayerIsTouch { get; }
    }
}
