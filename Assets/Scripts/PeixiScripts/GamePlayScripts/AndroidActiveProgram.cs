using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;


namespace Peixi
{
    public class AndroidActiveProgram : MonoBehaviour
    {
        public SphereCollider playerDetectCollider;

        public Transform RealAndroidActiveArtModel;
        public Transform fakeArtModel;

        [SerializeField] bool readyToActive;

        Animator animator;
        AndroidraStateController androidStateController;

        public IObservable<Unit> OnAndroidraActiveCompleted => onAndroidActiveCompleted;
        private Subject<Unit> onAndroidActiveCompleted = new Subject<Unit>();

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            var iandroid = InterfaceArichives.Archive.IAndroidraSystem;
            androidStateController = iandroid.androidState;
            androidStateController.SetState(AndroidraState.Sleep, this.name);

            ObservableTriggerExtensions.OnTriggerEnterAsObservable(playerDetectCollider)
                 .Where(x => x.transform.tag == "Player" && !readyToActive)
                 .Subscribe(x =>
                 {
                     readyToActive = true;
                 });

            ObservableTriggerExtensions.OnTriggerExitAsObservable(playerDetectCollider)
                .Where(x => x.transform.tag == "Player" && readyToActive)
                .Subscribe(x =>
                {
                    readyToActive = false;
                });

            InputSystem.Singleton.OnInteractBtnPressed
                .Where(x => readyToActive)
                .First()
                .Subscribe(y =>
                {
                    print("start active android");
                    animator.Play("AndroidraActiveAnimation");
                });
        }

        public void AndroidraActiveProgramCompleted()
        {
            print("active completed");
            fakeArtModel.gameObject.SetActive(false);
            RealAndroidActiveArtModel.gameObject.SetActive(true);
            //androidStateController.SetState(AndroidraState.Idle, this.name);
            onAndroidActiveCompleted.OnNext(Unit.Default);
            gameObject.SetActive(false);
        }
    }
}
