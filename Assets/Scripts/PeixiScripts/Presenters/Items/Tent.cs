using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class Tent : MonoBehaviour
    {
        private CollectableObjectAgent collectableAgent;
        private FacilityInteractionAgent interactAgent;
        public FacilityData data;
        public CollectableObjectAgent CollectableAgent => collectableAgent;


        private FacilityData createFacilityData
        {
            get
            {
                data.instanceId = this.GetInstanceID();
                data.name = "Tent";
                data.position = transform.position;
                data.type = FacilityType.Tent;

                return data;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            var collider = GetComponent<SphereCollider>();
            collectableAgent = new CollectableObjectAgent(collider);
            data = createFacilityData;
            interactAgent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
 
            collectableAgent.OnPlayerTouch.Subscribe(x =>
            {
                interactAgent.PlayerTouchFacility(data);
            });

            collectableAgent.OnPlayerUntouch.Subscribe(x =>
            {
                interactAgent.PlayerUntouchFacility(data);
            });
        }

    }
}
