using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


namespace Peixi
{
    public class AndroidActiveProgram : MonoBehaviour
    {
        public SphereCollider playerDetectCollider;

        [SerializeField] bool readyToActive;
        

        // Start is called before the first frame update
        void Start()
        {
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

        }
    }
}
