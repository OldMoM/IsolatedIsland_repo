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
        public void createIsland_notNull()
        {
            var islandEntity = ToolKit.prefabFactory.creatGameobject(PrefabTags.island);
            Assert.IsNotNull(islandEntity);
        }
        [Test]
        public void createWaterPuifier_notNull()
        {
            var waterPuifier = ToolKit.prefabFactory.creatGameobject(PrefabTags.waterPuifier);
            Assert.IsNotNull(waterPuifier);
        }
        [Test]
        public void createFoodPlant_notNull()
        {
            var foodPlant = ToolKit.prefabFactory.creatGameobject(PrefabTags.foodPlant);
            Assert.IsNotNull(foodPlant);
        }
        [Test]
        public void creatFishPoint_notNull()
        {
            var fishPoint = ToolKit.prefabFactory.creatGameobject(PrefabTags.fishPoint);
            Assert.IsNotNull(fishPoint);
        }
    }
}
