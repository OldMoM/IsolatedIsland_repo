using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Peixi;
using Siwei;
using UniRx;

namespace Peixi
{
    /// <summary>
    /// BuildSystem处理和BuildSketch交流的agent
    /// </summary>
    public class BuildSketchAgent
    {
        private IBuildSketch _buildSketch;
        private IBuildSystem _ibuildSystem;

        private bool permitBuildIsland;
        private Vector2Int buildTarget;

        public BuildSketchAgent(IBuildSketch ibuildSketch, IBuildSystem buildSystem)
        {
            Debug.Log("init BuildSketchAgent");
            _buildSketch = ibuildSketch;
            _ibuildSystem = buildSystem;

            ibuildSketch.OnMouseHoverPositionChanged
                .Subscribe(x =>
                {
                    
                    buildTarget = buildSystem.newWorldToGridPosition(x);
                    permitBuildIsland = !buildSystem.CheckThePositionHasIsland(buildTarget);
                    _buildSketch.PermitBuildIsland = permitBuildIsland;
                });

            ibuildSketch.OnMouseClicked
                .Where(x=> permitBuildIsland)
                .Subscribe(x =>
                {
                    buildSystem.BuildIslandAt(buildTarget);
                });
        }
    }
}



