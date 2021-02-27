using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Peixi
{
    public class FacilityDistillerPresenter : MonoBehaviour
    {
        private FacilityInteractionAgent agent;
        private SphereCollider trigger;
        [SerializeField]
        FoodPlantModel model;
        // Start is called before the first frame update
        void Start()
        {
            Config()
                .React(OnPlayerTouched)
                .React(OnPlayerUntouched);
        }
        FacilityDistillerPresenter Config()
        {
            agent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
            trigger = GetComponent<SphereCollider>();

            model.instanceId = GetInstanceID();
            model.name = transform.name;
            model.position = transform.position;

            return this;
        }
        FacilityDistillerPresenter React(Action action)
        {
            action();
            return this;
        }
        void OnPlayerTouched()
        {
            ObservableTriggerExtensions.OnTriggerEnterAsObservable(trigger)
                .Where(x => x.tag == "Player" && !model.isContact.Value)
                .Subscribe(x =>
                {
                    model.isContact.Value = true;
                    agent.PlayerTouchFacility(model.facilityData);
                });
        }
        void OnPlayerUntouched()
        {
            ObservableTriggerExtensions.OnTriggerExitAsObservable(trigger)
                .Where(x => x.tag == "Player" && model.isContact.Value)
                .Subscribe(x =>
                {
                    model.isContact.Value = false;
                    agent.PlayerUntouchFacility(model.facilityData);
                });
        }
    }
    public struct DistillerModel
    {
        public BoolReactiveProperty isContact;
        public Vector3 position;
        public int instanceId;
        public string name;
        public FacilityType type;
        public FacilityData facilityData
        {
            get
            {
                var data = new FacilityData();
                data.instanceId = this.instanceId;
                data.name = this.name;
                data.position = this.position;
                data.type = this.type;
                return data;
            }
        }
    }
}
