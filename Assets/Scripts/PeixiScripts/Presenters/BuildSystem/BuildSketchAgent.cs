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

        public BuildSketchAgent(IBuildSketch ibuildSketch, IBuildSystem buildSystem)
        {
            Debug.Log("init BuildSketchAgent");
            _buildSketch = ibuildSketch;
            _ibuildSystem = buildSystem;

            _buildSketch.PermitBuildIsland = false;

            Debug.Log("_buildSketch.PermitBuildIsland is " + _buildSketch.PermitBuildIsland);

            ibuildSketch.OnMouseHoverPositionChanged
                .Subscribe(x =>
                {
                    var gridPos = buildSystem.newWorldToGridPosition(x);
                    var hasIsland = buildSystem.CheckThePositionHasIsland(gridPos);
                    _buildSketch.PermitBuildIsland = hasIsland;
                });
        }
        void OnMouseHoverPositionChanged()
        {

        }
        void OnMouseClicked()
        {

        }
    }
}



