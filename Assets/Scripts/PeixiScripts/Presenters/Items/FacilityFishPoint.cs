using UnityEngine;
using UniRx.Triggers;
using UniRx;

namespace Peixi
{
    public class FacilityFishPoint : MonoBehaviour
    {
  
        public FacilityData facilityData;

        public FishPointModel model;

        private FacilityInteractionAgent agent;
        private SphereCollider trigger;

        private void Start()
        {
            facilityData.position = transform.position;
            print(transform.position);
            facilityData.instanceId = this.GetInstanceID();
            trigger = GetComponent<SphereCollider>();

            model.isContact = new BoolReactiveProperty();

            agent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;


            OnPlayerTouched();
            OnPlayerUntouched();
        }

        void OnPlayerTouched()
        {
            ObservableTriggerExtensions.OnTriggerEnterAsObservable(trigger)
                .Where(x => x.tag == "Player" && !model.isContact.Value)
                .Subscribe(x =>
                {
                    model.isContact.Value = true;
                    agent.PlayerTouchFacility(facilityData);
                });
        }
        void OnPlayerUntouched()
        {
            ObservableTriggerExtensions.OnTriggerExitAsObservable(trigger)
                .Where(x => x.tag == "Player" && model.isContact.Value)
                .Subscribe(x =>
                {
                    model.isContact.Value = false;
                    agent.PlayerUntouchFacility(facilityData);
                });
        }
    }

    public struct FishPointModel
    {
        public BoolReactiveProperty isContact;
    }
}
