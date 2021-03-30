using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi.WeatherSystem
{
    /// <summary>
    ///   <para>主要功能：依据时间变化改变环境光和天气设置</para>
    ///   <para>组成模块：</para>
    ///   <para>-EnvironemntLightAgent：控制环境光</para>
    ///   <para>-WeatherAgent：控制天气特效</para>
    ///   <para>-LightEffectController：智钦做的特效</para>
    ///   <para>
    ///     <br />
    ///   </para>
    /// </summary>
    /// <seealso cref="EnvironmentLightAgent" />
    /// <seealso cref="WeatherAgent" />
    /// <seealso cref="LightEffectController" />
    public class WeatherControlSystem : MonoBehaviour
    {
        private WeatherAgent CreateWeatherAgent()
        {
            return null;
        }
        private EnvironmentLightAgent CreateEnvironmentLightAgent()
        {
            return null;
        }

        public Animator weatherAnimator;


        private void Start()
        {
            weatherAnimator.speed = 1;
            weatherAnimator.Play("AnimData", 0, 0);

            InterfaceArichives.Archive.ITimeSystem.onDayStart
                .Subscribe(x =>
                {
                    weatherAnimator.speed = 1;
                    weatherAnimator.Play("AnimData", 0, 0);
                });

            InterfaceArichives.Archive.ITimeSystem.onDayEnd
                .Subscribe(x =>
                {
                    weatherAnimator.speed = 0;
                });
        }
    }
}
