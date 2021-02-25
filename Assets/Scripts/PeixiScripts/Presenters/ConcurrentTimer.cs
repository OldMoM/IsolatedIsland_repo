using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public class ConcurrentTimer
    {
        public IObservable<Unit> OnTimerStart => onTimeCountdownStart;
        public IObservable<Unit> OnTimerEnd => onTimeCountdownEnd;
        public IObservable<float> OnProcessChanged => timeCountValue;
        public IObservable<float> OnRatioProcessChanged => timeCountAtRatio;
        

        private Subject<Unit> onTimeCountdownStart = new Subject<Unit>();
        private Subject<Unit> onTimeCountdownEnd = new Subject<Unit>();

        private FloatReactiveProperty timeCountValue = new FloatReactiveProperty();
        private FloatReactiveProperty timeCountAtRatio = new FloatReactiveProperty();
        IDisposable timeCountdownProcess;
        
        public void StartTimeCountdown(float countTime)
        {
            var time = countTime;
            float timeAtRatio = 0;
            onTimeCountdownStart.OnNext(Unit.Default);
            timeCountdownProcess = Observable.EveryLateUpdate()
                .Subscribe(x =>
                {
                    time -= Time.deltaTime;
                    time = Mathf.Clamp(time, 0, countTime);
                    timeAtRatio = time / countTime;

                    timeCountValue.Value = time;
                    timeCountAtRatio.Value = timeAtRatio;
                    if (time <= 0)
                    {
                        onTimeCountdownEnd.OnNext(Unit.Default);
                        timeCountdownProcess.Dispose();
                    }
                });
        }
    }
}
