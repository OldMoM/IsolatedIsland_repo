using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Peixi
{
    public class AndroidraBuildTimerWidge : MonoBehaviour
    {
        private AndroidraBuildAnimationPresenter buildAnimationPresenter;
        private Transform timerCircle_tran;
        private Image timer_image;

        private bool isActive;
        private AndroidraBuildAnimationPresenter BuildAnimationPresenter
        {
            get
            {
                if (buildAnimationPresenter is null)
                {
                    buildAnimationPresenter = InterfaceArichives.Archive.IAndroidraSystem.BuildAnim;
                }
                return buildAnimationPresenter;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            timerCircle_tran = transform.Find("timerCircle");
            timer_image = timerCircle_tran.GetComponent<Image>();

            StartCoroutine(Init());

            
        }

        IEnumerator Init()
        {
            yield return new WaitUntil(() => BuildAnimationPresenter != null);

            BuildAnimationPresenter.OnBuildAminStart
                .Subscribe(x =>
                {
                    var camera = FindObjectOfType<Camera>();
                    var screenPos = Camera.main.WorldToScreenPoint(x.Item2);
                    var offset = new Vector3(0, 200, 0);
                    screenPos += offset;
                    timerCircle_tran.position = screenPos;
                    timerCircle_tran.gameObject.SetActive(true);
                });

            BuildAnimationPresenter.OnBuildAnimProgressChanged
                .Subscribe(x =>
                {
                    timer_image.fillAmount = x;
                });

            BuildAnimationPresenter.OnBuildAnimEnd
                .Subscribe(x =>
                {
                    timer_image.fillAmount = 0;
                    timerCircle_tran.gameObject.SetActive(false);
                });
        }

    }
}
