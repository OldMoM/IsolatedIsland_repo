using System;
using UniRx;
using UnityEngine;
using UniRx.Triggers;

namespace Peixi
{
    public class CollectableObjectAgent:ICollectableObjectAgent
    {
        public IObservable<Unit> OnPlayerTouch => onPlayerTouch;
        public IObservable<Unit> OnPlayerUntouch => onPlayerUntouch;
        public bool PlayerIsTouch => isActive;

        bool isActive = false;
        SphereCollider collider;
        Subject<Unit> onPlayerTouch = new Subject<Unit>();
        Subject<Unit> onPlayerUntouch = new Subject<Unit>();


        public CollectableObjectAgent(SphereCollider collider)
        {
            ObservableTriggerExtensions.OnTriggerEnterAsObservable(collider)
                .Where(x => x.transform.tag == "Player" && !isActive)
                .Subscribe(x =>
                {
                    onPlayerTouch.OnNext(Unit.Default);
                    isActive = true;
                });

            ObservableTriggerExtensions.OnTriggerExitAsObservable(collider)
                .Where(x => x.transform.tag == "Player" && isActive)
                .Subscribe(x =>
                {
                    onPlayerUntouch.OnNext(Unit.Default);
                    isActive = false;
                });
        }

    }
}
