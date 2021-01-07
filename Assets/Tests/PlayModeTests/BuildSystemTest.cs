using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
using System;
using Extensions;
using System.Threading;

namespace Tests
{
    public class BuildSystemTest
    {
        private IObservable<GameObject> Init()
        {
            GameObject buildSystemObject_sample = Resources.Load<GameObject>("Systems/BuildSystem");
            var buildSystem = GameObject.Instantiate(buildSystemObject_sample);
            Assert.IsNotNull(buildSystemObject_sample, "Failed to load BuildSystem.prefab");
            var iBuildSystem = buildSystem.GetComponent<IBuildSystem>();
            iBuildSystem.BuildIslandAt(Vector2Int.zero, 5);

            var t = new List<GameObject>().AddItem(buildSystem).ToObservable();
            return t;
        }
        [UnityTest]
        public IEnumerator CreatIsland_true()
        {
            var config = Init();
            config.Subscribe(x =>
            {
                var buildSystem = x.GetComponent<IBuildSystem>();
                var hasIsland = buildSystem.CheckThePositionHasIsland(Vector2Int.zero);
                Assert.IsTrue(hasIsland);
            });

            config.Subscribe(GameObject.Destroy);
            yield return null;
        }
        [UnityTest]
        public IEnumerator RemoveIsland_false()
        {
            var config = Init();
            config.Subscribe(x =>
            {
                var buildSystem = x.GetComponent<IBuildSystem>();
                buildSystem.RemoveIslandAt(Vector2Int.zero);
                var hasIsland = buildSystem.CheckThePositionHasIsland(Vector2Int.zero);
                Assert.IsFalse(hasIsland);
            });

            config.Subscribe(GameObject.Destroy);
            yield return null;
        }
        [UnityTest]
        public IEnumerator RemoveIsland_OnIslandSunk_false()
        {
            //var config = Init();

            //yield return new WaitForSeconds(6);
            //config.Subscribe(x =>
            //{
            //    var buildSystem = x.GetComponent<IBuildSystem>();
            //    var hasIsland = buildSystem.CheckThePositionHasIsland(Vector2Int.zero);
            //    Assert.IsFalse(hasIsland);
            //});

            //config.Subscribe(GameObject.Destroy);
            yield return null;
        }

        [UnityTest]
        public IEnumerator GetIslandInterface_NotNull()
        {
            GameObject buildSystemObject_sample = Resources.Load<GameObject>("Systems/BuildSystem");
            var buildSystem = GameObject.Instantiate(buildSystemObject_sample);
            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(buildSystemObject_sample, "Failed to load BuildSystem.prefab");
            var iBuildSystem = buildSystem.GetComponent<IBuildSystem>();
            iBuildSystem.BuildIslandAt(Vector2Int.zero, 5);

            var iisland = iBuildSystem.GetIslandInterface(Vector2Int.zero);
            Assert.IsNotNull(iisland);
            yield return null;
        }
        [UnityTest]
        public IEnumerator GetIslandDurability_4()
        {
            //arrange
            GameObject buildSystemObject_sample = Resources.Load<GameObject>("Systems/BuildSystem");
            var buildSystem = GameObject.Instantiate(buildSystemObject_sample);
            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(buildSystemObject_sample, "Failed to load BuildSystem.prefab");
            var iBuildSystem = buildSystem.GetComponent<IBuildSystem>();
            iBuildSystem.BuildIslandAt(Vector2Int.zero, 5);

            yield return new WaitForSeconds(1);
            var iisland = iBuildSystem.GetIslandInterface(Vector2Int.zero);
            var durability = iisland.Durability_current;
            Assert.AreEqual(durability, 4);

            GameObject.Destroy(buildSystem);
            yield return null;
        }
        [UnityTest]
        public IEnumerator SetIslandDurability_55()
        {
            //arrange
            GameObject buildSystemObject_sample = Resources.Load<GameObject>("Systems/BuildSystem");
            var buildSystem = GameObject.Instantiate(buildSystemObject_sample);
            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(buildSystemObject_sample, "Failed to load BuildSystem.prefab");
            var iBuildSystem = buildSystem.GetComponent<IBuildSystem>();
            iBuildSystem.BuildIslandAt(Vector2Int.zero, 5);

            yield return new WaitForSeconds(1);
            var iisland = iBuildSystem.GetIslandInterface(Vector2Int.zero);
            iisland.SetDurabilityTo(55);
            var durability = iisland.Durability_current;
            Assert.AreEqual(55, durability);

            GameObject.Destroy(buildSystem);
            yield return null;
        }
        
    }
}
