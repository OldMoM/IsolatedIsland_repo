using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    /// <summary>
    /// 处理玩家和设施之间的交互行为
    /// </summary>
    public class FacilityInteractionAgent
    {
        public FacilityData targetData => _targetData.Value;
        public IObservable<FacilityType> onInteractStart => _onInteractStart;
        public IObservable<FacilityType> onInteractEnd => _onInteractEnd;
        public IObservable<FacilityData> onTargetChanged => _targetData;
        public IObservable<InteractState> onStateChanged => _state;
        public FishPointInteractHandleUnit fishUnit => _fishPointUnit;
        public InteractState state => _state.Value;

        private ReactiveProperty<FacilityData> _targetData = new ReactiveProperty<FacilityData>();
        private ReactiveProperty<InteractState> _state = new ReactiveProperty<InteractState>(InteractState.Idle);
        private BoolReactiveProperty _isActive = new BoolReactiveProperty(false);
        private Subject<FacilityType> _onInteractStart = new Subject<FacilityType>();
        private Subject<FacilityType> _onInteractEnd = new Subject<FacilityType>();
        private List<FacilityData> _pendingItems = new List<FacilityData>();
        private Dictionary<FacilityType, Action> _startInteractCode = new Dictionary<FacilityType, Action>();

        private FishPointInteractHandleUnit _fishPointUnit = new FishPointInteractHandleUnit();

        public void PlayerTouchFacility(FacilityData facility)
        {
            var result = _pendingItems.Contains(facility);

            if (!result)
            {
                _pendingItems.Add(facility);
                _state.Value = InteractState.Contact;
                _targetData.Value = facility;
            }
        }
        public void PlayerUntouchFacility(FacilityData facility)
        {
            var result = _pendingItems.Contains(facility);
            if (result)
            {
                _pendingItems.Remove(facility);

                var count = _pendingItems.Count;
                if (count > 0)
                {
                    _targetData.Value = _pendingItems[count - 1];
                }
                else
                {
                    _state.Value = InteractState.Idle;

                    _targetData.Value = new FacilityData();

                }
            }
        }
        public void SwitchTarget()
        {
            var count = _pendingItems.Count;
            if (count > 0)
            {
                var removedOne = _pendingItems[count - 1];
                _pendingItems.RemoveAt(count - 1);
                _pendingItems.Insert(0, removedOne);
                _targetData.Value = _pendingItems[count - 1];
            }
        }
        public void InteractStart(FacilityType type)
        {
            if (type == FacilityType.FishPoint)
            {
                _state.Value = InteractState.Interact;
                fishUnit.startInteract();
            }
        }
        public void InteractEnd(FacilityType type)
        {
            if (type == FacilityType.FishPoint)
            {
                _state.Value = InteractState.Idle;
            }
        }
        private void onSwitchBtnPressed()
        {
            InputSystem.Singleton.onSwitchBtnPressed
               .Where(x => _state.Value == InteractState.Contact)
               .Subscribe(x =>
               {
                   SwitchTarget();
               });
        }
        private void onInteractBtnPressed()
        {
            InputSystem.Singleton.OnInteractBtnPressed
                .Where(x => _state.Value == InteractState.Contact)
                .Subscribe(x =>
                {
                    _state.Value = InteractState.Interact;
                    _startInteractCode[targetData.type]();
                });
        }
        private void onFishingEnd()
        {
            fishUnit.onInteractEnd
                .Subscribe(x =>
                {
                    if (_pendingItems.Count > 1)
                    {
                        _state.Value = InteractState.Contact;
                    }
                    else
                    {
                        _state.Value = InteractState.Idle;
                    }
                });
        }
        private FacilityInteractionAgent React(Action action)
        {
            action();
            return this;
        }
        private FacilityInteractionAgent Init()
        {
            _startInteractCode.Add(FacilityType.FishPoint, fishUnit.startInteract);
            return this;
        }
        public FacilityInteractionAgent()
        {
            Init()
                .React(onSwitchBtnPressed)
                .React(onInteractBtnPressed)
                .React(onFishingEnd);
        }
    }
    [Serializable]
    public struct FacilityData
    {
        public Vector3 position;
        public int instanceId;
        public string name;
        public FacilityType type;
    }
    public enum FacilityType
    {
        None,
        Island,
        FishPoint
    }
    public enum InteractState
    {
        Idle,
        Contact,
        Interact
    }
}
