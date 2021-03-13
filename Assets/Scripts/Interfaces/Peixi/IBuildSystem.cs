using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public interface IBuildSystem
    {
        /// <summary>
        /// 向网格中添加Island
        /// </summary>
        /// <param name="gridPos">网格坐标</param>
        void BuildIslandAt(Vector2Int gridPos, int durability = 100);
        /// <summary>
        /// 从网格中移除Island
        /// </summary>
        /// <param name="gridPos">网格坐标/param>
        void RemoveIslandAt(Vector2Int gridPos);
        /// <summary>
        /// 查询网格中是否有Island
        /// </summary>
        /// <param name="gridPos">网格坐标</param>
        /// <returns></returns>
        bool CheckThePositionHasIsland(Vector2Int gridPos);
        /// <summary>
        /// 建造设施
        /// </summary>
        /// <param name="gridPos">建造坐标</param>
        /// <param name="facilityName">设施名称</param>
        void BuildFacility(Vector2Int gridPos, string facilityName);
        /// <summary>
        /// 移除某处的设施
        /// </summary>
        /// <param name="gridPos">移除点的坐标</param>
        void RemoveFacility(Vector2Int gridPos);
        [Obsolete]
        /// <summary>
        /// 世界转换为网格坐标
        /// </summary>
        Func<Vector3,GridSetting, Vector2Int> worldToGridPosition { get; }
        [Obsolete]
        /// <summary>
        /// 网格坐标转换为世界坐标
        /// </summary>
        Func<Vector2Int,GridSetting, Vector3> gridToWorldPosition { get; }
        Func<Vector2Int,Vector3> newGridToWorldPosition { get; }
        Func<Vector3, Vector2Int> newWorldToGridPosition { get; }
        /// <summary>
        /// 获得某处岛块的操作接口
        /// </summary>
        /// <param name="gridpos">岛块坐标</param>
        /// <returns></returns>
        IIsland GetIslandInterface(Vector2Int gridpos);
        /// <summary>
        /// 当向场景中添加Island时触发此事件<summary>
        /// </summary>
        IObservable<DictionaryAddEvent<Vector2Int, IslandGridData>> OnIslandBuilt { get; }
        /// <summary>
        /// 当移除场景中的Island时触发此事件
        /// </summary>
        IObservable<DictionaryRemoveEvent<Vector2Int, IslandGridData>> OnIslandRemoved { get; }
        /// <summary>
        /// 当岛块沉没时触发此事件
        /// </summary>
        IObservable<Vector2Int> OnIslandSunk { get; }
        /// <summary>
        /// 当修建设施时触发此事件
        /// Item1：设施坐标
        /// Item2：设施类型
        /// </summary>
        IObservable<DictionaryAddEvent<Vector2Int, FacilityGridData>> OnFacilityBuilt { get; }
        /// <summary>
        /// 当设施被移除时触发此事件
        /// arg：被移除设施的坐标
        /// </summary>
        IObservable<DictionaryRemoveEvent<Vector2Int, FacilityGridData>> OnFacilityRemoved { get; }
        /// <summary>
        /// 网格设置参数
        /// </summary>
        GridSetting Settings { get; set; }
        FacilityBuildModulePresenter facilityBuildMod { get;}
    }
}

