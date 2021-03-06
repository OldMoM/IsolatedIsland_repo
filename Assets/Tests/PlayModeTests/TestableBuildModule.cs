﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using UniRx;

public class TestableBuildModule : GameObjectBuiltModuleView
{
    public bool HasIslandAt(Vector2Int gridPos)
    {
        return islandSquares.ContainsKey(gridPos);
    }
    public GameObject BuildIsland(Vector2Int gridPos)
    {
        CreateIslandCube(gridPos);
        return islandSquares[gridPos];
    }
    public void RemoveIsland(Vector2Int gridPos)
    {
        RemoveIslandAt(gridPos);
    }
}
