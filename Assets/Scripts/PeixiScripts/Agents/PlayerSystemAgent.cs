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
        #region//Private variables
        private IPlayerSystem iPlayerSystem;
        private IDialogSystem iDialogSystem;
        private PlayerStateController controller;
        private Rigidbody rigid;
        private ITimeSystem timeSystem;
        private FacilityInteractionAgent facilityInteractionAgent;
        #endregion

        #region//Private methods
        private void SetPlayState(PlayerState state)
        {
            controller.playerState.Value = state;
        }
        private void Init(IPlayerSystem playerSystem)
        {
            iPlayerSystem = playerSystem;
            iDialogSystem = InterfaceArichives.Archive.IDialogSystem;
            controller = playerSystem.StateController;
            rigid = iPlayerSystem.Rigid;
            timeSystem = InterfaceArichives.Archive.ITimeSystem;
            facilityInteractionAgent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
        }
        #endregion

        public PlayerSystemAgent (IPlayerSystem playerSystem)
        {
            Init(playerSystem);

            #region//对DialogSystem事件的反应
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
            #endregion

            #region//对TimeSystem事件的反应
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
            #endregion

            #region//对设施交互事件的反应
            facilityInteractionAgent.RestoreIslandProgress.OnInteractStart
                .Subscribe(x =>
                {
                    SetPlayState(PlayerState.InteractState);
                });

            facilityInteractionAgent.RestoreIslandProgress.OnInteractEnd
                .Subscribe(x =>
                {
                    SetPlayState(PlayerState.IdleState);
                });


            #endregion
        }
    }
}
