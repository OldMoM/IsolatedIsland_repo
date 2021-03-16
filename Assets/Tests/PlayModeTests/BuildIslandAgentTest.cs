using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
using System;

namespace Tests
{
    public class BuildIslandAgentTest
    {
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator BuildIslandAgent_NoEnoughMaterial_false()
        {
            var gameSystems = LoadPrefab.GetSystemPrefab("GameSystems");
            GameObject.Instantiate(gameSystems);
            var agent = new BuildIslandAgent(new Vector3ReactiveProperty(), InterfaceArichives.Archive);

            Assert.IsFalse(agent.Enough);
            yield return null;
        }
        [UnityTest]
        public IEnumerator BuildIslandAgent_HasEnoughMaterial_true()
        {
            var gameSystems = LoadPrefab.GetSystemPrefab("GameSystems");
            GameObject.Instantiate(gameSystems);

            var inventory = InterfaceArichives.Archive.IInventorySystem;
            inventory.AddItem("Plastic", 50)
                     .AddItem("String", 50);

            var agent = new BuildIslandAgent(new Vector3ReactiveProperty(), InterfaceArichives.Archive);
            Assert.IsTrue(agent.Enough);

            yield return null;
        }
        [UnityTest]
        public IEnumerator BuildIslandAgent_SetIsActiveOnInteractStart_True()
        {
            var gameSystems = LoadPrefab.GetSystemPrefab("GameSystems");
            GameObject.Instantiate(gameSystems);

            var inventory = InterfaceArichives.Archive.IInventorySystem;
            inventory.AddItem("Plastic", 50)
                     .AddItem("String", 50);

            var mousePos = new Vector3ReactiveProperty();

            var agent = new BuildIslandAgent(mousePos, InterfaceArichives.Archive);
            mousePos.Value = new Vector3(5, 5, 5);

            Assert.IsTrue(agent.IsActive);
            yield return null;
        }
        [UnityTest]
        public IEnumerator BuildIslandAgent_SetIsActiveOnInteractCompleted_False()
        {
            var gameSystems = LoadPrefab.GetSystemPrefab("GameSystems");
            GameObject.Instantiate(gameSystems);

            var inventory = InterfaceArichives.Archive.IInventorySystem;
            inventory.AddItem("Plastic", 50)
                     .AddItem("String", 50);

            var mousePos = new Vector3ReactiveProperty();

            var agent = new BuildIslandAgent(mousePos, InterfaceArichives.Archive);
            yield return new WaitForSeconds(10);
            Assert.IsFalse(agent.IsActive);
            yield return null;
        }
        [UnityTest]
        public IEnumerator BuildIslandAgent_OnInteractStartMessage_Unit()
        {
            var gameSystems = LoadPrefab.GetSystemPrefab("GameSystems");
            GameObject.Instantiate(gameSystems);

            var inventory = InterfaceArichives.Archive.IInventorySystem;
            inventory.AddItem("Plastic", 50)
                     .AddItem("String", 50);
            var mousePos = new Vector3ReactiveProperty();

            BuildIslandAgent agent = new BuildIslandAgent(mousePos, InterfaceArichives.Archive);

            agent.OnInteractStart
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(Unit.Default, x);
                });
            mousePos.Value = new Vector3(3, 3, 3);
            yield return null;
        }
        [UnityTest]
        public IEnumerator BuildIslandAgent_OnInteractCompletedMessage_Unit()
        {
            var gameSystems = LoadPrefab.GetSystemPrefab("GameSystems");
            GameObject.Instantiate(gameSystems);

            var inventory = InterfaceArichives.Archive.IInventorySystem;
            inventory.AddItem("Plastic", 50)
                     .AddItem("String", 50);
            var mousePos = new Vector3ReactiveProperty();

            BuildIslandAgent agent = new BuildIslandAgent(mousePos, InterfaceArichives.Archive);

            agent.OnInteractCompleted
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(Unit.Default, x);
                });
            mousePos.Value = new Vector3(3, 3, 3);
            yield return new WaitForSeconds(10);
        }

    }
}
