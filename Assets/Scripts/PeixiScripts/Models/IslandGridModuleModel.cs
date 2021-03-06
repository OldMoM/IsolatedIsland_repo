﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class IslandGridModuleModel
    {
        public ReactiveDictionary<Vector2Int, IslandGridData> reactiveGridData = new ReactiveDictionary<Vector2Int, IslandGridData>();
    }
    public class WallGridModuleModel
    {
        public ReactiveDictionary<Vector2Int, WallGridData> reactiveGridData = new ReactiveDictionary<Vector2Int, WallGridData>();
    }
    public struct WallGridData
    {
        private bool m_hasWall;
        private Vector2Int m_gridPos;
        public bool HasWall => m_hasWall;
        public Vector2Int GridPosition => m_gridPos;
        public WallGridData(Vector2Int gridPos,bool hasWall)
        {
            m_hasWall = hasWall;
            m_gridPos = gridPos;
        }
    }
    public struct IslandGridData
    {
        public IslandGridData(Vector2Int pos,
            bool hasIsland,
            string islandType = "Soil")
        {
            m_hasIsland = hasIsland;
            m_pos = pos;
            m_islandType = islandType;
        }
        public Vector2Int m_pos;
        public bool m_hasIsland;
        public string m_islandType;
    }
}