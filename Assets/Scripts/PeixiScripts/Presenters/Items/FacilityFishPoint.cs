using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi
{
    public class FacilityFishPoint : BaseItem
    {
        public FacilityData facilityData;
        protected override void OnPlayerTouch(Collider other)
        {
            if (other.transform.tag == "Player" && data.isContact == false)
            {
                data.isContact = true;

                getArbitorSysten()
                    .facilityInteractAgent
                    .PlayerTouchFacility(facilityData);
            }
        }
        protected override void OnPlayerUntouch(Collider other)
        {
            if (other.transform.tag == "Player" && data.isContact == true)
            {
                data.isContact = false;

                getArbitorSysten()
                    .facilityInteractAgent
                    .PlayerUntouchFacility(facilityData);
                    
            }
        }
        protected override void init()
        {
            base.init();
            facilityData.position = transform.position;
            facilityData.instanceId = this.GetInstanceID();
        }
    }
}
