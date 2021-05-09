using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace Peixi
{
    public class GarbageCollect:MonoBehaviour
    {
        private CollectableObjectAgent collectable;
        public FacilityData data;
        // Start is called before the first frame update
        void Start()
        {
            collectable = new CollectableObjectAgent(GetComponent<SphereCollider>());
            data.position = transform.position;
            data.gridPos = InterfaceArichives.Archive.IBuildSystem.newWorldToGridPosition(transform.position);
            data.instanceId = gameObject.GetInstanceID();
            data.attachedTransfom = transform;

            collectable.OnPlayerTouch
                .Subscribe(x =>
                {
                    var facilityInteract = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
                    facilityInteract.PlayerTouchFacility(data);
                });

            collectable.OnPlayerUntouch
                .Subscribe(x =>
                {
                    var facilityInteract = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
                    facilityInteract.PlayerUntouchFacility(data);
                });
        }
    }
}
