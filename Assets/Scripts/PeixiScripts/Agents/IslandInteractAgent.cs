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
        private bool IsBrokenIsland(Vector2Int islandPos)
        {
            var hasIsland = buildSystem.CheckThePositionHasIsland(islandPos);
            bool isBroken = false;
            if (hasIsland)
            {
                var island = buildSystem.GetIslandInterface(islandPos);
                isBroken = island.Durability_current < 50;
            }
            return isBroken;
        }
        public IslandInteractAgent(IObservable<Vector3> onPlayerPosChanged)
        {
            buildSystem = InterfaceArichives.Archive.IBuildSystem;
            _onPlayerPosChanged = onPlayerPosChanged;

            _onPlayerPosChanged.Subscribe(x =>
            {
                var gridPos = buildSystem.newWorldToGridPosition(x);
                brokenIslandPos.Value = gridPos;
            });

            brokenIslandPos
                .Subscribe(x =>
                {
                    isContactBrokenIsland.Value = IsBrokenIsland(x);
                });
        }
    }
}
