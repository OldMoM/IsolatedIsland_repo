using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;

namespace Tests
{
    public class PositionConventTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void WorldToGrid_1and1()
        {
            PositionConvent convent = new PositionConvent();
            var gridPos = convent.WorldToGridPosition(new Vector3(3.1f, 0, 3.2f));
            Assert.AreEqual(gridPos, new Vector2Int(1, 1));
        }
    }
}
