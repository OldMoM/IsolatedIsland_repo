using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Peixi {
    public class GarbagePresenter : BaseItem
    {
        Rigidbody rigid;
        private void OnEnable()
        {
            rigid = GetComponent<Rigidbody>();
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

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerTouch(other);
        }
        private void OnTriggerExit(Collider other)
        {
            OnPlayerUntouch(other);
        }
    }
}
