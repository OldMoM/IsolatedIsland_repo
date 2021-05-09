using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Peixi
{
    public class CollectGarbageTigGui : MonoBehaviour
    {
        public List<FacilityType> handleScope;
        public RawImage icon;
        public bool isActive;

        public GameObject target;
        // Start is called before the first frame update
        void Start()
        {
            var interactionHandle = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;

            interactionHandle.onTargetChanged
                .Where(x => handleScope.Contains(x.type))
                .Subscribe(x =>
                {
                    isActive = true;

                    icon.enabled = isActive;
                    target = Entity.garbages[x.instanceId];
                });

            interactionHandle.onTargetChanged
                .Where(x => x.type == FacilityType.None)
                .Subscribe(x =>
                {
                    isActive = false;
                    icon.enabled = isActive;
                    target = null;
                });

            Observable.EveryLateUpdate()
                .Where(x => isActive)
                .Where(x=> target != null)
                .Subscribe(x =>
                {
                    var worldPos = target.transform.position;
                    var screenPos = FindObjectOfType<Camera>().WorldToScreenPoint(worldPos);
                    var offset_screen = new Vector3(0, 200, 0);
                    transform.position = screenPos + offset_screen;

                }); 
        }
    }
}

