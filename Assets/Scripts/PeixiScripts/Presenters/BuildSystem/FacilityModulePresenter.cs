using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Assertions;

namespace Peixi
{
    /// <summary>
    /// 处理Facility的数据逻辑
    /// </summary>
    public class FacilityModulePresenter : MonoBehaviour
    {
        private GridModule<Vector2Int, FacilityGridData> model = new GridModule<Vector2Int, FacilityGridData>();
        public System.IObservable<DictionaryAddEvent<Vector2Int, FacilityGridData>> OnFacilityAdded => model.OnDataAdded;
        IBuildSystem buildSystem;
        private void Awake()
        {
            buildSystem = GetComponentInParent<IBuildSystem>();
        }
        public void BuildFacility(string name,Vector2Int gridPos)
        {
            var hasIsland = buildSystem.CheckThePositionHasIsland(gridPos);
            Assert.IsTrue(hasIsland, "Facility必须建造在Island上");
            model.AddData(gridPos,new FacilityGridData());
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
        public string FacilityName => facilityName;
        public Vector2Int FacilityPosition => facilityPosition;
    }
}
