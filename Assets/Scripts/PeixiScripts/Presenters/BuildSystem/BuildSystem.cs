using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Peixi
{
    /// <summary>
    /// 将土地网格、岛块网格和空气墙网格集成
    /// </summary>

    public class BuildSystem : MonoBehaviour, IBuildSystem
    {
        public IObservable<int> test;
        IslandGridModulePresenter islandGridModule;
        FacilityModulePresenter facilityModule;
        GameObjectBuiltModuleView view;

        private Subject<Vector2Int> onIslandSunk = new Subject<Vector2Int>();
        public IObservable<DictionaryAddEvent<Vector2Int, IslandGridData>> OnIslandBuilt => islandGridModule.OnIslandBuilt;
        public IObservable<DictionaryRemoveEvent<Vector2Int, IslandGridData>> OnIslandRemoved => islandGridModule.OnIslandRemoved;
        public IObservable<Vector2Int> OnIslandSunk => throw new NotImplementedException();

        public IObservable<Tuple<Vector2Int, string>> OnFacilityBuilt => throw new NotImplementedException();

        public IObservable<Vector2Int> OnFacilityRemoved => throw new NotImplementedException();

        public void BuildFacility(Vector2Int gridPos,string facilityName = "workbench")
        {
            facilityModule.BuildFacility(facilityName, gridPos);
        }
        public void BuildFacility(string facilityName, Vector2Int gridPos)
        {
            
        }
        public void BuildIslandAt(Vector2Int gridPos,int durability = 100)
        {
            Assert.IsNotNull(islandGridModule);
            islandGridModule.BuildIslandAt(gridPos,durability);
        }
        public bool CheckThePositionHasIsland(Vector2Int gridPos)
        {
            Assert.IsNotNull(islandGridModule);
            var hasIsland = islandGridModule.CheckThePositionHasIsland(gridPos);
            return hasIsland;
        }
        public IIsland GetIslandInterface(Vector2Int gridpos)
        {
            return view.GetIslandInterface(gridpos);
        }
        public void RemoveFacility(Vector2Int gridPos)
        {
            facilityModule.RemoveFacility(gridPos);
        }
        public void RemoveIslandAt(Vector2Int gridPos)
        {
            Assert.IsNotNull(islandGridModule);
            islandGridModule.RemoveIslandAt(gridPos);

        }

        private void Awake()
        {
            islandGridModule = GetComponentInChildren<IslandGridModulePresenter>();
            facilityModule = GetComponentInChildren<FacilityModulePresenter>();
            view = GetComponent<GameObjectBuiltModuleView>();
        }
    }
}
