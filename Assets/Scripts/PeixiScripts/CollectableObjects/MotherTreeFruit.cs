using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class MotherTreeFruit : MonoBehaviour
    {
        private CollectableObjectAgent agent;
        // Start is called before the first frame update
        void Start()
        {
            var sphereCollider = GetComponent<SphereCollider>();
            agent = new CollectableObjectAgent(sphereCollider);

            InputSystem.Singleton.OnInteractBtnPressed
                .Where(x => agent.PlayerIsTouch)
                .Subscribe(x =>
                {
                    print("player picked mother tree fruit");
                    var inventory = InterfaceArichives.Archive.IInventorySystem;
                    inventory.AddItem("Apple", 1);
                    ShowMessage.singlton.Message("");
                    Destroy(gameObject, 0.2f);
                }).AddTo(this);

            agent.OnPlayerTouch
                .Subscribe(x =>
                {
                    ShowMessage.singlton.Message("按E键摘取苹果");
                });

            agent.OnPlayerUntouch
                .Subscribe(x =>
                {
                    ShowMessage.singlton.Message("");
                });
        }
    }
}
