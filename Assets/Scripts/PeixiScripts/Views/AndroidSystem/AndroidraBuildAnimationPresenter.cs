using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class AndroidraBuildAnimationPresenter 
    {
        public IObservable<ValueTuple<string, Vector3>> OnBuildAminStart => onBuildAminStart;
        public IObservable<Unit> OnBuildAnimEnd => timer.OnTimerEnd;
        public IObservable<float> OnBuildAnimProgressChanged => timer.OnRatioProcessChanged;

        private AndroidraNavPresenter navPresenter;
        private Subject<ValueTuple<string, Vector3>> onBuildAminStart = new Subject<(string, Vector3)>();
        private Subject<Unit> onBuildAnimEnd = new Subject<Unit>();
        private Subject<float> onBuildAnimProgressChanged = new Subject<float>();

        private ConcurrentTimer timer = new ConcurrentTimer();
        //public AndroidraBuildAnimationPresenter(AndroidraNavPresenter androidraNavPresenter)
        //{
        //    navPresenter = androidraNavPresenter;
        //    navPresenter.OnAndroidraReachBuildTarget
        //        .Subscribe(x =>
        //        {
        //            onBuildAminStart.OnNext(x);
        //            timer.StartTimeCountdown(3);
        //        });
        //}

        public void StartPlayBuildAnimation()
        {
            timer.StartTimeCountdown(3);
        }
    }
}
