using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;

namespace Tests
{
    public class PositionTransformTest
    {
        [Test]
        public void WorldPosToGrid_1and1()
        {
            PositionConvent convent = new PositionConvent();
            GridSetting setting = new GridSetting(3, Vector2.zero);
            var gridPos = convent.WorldToGridPosition(new Vector3(3.1f, 0, 3.3f),setting);
            Assert.AreEqual(new Vector2Int(1, 1), gridPos);
        }
        [Test]
        public void GridPosToWorld_6and9() 
        {
            PositionConvent convent = new PositionConvent();
            GridSetting setting = new GridSetting(3, Vector2.zero);
            var worldPos = convent.GridToWorldPosition(
                new Vector2Int(2, 3),
                setting
                );
            Assert.AreEqual(new Vector3(6, 0, 9), worldPos); 
        }
    }
}
