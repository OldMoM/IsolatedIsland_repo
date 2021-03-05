using UnityEngine.UI;
using UnityEngine;
using System;
using UniRx;


namespace Peixi {
    public class TimeSystemView : MonoBehaviour
    {
        public RawImage blackScreen;
        public float fadeTime;

        private ITimeSystem itimeSystem;
        // Start is called before the first frame update
        void Start()
        {
            itimeSystem = InterfaceArichives.Archive.ITimeSystem;

            itimeSystem.onDayEnd
                .Subscribe(x =>
                {
                    print("the day ended");
                    BlackScreenFadeIn();
                });

            itimeSystem.onDayStart
                .Subscribe(x =>
                {
                    print("the day started");
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

                    _color.a = Mathf.Lerp(blackScreen.color.a, 0, fadeTime * Time.deltaTime);
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
                _color.a = Mathf.Lerp(blackScreen.color.a, 1, fadeTime * Time.deltaTime);
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
