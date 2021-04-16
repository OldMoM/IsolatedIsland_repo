using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class MotherTreeFruit : MonoBehaviour
    {
        private CollectableObjectAgent agent;
        public GameObject appleArtModel;
        // Start is called before the first frame update
        void Start()
        {
            var sphereCollider = GetComponent<SphereCollider>();
            agent = new CollectableObjectAgent(sphereCollider);

            InputSystem.Singleton.OnInteractBtnPressed
                .Where(x => agent.PlayerIsTouch)
                .Subscribe(x =>
                {
                    var inventory = InterfaceArichives.Archive.IInventorySystem;
                    inventory.AddItem("fruit", 1);
                    ShowMessage.singlton.Message("");
                    appleArtModel.SetActive(false);

                    AudioEvents.StartAudio("OnPlayerPickFruit");

                    Destroy(gameObject, 1);
                }).AddTo(this);

            agent.OnPlayerTouch
                .Subscribe(x =>
                {
                    ShowMessage.singlton.Message("Press E to pick fruit");
                });

            agent.OnPlayerUntouch
                .Subscribe(x =>
                {
                    ShowMessage.singlton.Message("");
                });
        }
    }
}
