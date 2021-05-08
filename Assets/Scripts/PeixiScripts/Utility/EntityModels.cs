using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public static class EntityModel
    {
        public static ReactiveDictionary<Vector2Int, IslandModel> islandModels = new ReactiveDictionary<Vector2Int, IslandModel>();
    }
}
