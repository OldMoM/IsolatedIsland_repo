using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Caoye;

namespace Peixi
{
    public class Torial1DemoDirector : MonoBehaviour
    {
        public Vector2Int[] startIslandSet;
        public Transform player;
        public GameObject androidra;

        public Transform blackScreen;
        private IDialogSystem dialogSystem;
        private IPlayerSystem iplayerSystem;
        public AndroidActiveProgram activeProgram;
        public Transform gameFlowPointer;

        private void Start()
        {
            

            blackScreen.gameObject.SetActive(true);
            dialogSystem = FindObjectOfType<LvDialogSystem>();
            iplayerSystem = InterfaceArichives.Archive.PlayerSystem;

            iplayerSystem.StateController.playerState.Value = PlayerState.InteractState;

            Observable.Timer(TimeSpan.FromSeconds(1))
                .First()
                .Subscribe(x =>
                {
                    dialogSystem.StartDialog(DialogIdTags.Lee_gameStart);
                });

            dialogSystem.OnDialogEnd
                .Where(x => x == DialogIdTags.Lee_gameStart)
                .First()
                .Subscribe(x =>
                {
                    //InitScene();
                });

            dialogSystem.OnDialogEnd
                .Delay(TimeSpan.FromSeconds(2))
                .First()
                .Subscribe(x =>
                {
                    var blackScreenFadeOut = FindObjectOfType<TimeSystemView>();
                    blackScreenFadeOut.BlackScreenFadeOut();
                });

            dialogSystem.OnDialogEnd
                .Delay(TimeSpan.FromSeconds(3))
                .First()
                .Subscribe(x =>
                {
                    dialogSystem.StartDialog(DialogIdTags.Lee_firstWakeUp);
                });

            dialogSystem.OnDialogEnd
                .Where(x => x == DialogIdTags.Lee_firstWakeUp)
                .First()
                .Subscribe(x =>
                {
                    var playerState = iplayerSystem.StateController.playerState;
                    playerState.Value = PlayerState.IdleState;
                    blackScreen.gameObject.SetActive(false);
                    //ShowMessage.singlton.Message("按下WASD移动");

                    gameFlowPointer.gameObject.SetActive(true);
                });

            dialogSystem.OnDialogEnd
                .Delay(TimeSpan.FromSeconds(2))
                .First()
                .Subscribe(x =>
                {
                    ShowMessage.singlton.Message("");
                });

            activeProgram.OnAndroidraActiveCompleted
                .Subscribe(x =>
                {
                    dialogSystem.StartDialog(DialogIdTags.Androidra_actived);
                    iplayerSystem.StateController.playerState.Value = PlayerState.InteractState;

                    gameFlowPointer.gameObject.SetActive(false);
                });

            /*Androidra被激活后播放对话Androidra_actived
            对话完成后，玩家获得操作
            Androidra状态设置为Idle
            激活PlayerPropertyHUD*/
            dialogSystem.OnDialogEnd
                .Where(x => x == DialogIdTags.Androidra_actived)
                .Subscribe(x =>
                {
                    iplayerSystem.StateController.playerState.Value = PlayerState.IdleState;

                    var iandroidra = InterfaceArichives.Archive.IAndroidraSystem;
                    iandroidra.androidState.SetState(AndroidraState.Idle, this.name);

                    var UIComponents = InterfaceArichives.Archive.InGameUIComponentsManager;
                    UIComponents.PlayerPropertyHUD.SetActiveHUD(true);

                    GameStageManager.StartStage("onPropertySpecification_Start");
                });
        }

        public void InitScene()
        {
            var iBuildSystem = InterfaceArichives.Archive.IBuildSystem;

            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(x =>
                {
                    foreach (var islandPos in startIslandSet)
                    {
                        iBuildSystem.BuildIslandAt(islandPos);
                    }
                });
            var islands = FindObjectsOfType<IslandPresenter>();
            islands.ToObservable().ForEachAsync(x =>
            {
                x.enabled = false;
            });
        }
    }
}
