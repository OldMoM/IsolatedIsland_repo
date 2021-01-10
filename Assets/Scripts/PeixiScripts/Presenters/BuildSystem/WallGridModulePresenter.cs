using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Extensions;
using System.Linq;

namespace Peixi
{
    /// <summary>
    /// 处理空气墙网格的数据逻辑
    /// </summary>
    public class WallGridModulePresenter : MonoBehaviour
    {
        protected IBuildSystem iBuildSystem;
        protected GridModule<Vector2Int, WallGridData> model = new GridModule<Vector2Int, WallGridData>();
        public IObservable<DictionaryAddEvent<Vector2Int, WallGridData>> OnWallAdded => model.OnDataAdded;
        public IObservable<DictionaryRemoveEvent<Vector2Int, WallGridData>> OnWallRemoved => model.OnDataRemoved;
        public IBuildSystem IBuildSystem
        {
            set { iBuildSystem = value; }
        }
        // Start is called before the first frame update
        private void Awake()
        {
            iBuildSystem = GetComponentInParent<IBuildSystem>();
        }
        private void Start()
        {
            iBuildSystem.OnIslandBuilt
                .Subscribe(OnIslandAdded);

            iBuildSystem.OnIslandRemoved
                .Subscribe(OnIslandRemoved);
        }
        protected WallGridModulePresenter BuildInvisibleWallAt(Vector2Int gridPos)
        {
            model.AddData(gridPos,
                new WallGridData(gridPos,true)
                );
            return this;
        }
        protected bool CheckTheGridHasWall(Vector2Int gridPos)
        {
            return model.CheckHasTheKey(gridPos); 
        }
        protected WallGridModulePresenter RemoveWallFrom(Vector2Int gridPos)
        {
            model.RemoveData(gridPos);
            return this;
        }
        private List<Vector2Int> GetAdjacentGrids(Vector2Int islandPos)
        {
            return
                new List<Vector2Int>()
                .AddItem(islandPos + Vector2Int.right)
                .AddItem(islandPos + Vector2Int.down)
                .AddItem(islandPos + Vector2Int.left)
                .AddItem(islandPos + Vector2Int.up)
                .AddItem(islandPos);
        }
        protected bool IsAdjacentWithIsland(Vector2Int gridPos)
        {
            var adjacentGrids = new List<Vector2Int>()
                .AddItem(gridPos + Vector2Int.right)
                .AddItem(gridPos + Vector2Int.down)
                .AddItem(gridPos + Vector2Int.left)
                .AddItem(gridPos + Vector2Int.up);

            var isAjacent = adjacentGrids
                .Where(x => iBuildSystem.CheckThePositionHasIsland(x))
                .ToList().Count > 0;

            return isAjacent;
        }
        protected void OnIslandAdded(DictionaryAddEvent<Vector2Int,IslandGridData> data)
        {
            var t1 = GetAdjacentGrids(data.Key)
                .ToObservable();

            t1.Where(x => CheckTheGridHasWall(x))
                .Subscribe(y =>
                {
                    RemoveWallFrom(y);
                });

            t1.Where(x => !iBuildSystem.CheckThePositionHasIsland(x))
                .Subscribe(y =>
                {
                    BuildInvisibleWallAt(y);
                });
        }
        protected void OnIslandRemoved(DictionaryRemoveEvent<Vector2Int,IslandGridData> data)
        {
            var buffer1 = GetAdjacentGrids(data.Key).ToObservable();

            buffer1
                .Where(x => CheckTheGridHasWall(x))
                .Subscribe(y => RemoveWallFrom(y));

            buffer1
                  .Where(x => IsAdjacentWithIsland(x))
                  .Where(z => !iBuildSystem.CheckThePositionHasIsland(z))
                  .Subscribe(y =>
                  {
                      BuildInvisibleWallAt(y);
                  }); 
        }
    }
}
