using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public struct PlayerStateController
    {
        public ReactiveProperty<PlayerState> playerState;
        private IObservable<InteractState> onInteractStateChanged;
        public void Init()
        {
            InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent.onStateChanged
                .Where(x => x == InteractState.Interact)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                });
        }
    }
   
}
