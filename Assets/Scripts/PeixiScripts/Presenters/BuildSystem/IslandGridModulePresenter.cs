using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using Extensions;

namespace Peixi
{
    public class IslandGridModulePresenter : MonoBehaviour
    {
        private IBuildSystem system;
        private void OnEnable()
        {
            system = GetComponentInParent<IBuildSystem>();
            system.OnIslandSunk.Subscribe(RemoveIslandAt);
        }
        protected GridModule<Vector2Int,IslandGridData> model = new GridModule<Vector2Int, IslandGridData>();
        public IObservable<DictionaryAddEvent<Vector2Int, IslandGridData>> OnIslandBuilt => model.OnDataAdded;
        public IObservable<DictionaryRemoveEvent<Vector2Int, IslandGridData>> OnIslandRemoved => model.OnDataRemoved;

        public void BuildIslandAt(Vector2Int buildPos,int durability = 100)
        {
            model.AddData(buildPos,
                new IslandGridData(buildPos, true));
        }
        public bool CheckThePositionHasIsland(Vector2Int buildPos)
        {
            return model.CheckHasTheKey(buildPos);
        }
        public void RemoveIslandAt(Vector2Int buildPos)
        {
            //print(buildPos);
            model.RemoveData(buildPos);
        }
        public List<Vector2Int> GetEmptyAdjacentGrids(Vector2Int gridPos)
        {
            var adjacentGrids = new List<Vector2Int>();
            adjacentGrids.Add(gridPos + Vector2Int.left);
            adjacentGrids.Add(gridPos + Vector2Int.right);
            adjacentGrids.Add(gridPos + Vector2Int.up);
            adjacentGrids.Add(gridPos + Vector2Int.down);

            return adjacentGrids
                .Where(x => !CheckThePositionHasIsland(x))
                .ToList();
        }
        protected bool IsContourGrid(Vector2Int gridPos)
        {
            return new List<Vector2Int>()
                .AddItem(gridPos + Vector2Int.left)
                .AddItem(gridPos + Vector2Int.right)
                .AddItem(gridPos + Vector2Int.up)
                .AddItem(gridPos + Vector2Int.down)
                .Where(x => !CheckThePositionHasIsland(x))
                .Count() == 0;
        }
        public List<Vector2Int> GetEmptyAdjacentGrids_Wrong(Vector2Int gridPos)
        {
            var adjacentGrids = new List<Vector2Int>();
            adjacentGrids
                .AddItem(gridPos + Vector2Int.left)
                .AddItem(gridPos + Vector2Int.right)
                .AddItem(gridPos + Vector2Int.up)
                .AddItem(gridPos + Vector2Int.down);

            foreach (var item in adjacentGrids)
            {
                if (CheckThePositionHasIsland(item))
                {
                    adjacentGrids.Remove(item);
                }
            }
            return adjacentGrids;
        }
    }
}

