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
        public GridSetting settings = new GridSetting(3, Vector2.zero);

        #region//privates
        private IslandGridModulePresenter _islandGridModule;
        private FacilityModulePresenter _facilityModule;
        private GameObjectBuiltModuleView _view;
        private PositionConvent _posTransMod = new PositionConvent();
        private Subject<Vector2Int> _onIslandSunk = new Subject<Vector2Int>();
        #endregion

        #region IObservables
        public IObservable<DictionaryAddEvent<Vector2Int, IslandGridData>> OnIslandBuilt => _islandGridModule.OnIslandBuilt;
        public IObservable<DictionaryRemoveEvent<Vector2Int, IslandGridData>> OnIslandRemoved => _islandGridModule.OnIslandRemoved;
        public IObservable<Vector2Int> OnIslandSunk => _onIslandSunk;
        public IObservable<DictionaryAddEvent<Vector2Int, FacilityGridData>> OnFacilityBuilt => _facilityModule.OnFacilityAdded;
        public IObservable<DictionaryRemoveEvent<Vector2Int, FacilityGridData>> OnFacilityRemoved => _facilityModule.OnFacilityRemoved;

        #endregion

        #region//Methods
        public void BuildFacility(Vector2Int gridPos,string facilityName = "workbench")
        {
            _facilityModule.BuildFacility(facilityName, gridPos);
        }
        public void BuildFacility(string facilityName, Vector2Int gridPos)
        {
            
        }
        public void BuildIslandAt(Vector2Int gridPos,int durability = 100)
        {
            Assert.IsNotNull(_islandGridModule);
            _islandGridModule.BuildIslandAt(gridPos,durability);
        }
        public bool CheckThePositionHasIsland(Vector2Int gridPos)
        {
            Assert.IsNotNull(_islandGridModule);
            var hasIsland = _islandGridModule.CheckThePositionHasIsland(gridPos);
            return hasIsland;
        }
        public IIsland GetIslandInterface(Vector2Int gridpos)
        {
            return _view.GetIslandInterface(gridpos);
        }
        public void RemoveFacility(Vector2Int gridPos)
        {
            _facilityModule.RemoveFacility(gridPos);
        }
        public void RemoveIslandAt(Vector2Int gridPos)
        {
            Assert.IsNotNull(_islandGridModule);
            _islandGridModule.RemoveIslandAt(gridPos);

        }
        public Func<Vector3, GridSetting,Vector2Int> worldToGridPosition => _posTransMod.WorldToGridPosition;
        public Func<Vector2Int,GridSetting, Vector3> gridToWorldPosition => _posTransMod.GridToWorldPosition;
        public GridSetting Settings => settings;
        #endregion

        private void Awake()
        {
            _islandGridModule = GetComponentInChildren<IslandGridModulePresenter>();
            _facilityModule = GetComponentInChildren<FacilityModulePresenter>();
            _view = GetComponent<GameObjectBuiltModuleView>();
        }
    }
    public struct PositionConvent 
    {
        public Vector2Int WorldToGridPosition(Vector3 worldPosition, GridSetting gridSetting)
        {
            var _size = gridSetting._cellSize;
            var _halfSize = _size / 2;

            var gridX_temp = (worldPosition.x + _halfSize) / _size;
            var gridX = Mathf.Floor(gridX_temp);

            var gridY_temp = (worldPosition.z + _halfSize) / _size;
            var gridY = Mathf.Floor(gridY_temp);

            return new Vector2Int(
                Convert.ToInt32(gridX),
                Convert.ToInt32(gridY));
        }
        public Vector3 GridToWorldPosition(Vector2Int gridPosition,GridSetting gridSetting)
        {
            var size = gridSetting._cellSize;
            var worldPos_x = gridPosition.x * size;
            var worldPos_z = gridPosition.y * size;
            return new Vector3(worldPos_x, 0, worldPos_z);
        }
    }

    [Serializable]
    /// <summary>
    /// 网格参数设置
    /// </summary>
    public struct GridSetting
    {
        /// <summary>
        /// 网格宽度
        /// </summary>
        public readonly float _cellSize;
        /// <summary>
        /// 网格起始坐标
        /// </summary>
        public readonly Vector2 _origin;
        public GridSetting(float cellSize,Vector2 origin)
        {
            _cellSize = cellSize;
            _origin = origin;
        }
    }
}
