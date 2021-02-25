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
        public IObservable<PlayerState> onStateChanged => playerState;
        private IObservable<FacilityType> onInteractStart => InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent.OnInteractStart;
        private IObservable<Unit> onInteractEnd => InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent.OnInteractEnd;
        private PlayerMovementPresenter movementPresenter;
        public void Init(PlayerMovementPresenter movementPresenter)
        {
            this.movementPresenter = movementPresenter;

            React(OnInteractStart)
                .React(OnInteractEnd)
                .React(OnPlayerStartMove)
                .React(OnPlayerEndMove);
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
                Debug.Log("on player interact start");
                playerState.Value = PlayerState.InteractState;
                Debug.Log(playerState.Value);
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

        void OnPlayerStartMove()
        {
            movementPresenter.OnVelocityChanged
                .Where(x => x.sqrMagnitude > 0.1f)
                .Subscribe(x =>
                {
                    playerState.Value = PlayerState.MotionState;
                });
        }
        void OnPlayerEndMove()
        {
            movementPresenter.OnVelocityChanged
                .Where(x => x.sqrMagnitude <= 0.1f)
                .Where(x=> playerState.Value == PlayerState.MotionState)
                .Subscribe(x =>
                {
                    playerState.Value = PlayerState.IdleState;
                });
        }
    }
   
}
