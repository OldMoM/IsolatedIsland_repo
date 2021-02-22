using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class AndroidraStateController
    {
        private IPlayerSystem playerSystem;

        private ReactiveProperty<AndroidraState> state = new ReactiveProperty<AndroidraState>();
        private AndroidraNavPresenter navModule;

        public IObservable<AndroidraState> OnStateChanged => state;
        public AndroidraState State
        {
            get => state.Value;
            set
            {
                state.Value = value;
                Debug.Log(state.Value);
            }
        }

        public AndroidraStateController(IPlayerSystem playerSystem,AndroidraNavPresenter nav)
        {
            this.playerSystem = playerSystem;
            navModule = nav;

            React(OnPlayerStartMoveToFollow)
               .React(OnPlayerEndMoveToEndFollow);
        }
        public AndroidraStateController React(Action action)
        {
            action();
            return this;
        }
        public void OnPlayerStartMoveToFollow()
        {
            playerSystem.StateController.onStateChanged
                .Where(x => x == PlayerState.MotionState)
                .Subscribe(x =>
                {
                    Debug.Log("androidra start follow player");
                    state.Value = AndroidraState.Follow;
                });
                
        }
        public void OnPlayerEndMoveToEndFollow()
        {
 
        }
    }
    public struct AndroidraEngineModel
    {
        public float speed;
    }
    
    public enum AndroidraState
    {
        Idle,
        Follow,
        Building
    }
}
