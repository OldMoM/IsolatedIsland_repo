using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi.Commands
{
    public static class BuildSystemCommands
    {
        public static void BuildIslandAt(Vector2Int gridPos)
        {
            IBuildSystem buildSystem = GameObject.FindObjectOfType<BuildSystem>();
            buildSystem.BuildIslandAt(gridPos);
        }
        public static void RemoveIslandAt(Vector2Int gridPos)
        {
            IBuildSystem buildSystem = GameObject.FindObjectOfType<BuildSystem>();
            buildSystem.RemoveIslandAt(gridPos);
        }
        public static void BuildFacilityAt(Vector2Int gridPos, string type)
        {
            IBuildSystem buildSystem = GameObject.FindObjectOfType<BuildSystem>();
            buildSystem.BuildFacility(gridPos, type);
        }
        public static void BuildIslandsBy(Vector2Int size)
        {
            IBuildSystem buildSystem = GameObject.FindObjectOfType<BuildSystem>();

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    buildSystem.BuildIslandAt(new Vector2Int(i, j));
                }
            }
        }
        public static void SetIslandDurability(Vector2Int islandGridPos, int durability)
        {
            IBuildSystem ibuildSystem = GameObject.FindObjectOfType<BuildSystem>();
            var iisland = ibuildSystem.GetIslandInterface(islandGridPos);
            iisland.SetDurabilityTo(durability);
        }
    }
}
