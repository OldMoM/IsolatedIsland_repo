using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi
{
    public struct FacilityInteractConditonDiscriminator 
    {
        private FacilityInteractionAgent agent;
        public FacilityInteractConditonDiscriminator(FacilityInteractionAgent agent)
        {
            this.agent = agent;
        }
        public bool FishPointInteractCondition
        {
            get
            {
                return true;
            }
        }
        public bool FoodPointInteractCondition
        {
            get
            {
                return true;
            }
        }
        public bool DistillerInteractCondition
        {
            get
            {
                return true;
            }
        }
    }
}
