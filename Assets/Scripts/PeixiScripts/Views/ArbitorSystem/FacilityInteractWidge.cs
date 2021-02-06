using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;
using System;

namespace Peixi
{
    public class FacilityInteractWidge : MonoBehaviour
    {
        Transform icon_rect;
        RawImage icon;
        FacilityInteractionAgent handle;
        // Start is called before the first frame update
        void Start()
        {
            Config()
                .React(OnTargetChanged)
                .React(OnStateChanged);
        }
        FacilityInteractWidge Config()
        {
            icon_rect = transform.Find("icon");
            icon_rect.gameObject.SetActive(false);
            icon = icon_rect.GetComponent<RawImage>();
            handle = InterfaceArichives
                .Archive
                .IarbitorSystem
                .facilityInteractionHandle;

            Assert.IsNotNull(icon_rect);
            Assert.IsNotNull(icon);
            Assert.IsNotNull(handle);

            return this;
        }
        FacilityInteractWidge React(Action action)
        {
            action();
            return this;
        }
        void OnTargetChanged()
        {
            handle.onTargetChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    var worldPos = x.position;
                    var screenPos = FindObjectOfType<Camera>().WorldToScreenPoint(worldPos);
                    transform.position = screenPos;
                });
        }
        void OnStateChanged()
        {
            handle.onStateChanged
               .Skip(1)
               .Subscribe(x =>
               {
                   if (x == InteractState.Idle)
                   {
                       icon_rect.gameObject.SetActive(false);
                   }
                   if (x == InteractState.Contact)
                   {
                       icon_rect.gameObject.SetActive(true);
                   }
               });
        }
    }
}
