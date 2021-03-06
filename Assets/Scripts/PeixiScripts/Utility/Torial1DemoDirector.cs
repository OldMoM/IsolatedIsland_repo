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
                    
                    InitScene();
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
                .Delay(TimeSpan.FromSeconds(5))
                .First()
                .Subscribe(x =>
                {
                    dialogSystem.StartDialog(DialogIdTags.Lee_firstWakeUp);
                });

            dialogSystem.OnDialogEnd
                .Where(x => x == DialogIdTags.Lee_firstWakeUp)
                .Subscribe(x =>
                {
                    var playerState = iplayerSystem.StateController.playerState;
                    playerState.Value = PlayerState.IdleState;
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
                    iBuildSystem.BuildFacility(new Vector2Int(1, 1), PrefabTags.foodPlant);
                    iBuildSystem.BuildFacility(new Vector2Int(0, -1), PrefabTags.waterPuifier);
                    iBuildSystem.BuildFacility(new Vector2Int(-1, -1), PrefabTags.foodPlant);
                });
        }
    }
}
