using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
