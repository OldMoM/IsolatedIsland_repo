using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
namespace Tests
{
    public class PlayerSystemTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayerState_onStateChanged_Interact()
        {
            var playerSystem = GameObject.FindObjectOfType<PlayerSystem>();
            Assert.IsNotNull(playerSystem, "未部署PlayerHandle.prefab至Hierarchy");

            var playerState = playerSystem.StateController.playerState;

           
        }
    }
}
