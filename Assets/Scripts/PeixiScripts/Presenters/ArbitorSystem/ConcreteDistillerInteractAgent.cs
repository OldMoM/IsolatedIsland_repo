using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public class ConcreteDistillerInteractAgent
    {
        public IObservable<Unit> OnInteractStart => timer.OnTimerStart;
        public IObservable<Unit> OnInteractEnd => timer.OnTimerEnd;
        public IObservable<float> OnTimeCountValueChanged => timer.OnProcessChanged;
        public IObservable<float> OnRatioTimeChanged => timer.OnRatioProcessChanged;

        public float distillCostTime = 3f;

        private IInventorySystem InventorySystem => InterfaceArichives.Archive.IInventorySystem;

        ConcurrentTimer timer = new ConcurrentTimer();

        public ConcreteDistillerInteractAgent()
        {
            timer.OnTimerEnd
                .Subscribe(x =>
                {
                    InventorySystem.AddItem(ItemTags.water);
                });
        }
        public void StartInteract()
        {
            timer.StartTimeCountdown(distillCostTime);
        }
    }
}
