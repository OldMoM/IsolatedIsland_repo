using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("BuildSystem",
                 "BuildIslnd",
                 "")]
    public class BuildIsland : Command
    {
        public Vector2Int[] buildPosition;
        public override void OnEnter()
        {
            var ibuildSystem = InterfaceArichives.Archive.IBuildSystem;

            foreach (var pos in buildPosition)
            {
                ibuildSystem.BuildIslandAt(pos, 100);
            }
        }
    }
}
