using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using TMPro;
using Fungus;

namespace Peixi
{
    public class AndroidActiveProgram : MonoBehaviour
    {
        public SphereCollider playerDetectCollider;

        public Transform RealAndroidActiveArtModel;
        public Transform fakeArtModel;
        public TextMeshProUGUI textMeshForTip;
        public Transform android;

        public Flowchart gameFlow;

        bool _readyToActive;
        public bool readyToActive
        {
            get => _readyToActive;
            set
            {
                _readyToActive = value;
            }
  
        }


        Animator animator;
        AndroidraStateController androidStateController;

        public IObservable<Unit> OnAndroidraActiveCompleted => onAndroidActiveCompleted;
        private Subject<Unit> onAndroidActiveCompleted = new Subject<Unit>();

        public IObservable<Collider> OnPlayerTouchBrokenAndroidra;
        public IObservable<Collider> OnPlayerUnTouchBrokenAndroidra;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            var iandroid = InterfaceArichives.Archive.IAndroidraSystem;
            androidStateController = iandroid.androidState;
            //androidStateController.SetState(AndroidraState.Sleep, this.name);

            OnPlayerTouchBrokenAndroidra = 
            ObservableTriggerExtensions.OnTriggerEnterAsObservable(playerDetectCollider)
                 .Where(x => x.transform.tag == "Player" && !_readyToActive);

            OnPlayerTouchBrokenAndroidra.Subscribe(x => { ToDoFlowChart("OnPlayerTouchBrokenAndroidra"); });


            OnPlayerUnTouchBrokenAndroidra =
            ObservableTriggerExtensions.OnTriggerExitAsObservable(playerDetectCollider)
                .Where(x => x.transform.tag == "Player" && _readyToActive);

            OnPlayerUnTouchBrokenAndroidra.Subscribe(x => { ToDoFlowChart("OnPlayerUnTouchBrokenAndroidra"); });


            InputSystem.Singleton.OnInteractBtnPressed
                .Where(x => _readyToActive)
                .First()
                .Subscribe(y =>
                {
                    ToDoFlowChart("OnAdroidraStartActive");
                    animator.Play("AndroidraActiveAnimation");

                    AudioEvents.StartAudio("OnAndroidraActived");
                });
        }

        public void AndroidraActiveProgramCompleted()
        {
            fakeArtModel.gameObject.SetActive(false);
            RealAndroidActiveArtModel.gameObject.SetActive(true);
            android.position = transform.position;
            onAndroidActiveCompleted.OnNext(Unit.Default);
            gameObject.SetActive(false);
        }

        private void ToDoFlowChart(string flowMessage)
        {
            gameFlow.SendFungusMessage(flowMessage);
        }
    }
}
