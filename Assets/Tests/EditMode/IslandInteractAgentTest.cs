using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class IslandInteractAgentTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void OnPlayerTouchBrokenIsland()
        {
            var playerPosition = new Vector3ReactiveProperty();
            var agent = new IslandInteractAgent(playerPosition);

            agent.OnContactBrokenIslandChanged
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.IsTrue(x);
                });

            agent.OnContactingBrokenIslandPosChanged
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(Vector2Int.zero, x);
                });

            playerPosition.Value = Vector3.right;
        }
    }
}
