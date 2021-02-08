using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class PlayerStateController
    {
        public ReactiveProperty<PlayerState> playerState = new ReactiveProperty<PlayerState>(PlayerState.IdleState);
        public IObservable<PlayerState> onInteractStateChanged => playerState;

        private IObservable<FacilityType> onInteractStart => InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent.OnInteractStart;
        private IObservable<Unit> onInteractEnd => InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent.OnInteractEnd;
        public void Init()
        {
            React(OnInteractStart)
                .React(OnInteractEnd);

        }
        public PlayerStateController()
        {
            //React(OnInteractStart)
                //.React(OnInteractEnd);
        }
        PlayerStateController React(Action action)
        {
            action();
            return this;
        }
        void OnInteractStart()
        {
            onInteractStart.Subscribe(x =>
            {
                playerState.Value = PlayerState.InteractState;
            });
        }
        void OnInteractEnd()
        {
            var controller = this;
            onInteractEnd
                .Subscribe(x =>
                {
                    playerState.Value = PlayerState.IdleState;
                });
        }
    }
   
}
