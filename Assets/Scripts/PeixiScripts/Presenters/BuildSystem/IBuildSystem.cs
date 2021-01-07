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
        void BuildIslandAt(Vector2Int gridPos,int durability = 100);
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
        /// 当向场景中添加Island时触发此事件
        /// </summary>
        IObservable<DictionaryAddEvent<Vector2Int, IslandGridData>> OnIslandBuilt { get; }
        /// <summary>
        /// 当移除场景中的Island时触发此事件
        /// </summary>
        IObservable<DictionaryRemoveEvent<Vector2Int, IslandGridData>> OnIslandRemoved { get; }
        void BuildFacility(Vector2Int gridPos, string facilityName );
        void RemoveFacility(Vector2Int gridPos);
        IIsland GetIslandInterface(Vector2Int gridpos);
        void SetIslanDurability(Vector2Int islandGridPos, int targetValue);
        Subject<Vector2Int> OnIslandSunk { get;}
    }
}
