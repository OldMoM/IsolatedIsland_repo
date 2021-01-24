using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class BuildSystemTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void IslandGridModule_CheckThePositionHasIsland_x0y0_true()
        {
            var grid = new IslandGridModulePresenter();
            grid.BuildIslandAt(Vector2Int.zero);
            var hasIsland = grid.CheckThePositionHasIsland(Vector2Int.zero);
            Assert.IsTrue(hasIsland);
        }
        [Test]
        public void IslandGridModule_RemoveIslandAt_x2y1_false()
        {
            var grid = new IslandGridModulePresenter();
            var pos = new Vector2Int(2, 1);
            grid.BuildIslandAt(pos);
            var hasIslandAtx2y1 = grid.CheckThePositionHasIsland(pos);
            Assert.IsTrue(hasIslandAtx2y1);
            grid.RemoveIslandAt(pos);
            hasIslandAtx2y1 = grid.CheckThePositionHasIsland(pos);
            Assert.IsFalse(hasIslandAtx2y1);
        }
        [Test]
        public void IslandGridModule_OnIslandBuilt_true()
        {
            var grid = GameObject.FindObjectOfType<IslandGridModulePresenter>();
            var hasIslandAtx2y3 = false;
            grid.OnIslandBuilt
                .Subscribe(x =>
                {
                    hasIslandAtx2y3 = x.Value.m_hasIsland;
                });
            grid.BuildIslandAt(new Vector2Int(2, 3));
            Assert.IsTrue(hasIslandAtx2y3);
        }
        [Test]
        public void IslandGridModule_OnIslandRemoved_false()
        {
            var grid = GameObject.FindObjectOfType<IslandGridModulePresenter>();
            var pos = new Vector2Int(1, 3);
            grid.BuildIslandAt(pos);
            grid.RemoveIslandAt(pos);
            var hasIslandAtx1y3 = grid.CheckThePositionHasIsland(pos);
            Assert.IsFalse(hasIslandAtx1y3);
        }
        [Test]
        public void GetEmptyAdjacentGrids_CheckEdgeGrid_1()
        {
            var testObject = new GameObject();
            var grid = testObject.AddComponent<IslandGridModulePresenter>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid.BuildIslandAt(new Vector2Int(i,j));
                }
            }
            var n = grid.GetEmptyAdjacentGrids(new Vector2Int(0, 1)).Count;
            Assert.AreEqual(1, n);
        }
        [Test]
        public void GetEmptyAdjacentGrids_CheckCornerGrid_2()
        {
            var testObject = new GameObject();
            var grid = testObject.AddComponent<IslandGridModulePresenter>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid.BuildIslandAt(new Vector2Int(i, j));
                }
            }
            var n = grid.GetEmptyAdjacentGrids(new Vector2Int(0,0)).Count;
            Assert.AreEqual(2, n);
        }
        [Test]
        public void GetEmptyAdjacentGrids_CheckCenterGrid_2()
        {
            var testObject = new GameObject();
            var grid = testObject.AddComponent<IslandGridModulePresenter>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid.BuildIslandAt(new Vector2Int(i, j));
                }
            }
            var n = grid.GetEmptyAdjacentGrids(new Vector2Int(1, 1)).Count;
            Assert.AreEqual(0, n);
        }
        [Test]
        public void IslandGridModule_IsContourGrid_false()
        {
            var testObject = new GameObject();
            var grid = testObject.AddComponent<IslandGridModuleChild>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid.BuildIslandAt(new Vector2Int(i, j));
                }
            }

            var isContourGrid = grid.New_IsContourGrid(new Vector2Int(4, 5));
            Assert.IsFalse(isContourGrid);
        }
        [Test]
        public void IslandGridModule_IsContourGrid_true()
        {
            var testObject = new GameObject();
            var grid = testObject.AddComponent<IslandGridModuleChild>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid.BuildIslandAt(new Vector2Int(i, j));
                }
            }

            var isContourGrid = grid.New_IsContourGrid(new Vector2Int(1, 1));
            Assert.IsTrue(isContourGrid);
        }
    }

    class IslandGridModuleChild : IslandGridModulePresenter
    {
        public bool New_IsContourGrid(Vector2Int gridPos)
        {
            return IsContourGrid(gridPos);
        }

    }
}
