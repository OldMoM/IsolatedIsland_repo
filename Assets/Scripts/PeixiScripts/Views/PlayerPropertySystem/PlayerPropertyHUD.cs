using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

namespace Peixi
{
    public class PlayerPropertyHUD : MonoBehaviour
    {
        public Image healthMask;
        public Image hungerMask;
        public Image thirstMask;
        public Image pleasureMask;

        public IPlayerPropertySystem property;

        public void SetActiveHUD(bool active)
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(active);
            }
        }

        private void Start()
        {
            Config()
                .React(OnHealthChanged)
                .React(onHealthLevelChanged)
                .React(OnPleasureLevelChanged)
                .React(OnHungerLevelChanged)
                .React(OnThirstLevelChanged);
        }

        PlayerPropertyHUD Config()
        {
            property = InterfaceArichives.Archive.IPlayerPropertySystem;
            return this;
        }
        PlayerPropertyHUD React(Action action)
        {
            action();
            return this;
        }
        void OnHealthChanged()
        {
            property.OnHealthChanged
                .Subscribe(x =>
                {
                    var fillValue = x / 100.0f;
                    healthMask.fillAmount = fillValue;
                });
        }
        void OnHungerLevelChanged()
        {
            property.OnSatietyLevelChanged
                .Subscribe(x =>
                {
                    ChangeImageColor(hungerMask, x);
                });
        }
        void OnThirstLevelChanged()
        {
            property.OnThirstLevelChanged
                .Subscribe(x =>
                {
                    ChangeImageColor(thirstMask, x);
                });
        }
        void OnPleasureLevelChanged()
        {
            property.OnPleasureLevelChanged
                .Subscribe(x =>
                {
                    ChangeImageColor(pleasureMask, x);
                });
        }
        void onHealthLevelChanged()
        {
            property.OnHealthLevelChanged
                .Subscribe(x =>
                {
                    ChangeImageColor(healthMask, x);
                });
        }
        void ChangeImageColor(Image image,PropertyLevel level)
        {
            switch (level)
            {
                case PropertyLevel.Safe:
                    image.color = CreatColor(189, 183, 107);
                    break;
                case PropertyLevel.Euclid:
                    image.color = CreatColor(210, 105, 30);
                    break;
                case PropertyLevel.Keter:
                    image.color = CreatColor(178, 34, 34);
                    break;
                default:
                    break;
            }
        }
        Color CreatColor(int r,int g,int b,int a)
        {
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
        }
        Color CreatColor(int r, int g, int b)
        {
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
        }
    }
}
