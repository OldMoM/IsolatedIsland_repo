using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class IslandInteractAgent
    {
        public IObservable<Vector2Int> OnContactingBrokenIslandPosChanged => brokenIslandPos;
        public IObservable<bool> OnContactBrokenIslandChanged => isContactBrokenIsland;
        public Vector2Int ContactBrokenIsland => brokenIslandPos.Value;
        public bool IsContactBrokenIsland => isContactBrokenIsland.Value;

        private IObservable<Vector3> _onPlayerPosChanged;
        private ReactiveProperty<Vector2Int> brokenIslandPos = new ReactiveProperty<Vector2Int>();
        private BoolReactiveProperty isContactBrokenIsland = new BoolReactiveProperty();

        IBuildSystem buildSystem;
  
        FacilityInteractionAgent interactionAgent;
        private bool IsBrokenIsland(Vector2Int islandPos,ref IIsland island)
        {
            var hasIsland = buildSystem.CheckThePositionHasIsland(islandPos);
            bool isBroken = false;
            if (hasIsland)
            {
                island = buildSystem.GetIslandInterface(islandPos);
                isBroken = island.Durability_current < 100;
            }
            return isBroken;
        }
        public IslandInteractAgent(IObservable<Vector3> onPlayerPosChanged)
        {
            buildSystem = InterfaceArichives.Archive.IBuildSystem;
            interactionAgent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
            _onPlayerPosChanged = onPlayerPosChanged;

            _onPlayerPosChanged.Subscribe(x =>
            {
                var gridPos = buildSystem.newWorldToGridPosition(x);
                brokenIslandPos.Value = gridPos;
            });

            brokenIslandPos
                .Subscribe(x =>
                {
                    IIsland island = null;
                    isContactBrokenIsland.Value = IsBrokenIsland(x,ref island);
                    if (isContactBrokenIsland.Value)
                    {
                        var islandData = new FacilityData();
                        islandData.gridPos = x;
                        islandData.instanceId = island.GetHashCode();
                        islandData.position = island.PositionInWorld;
                    }
                });
        }
    }
}
