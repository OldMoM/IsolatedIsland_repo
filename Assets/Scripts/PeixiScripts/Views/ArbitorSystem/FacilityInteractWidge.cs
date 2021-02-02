using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;

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
            icon_rect = transform.Find("icon");
            icon_rect.gameObject.SetActive(false);
            icon = icon_rect.GetComponent<RawImage>();
            handle = getFacilityInteractHandle();

            Assert.IsNotNull(icon_rect);
            Assert.IsNotNull(icon);
            Assert.IsNotNull(handle);

            handle.onTargetChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    var worldPos = x.position;
                    var screenPos = FindObjectOfType<Camera>().WorldToScreenPoint(worldPos);
                    transform.position = screenPos;
                });

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
        FacilityInteractionAgent getFacilityInteractHandle()
        {
            return InterfaceArichives
                .Archive
                .IarbitorSystem
                .facilityInteractionHandle;
        }
    }
}
