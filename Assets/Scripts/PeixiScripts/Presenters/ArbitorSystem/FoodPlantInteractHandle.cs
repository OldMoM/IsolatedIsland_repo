using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class FoodPlantInteractHandle
    {
        public IObservable<Unit> onInteractStart => _onInteractStart;
        public IObservable<Unit> onInteractEnd => _onInteractEnd;

        private Subject<Unit> _onInteractStart = new Subject<Unit>();
        private Subject<Unit> _onInteractEnd = new Subject<Unit>();
        public void EndInteract()
        {
            Debug.Log("player end interaction with food plant");
        }
        public void StartInteract()
        {
            Debug.Log("player start interaction with food plant");
            EndInteract();
        }
    }
}
