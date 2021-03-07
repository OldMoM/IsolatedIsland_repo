using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using Siwei;

namespace Tests
{
    public class InterfaceArchiveTest
    {
        IInterfaceArchive config()
        {
            var archive_go = new GameObject();
            return archive_go.AddComponent<InterfaceArichives>();
        }

        IArbitorSystem CreateArbitorSystem()
        {
            var arbitor_go = new GameObject();
            return arbitor_go.AddComponent<ArbitorSystem>();
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
        [UnityTest] 
        public IEnumerator GetInterface_IBuildSketch_notnull()
        {
            var achive = config();
            var ibuildSketch = new GameObject().AddComponent<BuildSketch>();
           
            Assert.IsNotNull(achive.IBuildSketch);
            yield return null;
        }
        [UnityTest]
        public IEnumerator GetInterface_IDialogSystem_notnull()
        {
            var achive = config();
            var iDialogSystem = new GameObject().AddComponent<LvDialogSystem>();

            Assert.IsNotNull(achive.IDialogSystem);
            yield return null;
        }

    }
}
