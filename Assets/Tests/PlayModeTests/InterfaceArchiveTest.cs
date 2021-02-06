﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;

namespace Tests
{
    public class InterfaceArchiveTest
    {
        IInterfaceArchive config()
        {
            var archive_go = new GameObject();
            return archive_go.AddComponent<InterfaceArichives>();
        }
        [UnityTest]
        public IEnumerator GetTimeSystem_notnull()
        {
            var timesystem_go = new GameObject().AddComponent<TimeSystem>();
            Assert.IsNotNull(config().ITimeSystem);
            yield return null;
        }
        [UnityTest]
        public IEnumerator GetInterface_inventorySystem_notnull()
        {
            var archive = config();
            var inventory = new GameObject().AddComponent<InventorySystem>();
            Assert.IsNotNull(InterfaceArichives.Archive.IInventorySystem);
            yield return null;
        }
        [UnityTest]
        public IEnumerator GetInterface_timeSystem_notnull()
        {
            var archive = config();
            var timeSystem = new GameObject().AddComponent<TimeSystem>();
            Assert.IsNotNull(archive.ITimeSystem);
            yield return null;
        }
        [UnityTest]
        public IEnumerator GetInterface_BuildSystem_notnull()
        {
            var achive = config();
            var buildSystem = new GameObject().AddComponent<BuildSystem>();
            Assert.IsNotNull(achive.IBuildSystem);
            yield return null;
        }
    }
}