using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
namespace Tests
{
    public class EntityModelTest
    {
        private IslandModel CreateFakeIslandModel(Vector2Int pos)
        {
            var model = EntityModelFactory.CreateIslandModel();
            model.onDayStart = new Subject<int>();
            model.onDayEnd = new Subject<int>();
            model.attachedObject = new GameObject();
            model.positionInGrid = pos;

            return model;
        }
        [Test]
        public void AddIslandModel_OnIslandEntityInited_notNull()
        {
            var model = CreateFakeIslandModel(new Vector2Int(1,1));
            IslandPresenterNew.Init(ref model);

            var key = new Vector2Int(1, 1);
            Assert.AreEqual(key, Entity.islandModels[key].positionInGrid);
        }
        [Test]
        public void RemoveIslandModel_OnIslandDestoryed_notFound()
        {
            var model1 = CreateFakeIslandModel(new Vector2Int(1, 2));
            var model2 = CreateFakeIslandModel(new Vector2Int(2, 2));
            IslandPresenterNew.Init(ref model1);
            IslandPresenterNew.Init(ref model2);

            var key = new Vector2Int(2, 2);
            //毁坏岛块，从EntityModel中剔除相关数据
            model2.durability.Value = 0;
            //查询EntityModel中的数据
            var hasKey = Entity.islandModels.ContainsKey(key);

            Assert.IsFalse(hasKey);
        }
    }
}
