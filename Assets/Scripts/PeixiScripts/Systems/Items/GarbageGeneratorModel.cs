using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace Peixi
{
    public struct GarbageGeneratorModel
    {
        public BoolReactiveProperty isAcitve;
        public GarbageGeneratorModel(bool active)
        {
            isAcitve = new BoolReactiveProperty(active);
        }
    }
}
