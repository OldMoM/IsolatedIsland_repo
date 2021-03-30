using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class WeatherController : MonoBehaviour
    {
        public Animator weatherAniamtor;
        // Start is called before the first frame update
        void Start()
        {
            var itimeSystem = InterfaceArichives.Archive.ITimeSystem;

            weatherAniamtor.speed = 1;
            weatherAniamtor.Play("AnimData", 0, 0);

            itimeSystem.onDayStart
                .Subscribe(x =>
                {
                    weatherAniamtor.speed = 1;
                    weatherAniamtor.Play("AnimData",0,0);
                });
        }
    }
}
