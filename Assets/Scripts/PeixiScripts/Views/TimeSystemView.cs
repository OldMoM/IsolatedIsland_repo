using UnityEngine.UI;
using UnityEngine;
using System;
using UniRx;


namespace Peixi 
{
    public class TimeSystemView : MonoBehaviour
    {
        public RawImage blackScreen;
        [Range(1,4)]
        public float fadeTime;

        private ITimeSystem itimeSystem;
        // Start is called before the first frame update
        void Start()
        {
            itimeSystem = InterfaceArichives.Archive.ITimeSystem;

            itimeSystem.onDayEnd
                .Subscribe(x =>
                {
                    BlackScreenFadeIn();
                });

            itimeSystem.onDayStart
                .Subscribe(x =>
                {
                    BlackScreenFadeOut();
                });
        }

        public void BlackScreenFadeOut()
        {
            IDisposable fadeInMircotine = null;
            Color _color = new Color();
            fadeInMircotine = Observable.EveryLateUpdate()
                .Subscribe(x =>
                {
                    _color.a = blackScreen.color.a;
                    _color.a -= 1 / fadeTime * Time.deltaTime;
                    blackScreen.color = _color;
                    if (_color.a < 0.05f)
                    {
                        _color.a = 0;
                        blackScreen.color = _color;
                        fadeInMircotine.Dispose();
                    }
                });
        }

        public void BlackScreenFadeIn()
        {
            IDisposable fadeInMircotine = null;
            Color _color = new Color();
            fadeInMircotine = Observable.EveryLateUpdate()
            .Subscribe(x =>
            {
                _color.a = blackScreen.color.a;
                _color.a += 1 / fadeTime * Time.deltaTime;
                blackScreen.color = _color;
                if (_color.a > 0.99f)
                {
                    _color.a = 1;
                    blackScreen.color = _color;
                    fadeInMircotine.Dispose();
                }
            });
        }
    }
}
