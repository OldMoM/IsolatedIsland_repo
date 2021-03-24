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
        private IInventorySystem inventorySystem;
        private IChatBubble chatBubble;

        private bool hasIsland;
        private Vector2Int buildTarget;

        private BuildSketchAgentDependency dependency;

        public BuildSketchAgent(IBuildSketch ibuildSketch, IBuildSystem buildSystem,BuildSketchAgentDependency dependency)
        {
            _buildSketch = ibuildSketch;
            _ibuildSystem = buildSystem;
            inventorySystem = InterfaceArichives.Archive.IInventorySystem;
            chatBubble = InterfaceArichives.Archive.InGameUIComponentsManager.ChatBubble;
            this.dependency = dependency;

            ibuildSketch.OnMouseHoverPositionChanged
                .Where(x => _buildSketch.SetBuildMode)
                .Subscribe(x =>
                {
                    buildTarget = buildSystem.newWorldToGridPosition(x);
                    hasIsland = !buildSystem.CheckThePositionHasIsland(buildTarget);
                    _buildSketch.PermitBuildIsland = hasIsland && HasEnoughMat();
                });

            ibuildSketch.OnMouseClicked
                .Where(x => _buildSketch.SetBuildMode)
                .Where(x => hasIsland)
                .Subscribe(x =>
                {
                    if (HasEnoughMat())
                    {
                        dependency.AndroidBuildAt(PrefabTags.plantIsland, buildTarget);
                        ibuildSketch.SetBuildMode = false;
                    }
                    else
                    {
                        chatBubble.StartChat(new string[] { "我没有足够的材料" },
                                             InterfaceArichives.Archive.PlayerSystem.OnPlayerPositionChanged);
                    }
                });

            dependency.onAndroidCompleteBuildIsland
                .Subscribe(x =>
                {
                    buildSystem.BuildIslandAt(buildTarget);
                });
        }
        public bool HasEnoughMat()
        {
            var plastic = inventorySystem.GetAmount("Plastic");
            var _string = inventorySystem.GetAmount("String");

            var hasEoughPlastic = (plastic == 16);
            var hasEnoughString = (_string == 8);

            return hasEnoughString && hasEoughPlastic;
        }
    }
}



