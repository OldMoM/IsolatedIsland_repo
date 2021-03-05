using System;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class TimeSystem : MonoBehaviour, ITimeSystem
    {
        [SerializeField]
        private TimeSystemModel model;

        public int day => model.day.Value;
        public int time => model.time.Value;
        public bool isDay => model.isDay;
        public IObservable<int> onDayStart => _onDayStart;
        public IObservable<int> onDayEnd => _onDayEnd;
        public IObservable<int> onTimeChanged => model.time;


        private IDisposable nightTimeCount_thread;
        private Subject<int> _onDayStart = new Subject<int>();
        private Subject<int> _onDayEnd = new Subject<int>();

        private void Start()
        {
            init();
        }

        public void init()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(x => model.isDay)
                .Subscribe(x =>
                {
                    var time = model.time.Value;
                    time++;
                    time = Mathf.Clamp(time, 0, model.dayTime);
                    model.time.Value = time;
                    if (time >= model.dayTime)
                    {
                        model.isDay = false;
                        _onDayEnd.OnNext(day);
                        startNight();
                    }
                });
        }

        void startNight()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(model.nightTime))
                .First()
                .Subscribe(x =>
                {
                    model.time.Value = 0;
                    model.day.Value++;                   
                    model.isDay = true;
                    _onDayStart.OnNext(day);
                }).AddTo(this);
        }
    }
    [Serializable]
    public struct TimeSystemModel
    {
        public IntReactiveProperty day;
        public IntReactiveProperty time;
        public int dayTime;
        public int nightTime;
        public bool isDay;
    }
}
