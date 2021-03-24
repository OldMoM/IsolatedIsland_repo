﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class AndroidraStateController
    {
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
        public AndroidraStateController(IPlayerSystem playerSystem,AndroidraNavPresenter nav, AndroidraStateControllerModel controlModel,IAndroidraSystem system)
        {
            this.playerSystem = playerSystem;
            navModule = nav;
            model = controlModel;
            _system = system;

            React(OnPlayerStartMoveToFollow)
               .React(OnPlayerEndMoveToEndFollow)
               .React(OnBuildMsgReceived)
               .React(OnBuildAnimationEnd);
            
        }
        public AndroidraStateController React(Action action)
        {
            action();
            return this;
        }

        private IPlayerSystem playerSystem;
        private ReactiveProperty<AndroidraState> state = new ReactiveProperty<AndroidraState>(AndroidraState.Idle);
        private AndroidraNavPresenter navModule;
        private AndroidraStateControllerModel model;
        private IAndroidraSystem _system;
        private void OnPlayerStartMoveToFollow()
        {
            playerSystem.StateController.onStateChanged
                .Where(x => x == PlayerState.MotionState)
                .Where(x => state.Value != AndroidraState.Building)
                .Subscribe(x =>
                {
                    Debug.Log(2);
                    state.Value = AndroidraState.Follow;
                });

            playerSystem.StateController.onStateChanged
                .Where(x => x == PlayerState.MotionState)
                .Subscribe(x =>
                {
                    Debug.Log(3);
                    state.Value = AndroidraState.Follow;
                });

        }
        private void OnPlayerEndMoveToEndFollow()
        {
        
        }
        private void OnBuildMsgReceived()
        {
            model.OnBuildMsgReceived
                .Subscribe(x =>
                {
                    state.Value = AndroidraState.Building;
                });
        }        
        private void OnBuildAnimationEnd()
        {
            _system.BuildAnim.OnBuildAnimEnd
                 .Subscribe(x =>
                 {
                     state.Value = AndroidraState.Follow;
                 });
        }
        public void SetState(AndroidraState state,string sender)
        {
            this.state.Value = state;
        }
    }
    
    public enum AndroidraState
    {
        Idle,
        Follow,
        Building,
        Sleep
    }
}
