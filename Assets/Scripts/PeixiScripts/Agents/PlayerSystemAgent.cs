using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Caoye;

namespace Peixi 
{
    public class PlayerSystemAgent
    {
        private IPlayerSystem iPlayerSystem;
        private IDialogSystem iDialogSystem;
        private PlayerStateController controller;
        private Rigidbody rigid;
        private ITimeSystem timeSystem;

        private void SetPlayState(PlayerState state)
        {
            controller.playerState.Value = state;
        }

        public PlayerSystemAgent (IPlayerSystem playerSystem)
        {
            iPlayerSystem = playerSystem;
            iDialogSystem = InterfaceArichives.Archive.IDialogSystem;
            controller = playerSystem.StateController;
            rigid = iPlayerSystem.Rigid;
            timeSystem = InterfaceArichives.Archive.ITimeSystem;


            iDialogSystem.OnDialogStart
                .Subscribe(x =>
                {
                    SetPlayState(PlayerState.InteractState);
                    rigid.velocity = Vector3.zero;
                });

            iDialogSystem.OnDialogEnd
                .Subscribe(x =>
                {
                    SetPlayState(PlayerState.IdleState);
                });

            timeSystem.onDayEnd
                .Subscribe(x =>
                {
                    SetPlayState(PlayerState.InteractState);
                    rigid.velocity = Vector3.zero;
                });

            timeSystem.onDayStart
                .Delay(TimeSpan.FromSeconds(1))
                .Subscribe(x =>
                {
                    SetPlayState(PlayerState.IdleState);
                });

        }
    }
}
