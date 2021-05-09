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
    public class InteractTipWidge : MonoBehaviour
    {
        Transform icon_rect;
        Image icon;
        FacilityInteractionAgent handle;
        public SpriteAtlas iconAtlas;

        private List<FacilityType> staticTipIcon = new List<FacilityType>()
        {
            FacilityType.Distiller,
            FacilityType.FishPoint,
            FacilityType.FoodPlant,
            FacilityType.Island,
            FacilityType.Tent,
            FacilityType.None
        };

        // Start is called before the first frame update
        void Start()
        {
            Config()
                .React(OnTargetChanged);
                //.React(OnStateChanged);
        }
        InteractTipWidge Config()
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
        InteractTipWidge React(Action action)
        {
            action();
            return this;
        }
        void OnTargetChanged()
        {
            handle.onTargetChanged
                .Skip(1)
                .Where(x=> staticTipIcon.Contains(x.type))
                .Subscribe(x =>
                {
                    var worldPos = x.position;
                    var screenPos = FindObjectOfType<Camera>().WorldToScreenPoint(worldPos);
                    var offset_screen = new Vector3(0, 200, 0);
                    transform.position = screenPos + offset_screen;

                    icon_rect.gameObject.SetActive(true);

                    switch (x.type)
                    {
                        case FacilityType.None:
                            icon_rect.gameObject.SetActive(false);
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
