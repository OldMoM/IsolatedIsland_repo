using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
namespace Tests
{
    public class LoadPrefabTest
    {
        // A Test behaves as an ordinary method
        [Test]
        [TestCase("AndroidBuildTimerWidege")]
        [TestCase("AndroidControlPanel")]
        [TestCase("BuildSketch")]
        [TestCase("CollectTipGui")]
        [TestCase("DialogueBox")]
        [TestCase("FacilityInteractWidge")]
        [TestCase("FishGameWidge")]
        [TestCase("InteractionTimerWidge")]
        [TestCase("InventoryGuiWindow")]
        [TestCase("PlayerPropertyHUD")]
        [TestCase("TransferBalckScreen")]
        [TestCase("InGameHudCanvas")]
        public void LoadPrefab_GetUIPrefab_Notnull(string uiComponents)
        {
            Debug.Log(uiComponents);
            var components = LoadPrefab.GetUIPrefab(uiComponents);
            Assert.IsNotNull(components);
        }
        [Test]
        [TestCase("GameSystems")]
        public void LoadPrefab_GetSystemPrefab_NotNull(string systemPrefab)
        {
            Debug.Log(systemPrefab);
            var prefab = LoadPrefab.GetSystemPrefab(systemPrefab);
            Assert.IsNotNull(prefab);
        }

    }
}
