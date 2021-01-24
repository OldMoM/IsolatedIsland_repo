using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class WallGridModuleTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void WallGridModule_CheckHasWall_false()
        {
            var grid = new WallGridModuleChild();
            var hasWall = grid.HasWall(Vector2Int.zero);
            Assert.IsFalse(hasWall);
        }
        [Test]
        public void WallGridModule_BuiltWall_true()
        {
            var grid = new WallGridModuleChild();
            grid.BuildWall(Vector2Int.zero);
            var hasWall = grid.HasWall(Vector2Int.zero);
            Assert.IsTrue(hasWall);
        }
        [Test]
        public void WallGridModule_RemoveWall_false()
        {
            var testObject = new GameObject();
            var grid = testObject.AddComponent<WallGridModuleChild>();
            var pos = Vector2Int.zero;
            grid.BuildWall(pos);
            Assert.IsTrue(grid.HasWall(pos));
            grid.RemoveWall(pos);
            Assert.IsFalse(grid.HasWall(pos));
        }
      
    }
    class WallGridModuleChild : WallGridModulePresenter 
    {
        public bool HasWall(Vector2Int gridPos)
        {
            return CheckTheGridHasWall(gridPos);
        }

        public void BuildWall(Vector2Int gridPos)
        {
            BuildInvisibleWallAt(gridPos);
        }

        public void RemoveWall(Vector2Int gridPos)
        {
            RemoveWallFrom(gridPos);
        }

    }
}
