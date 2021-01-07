using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Peixi
{
    public class BuildSystem : MonoBehaviour, IBuildSystem
    {
        public IObservable<int> test;
        IslandGridModulePresenter islandGridModule;
        FacilityModulePresenter facilityModule;
        GameObjectBuiltModuleView view;
        public IObservable<DictionaryAddEvent<Vector2Int, IslandGridData>> OnIslandBuilt => islandGridModule.OnIslandBuilt;
        public IObservable<DictionaryRemoveEvent<Vector2Int, IslandGridData>> OnIslandRemoved => islandGridModule.OnIslandRemoved;
        private Subject<Vector2Int> onIslandSunk = new Subject<Vector2Int>();
        public Subject<Vector2Int> OnIslandSunk => onIslandSunk;

        private void Start()
        {

        }
        private void OnEnable()
        {
            
        }
        public void BuildFacility(Vector2Int gridPos,string facilityName = "workbench")
        {
            facilityModule.BuildFacility(facilityName, gridPos);
        }

        public void BuildFacility(string facilityName, Vector2Int gridPos)
        {
            //throw new NotImplementedException();
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
            print("msg has ben delivered to BuildSystem");
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
        public void SetIslanDurability(Vector2Int islandGridPos, int targetValue)
        {
            
        }
        private void Awake()
        {
            print("init buildsystem at awake");
            islandGridModule = GetComponentInChildren<IslandGridModulePresenter>();
            facilityModule = GetComponentInChildren<FacilityModulePresenter>();
            view = GetComponent<GameObjectBuiltModuleView>();
        }
    }
}
