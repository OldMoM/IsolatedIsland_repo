using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
using System;
using Extensions;

namespace Tests
{
    public class IslandTest
    {

        private IObservable<GameObject> Init()
        {
            GameObject islandObject = new GameObject();
            islandObject.transform.name = "TestIsland";
            islandObject.AddComponent<IslandPresenter>()
                .Active(Vector2Int.zero, 5);

            return new List<GameObject>()
                .AddItem(islandObject)
                .ToObservable();
        }
        IslandPresenter islandPresenter;
        [UnityTest]
        public IEnumerator CreatIsland_NotNull()
        {
            //Arrage
            var cleanConfig = Init();
            //Act
            cleanConfig.Subscribe(x =>
            {
                var islandInScene = GameObject.Find("TestIsland");
                Assert.IsNotNull(islandInScene);
            });
            //Clean config
            cleanConfig.Subscribe(GameObject.Destroy);
            yield return null;
        }
        [UnityTest]
        public IEnumerator CheckDurabilityIsDecreasing_3()
        {
            GameObject islandObject = new GameObject();
            islandObject.transform.name = "TestIsland";
            var _presenter = islandObject.AddComponent<IslandPresenter>();
            Assert.IsNotNull(_presenter);
            _presenter.Active(Vector2Int.zero, 5);
             
            yield return new WaitForSeconds(3);
            Assert.AreEqual(3, _presenter.Durability_current);
        }
        [UnityTest]
        public IEnumerator DestoryIsland_OnDurabilityOut_null()
        {

            //Arrage
            var config = Init();
            yield return new WaitForSeconds(6);
            var island = GameObject.Find("TestIsland");
            Assert.IsNull(island);
            yield return null;
        }
        [UnityTest]
        public IEnumerator IslandGridModule_OnIslandSunk_false()
        {
            yield return null;
        }
        [UnityTest]
        public IEnumerator IslandPresenter_SetDurabilityTo_55()
        {
            var island = new GameObject();
            var presenter = island.AddComponent<IslandPresenter>();
            presenter.SetDurabilityTo(55);
            Assert.AreEqual(55, presenter.Durability_current);
            yield return null;
        }
    }
}
