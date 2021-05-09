using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace Peixi
{
    public static class InteractionCommands
    {
        public static void BuildFacility(Vector2Int gridPos)
        {
            var worldPos = InterfaceArichives.Archive.IBuildSystem.newGridToWorldPosition(gridPos);

            var table = new Hashtable();
            var onMouseClicked = new Subject<(string, Vector3)>();

            table.Add("onMouseClicked", onMouseClicked);
            BuildFacilityAgent.Init(table);

            onMouseClicked.OnNext((PrefabTags.fishPoint, worldPos));
        }
    }
}
