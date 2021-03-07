using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Caoye;

namespace Peixi 
{
    public class PlayerSystemAgents
    {
        private IPlayerSystem iPlayerSystem;
        private IDialogSystem iDialogSystem;
        private PlayerStateController controller;

        public PlayerSystemAgents (IPlayerSystem playerSystem)
        {
            iPlayerSystem = playerSystem;
            iDialogSystem = InterfaceArichives.Archive.IDialogSystem;
            controller = playerSystem.StateController;


            iDialogSystem.OnDialogStart
                .Subscribe(x =>
                {
                    controller.playerState.Value = PlayerState.InteractState;
                });

            iDialogSystem.OnDialogEnd
                .Subscribe(x =>
                {
                    controller.playerState.Value = PlayerState.IdleState;
                });
        }
    }
}
