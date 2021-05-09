using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi 
{
    public static class Entity
    {
        public static ReactiveDictionary<int, GameObject> garbages = new ReactiveDictionary<int, GameObject>();

        public static GarbageGeneratorModel garbageGeneratorModel = new GarbageGeneratorModel(false);
    }
}
