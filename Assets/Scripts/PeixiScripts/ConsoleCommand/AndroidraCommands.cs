using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi
{
    public static class AndroidraCommands 
    {
        public static void BuildAt(string type,Vector2Int pos)
        {
            IAndroidraSystem androidra = InterfaceArichives.Archive.IAndroidraSystem;
            androidra.Control.BuildAt(type, pos);
        }
        public static void RestoreIsland(Vector2Int islandPos)
        {
            IAndroidraSystem androidra = GameObject.FindObjectOfType<AndroidraSystem>();
            androidra.Control.RestoreIsland(islandPos);
        }
    }
}
