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
                .React(OnPlayerEndMove)
                .React(OnGamePaused)
                .React(OnGameResumed);
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

        void OnGamePaused()
        {
            var onGamePaused = Entity.gameTriggers["onGamePaused"] as IObservable<Unit>;
            onGamePaused.Subscribe(x =>
            {
                playerState.Value = PlayerState.InteractState;
            });
        }
        void OnGameResumed()
        {
            var onGameResumed = Entity.gameTriggers["onGameResumed"] as IObservable<Unit>;
            onGameResumed.Subscribe(x =>
            {
                playerState.Value = PlayerState.IdleState;
            });
        }
    }
   
}
