using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Peixi {
    public class GarbagePresenter : BaseItem
    {
        public CollectableObjectAgent collectable;
        Rigidbody rigid;
        public FacilityData garbageData;


        private void OnEnable()
        {
            rigid = GetComponent<Rigidbody>();

            collectable = new CollectableObjectAgent(GetComponent<SphereCollider>());
            garbageData.instanceId = gameObject.GetInstanceID();

            Entity.garbages.Add(garbageData.instanceId, gameObject);

            collectable.OnPlayerTouch
                .Subscribe(x =>
                {
                    var interactionAgent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
                    interactionAgent.PlayerTouchFacility(garbageData);
                }).AddTo(this);

            collectable.OnPlayerUntouch
                .Subscribe(x =>
                {
                    var interactionAgent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
                    interactionAgent.PlayerUntouchFacility(garbageData);
                }).AddTo(this);
        }
        /// <summary>
        /// 激活Garbage并设置速度和方向
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public void Active(float speed, Vector3 direction)
        {
            rigid.velocity = direction.normalized * speed;

            //-----10秒后回收Garbage-----
            Observable
                .Timer(System.TimeSpan.FromSeconds(25))
                .First()
                .Subscribe(x =>
                {
                    Recycle();
                }).AddTo(this);
        }
        public override void Recycle()
        {
            ArbitorSystem.Singlton.OnItemRecycle(this);
            Destroy(gameObject);
        }
    }
}
