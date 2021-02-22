using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Events;
using Extensions;

namespace Peixi
{
    [RequireComponent(typeof(SphereCollider))]
    public class BaseItem : MonoBehaviour
    {
        public BaseItemModel data = new BaseItemModel();

        private IObservable<Collider> onPlayerTouch;
        private IObservable<Collider> onPlayerUntouch;
        private void OnTriggerEnter(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {
            List<string> vs = new List<string>();
            vs
              .AddItem("A")
              .AddItem("b");
        }
        protected SphereCollider trigger;

        void OnEnable()
        {
            init();
        }
        public virtual void Recycle()
        {
            GameObject.Destroy(gameObject);
        }
        protected virtual void OnPlayerTouch(Collider other)
        {
            if (other.transform.tag == "Player")
            {
           
                ArbitorSystem.Singlton.OnPlayerTouch(this);
            }
        }
        protected virtual void OnPlayerUntouch(Collider other)
        {
            if (other.transform.tag == "Player" )
            {
                ArbitorSystem.Singlton.OnPlayerUntouch(this);
            }
        }
        protected IArbitorSystem getArbitorSysten()
        {
            return InterfaceArichives.Archive.IArbitorSystem;    
        }
        protected virtual void init()
        {
            trigger = GetComponent<SphereCollider>();
            trigger.radius = data.detectRadius;

            onPlayerTouch = ObservableTriggerExtensions.OnTriggerEnterAsObservable(trigger)
                .Where(x => x.tag == "Player");

            onPlayerUntouch = ObservableTriggerExtensions.OnTriggerExitAsObservable(trigger)
                .Where(x => x.tag == "Player");

            onPlayerTouch.Subscribe(OnPlayerTouch);

            onPlayerUntouch.Subscribe(OnPlayerUntouch);
        }
            
    }
    [System.Serializable]
    public class BaseItemModel
    {
        public string name;
        public int amount;
        public ItemType itemType;
        public float detectRadius = 6;
        public bool isContact = false;
    }
    public enum ItemType
    {
        Material,
        Blueprint
    } 
}
