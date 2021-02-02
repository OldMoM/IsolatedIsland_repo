using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
namespace Tests
{
    public class PrefabFactoryTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void creatIsland_notNull()
        {
            var islandEntity = PrefabFactory.singleton.creatGameobject("prop_island");
            Assert.IsNotNull(islandEntity);
        }
    }
}
