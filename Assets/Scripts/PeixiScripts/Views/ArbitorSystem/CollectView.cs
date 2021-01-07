using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Peixi
{
    public class CollectView : MonoBehaviour
    {
        #region//Variables
        Image collectIcon;
        /// <summary>
        /// bubble的追踪Item
        /// </summary>
        private Transform bubbleSpike;
        /// <summary>
        /// 允许气泡追踪Item
        /// </summary>
        private bool activeBubbleFollow;
        #endregion

        #region//Private methods
        private void Init()
        {
            collectIcon = transform.GetComponentInChildren<Image>();
            collectIcon.enabled = false;
        }
        private void ThreadForBubbleFollowingItem()
        {
            Observable
              .EveryUpdate()
              .Where(x => activeBubbleFollow || bubbleSpike != null)
              .Subscribe(x => {
                  var worldPos = bubbleSpike.position;
                  Vector3 scenePos = Camera.main.WorldToScreenPoint(worldPos);
                  collectIcon.enabled = true;
                  collectIcon.rectTransform.position = scenePos;
              });
        }
        #endregion

        void Start()
        {
            Init();

            ArbitorSystem.Singlton.HandleItem
                .Subscribe(x =>
                {
                    if (x == null)
                    {
                        collectIcon.enabled = false;
                        activeBubbleFollow = false;
                        bubbleSpike = null;
                    }
                    else
                    {
                        bubbleSpike = x.transform;
                        activeBubbleFollow = true;
                        collectIcon.enabled = true;
                    }
                });

            ArbitorSystem.Singlton.OnCollectHandleCountChanged
                .Where(x => x == 0)
                .Subscribe(y =>
                {
                    collectIcon.enabled = false;
                });

            ThreadForBubbleFollowingItem();
        }
    }
}
