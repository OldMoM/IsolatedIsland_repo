using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;
using System;
using UnityEngine.U2D;

namespace Peixi
{
    public class FacilityInteractWidge : MonoBehaviour
    {
        Transform icon_rect;
        Image icon;
        FacilityInteractionAgent handle;

        public SpriteAtlas iconAtlas;
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
            icon = icon_rect.GetComponent<Image>();
            handle = InterfaceArichives
                .Archive
                .IArbitorSystem
                .facilityInteractAgent;

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
                    var offset_screen = new Vector3(0, 200, 0);
                    transform.position = screenPos + offset_screen;

                    switch (x.type)
                    {
                        case FacilityType.None:
                            break;
                        case FacilityType.Island:
                            break;
                        case FacilityType.FishPoint:
                            icon.sprite = iconAtlas.GetSprite("Icon_Fishing");
                            break;
                        case FacilityType.FoodPlant:
                            icon.sprite = iconAtlas.GetSprite("Icon_FoodCollect");
                            break;
                        case FacilityType.Distiller:
                            icon.sprite = iconAtlas.GetSprite("Icon_Service");
                            break;
                        case FacilityType.Tent:
                            icon.sprite = iconAtlas.GetSprite("Icon_Sleeping");
                            break;
                        default:
                            break;
                    }
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
