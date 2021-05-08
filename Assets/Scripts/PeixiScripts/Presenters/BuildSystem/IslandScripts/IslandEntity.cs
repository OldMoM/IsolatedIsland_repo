using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi
{
    public class IslandEntity : MonoBehaviour
    {
        public IslandModel model;
        public void Init(Vector2Int pos,int durability = 100)
        {
            model = EntityModelFactory.CreateIslandModel();

            model.onDayEnd = InterfaceArichives.Archive.ITimeSystem.onDayEnd;
            model.onDayStart = InterfaceArichives.Archive.ITimeSystem.onDayStart;
            model.attachedObject = gameObject;

            IslandPresenterNew.Init(ref model);
        }
    }
}
