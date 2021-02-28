using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Assertions;

namespace Peixi
{
    public class FacilityBuildModulePresenter
    {
        private GridModule<Vector2Int, FacilityGridData> model = new GridModule<Vector2Int, FacilityGridData>();
        public IObservable<DictionaryAddEvent<Vector2Int, FacilityGridData>> OnFacilityAdded => model.OnDataAdded;
        public IObservable<DictionaryRemoveEvent<Vector2Int, FacilityGridData>> OnFacilityRemoved => model.OnDataRemoved;
        IBuildSystem buildSystem;

        public FacilityBuildModulePresenter(IBuildSystem buildSystem)
        {
            this.buildSystem = buildSystem;
        }

        public void BuildFacility(string type,Vector2Int gridPos)
        {
            Debug.Log(type);
            //var hasIsland = buildSystem.CheckThePositionHasIsland(gridPos);
            //Assert.IsTrue(hasIsland, "Facility必须建造在Island上");

            var facilityData = new FacilityGridData();
            facilityData.type = type;
            facilityData.position = gridPos;
            //facilityData.FacilityName = gridPos;


            model.AddData(gridPos,facilityData);
        }
        public bool HasFacility(Vector2Int gridPos)
        {
            return model.CheckHasTheKey(gridPos);
        }
        public void RemoveFacility(Vector2Int gridPos)
        {
            model.RemoveData(gridPos);
        }
    }
    /// <summary>
    /// Facility在Ground数据库中的储存模式
    /// </summary>
    public struct FacilityGridData
    {
        private Vector2Int facilityPosition;
        private string facilityName;
        [Obsolete]
        public string FacilityName => facilityName;
        [Obsolete]
        public Vector2Int FacilityPosition => facilityPosition;
        public string type;
        public Vector2Int position;
    }
}
