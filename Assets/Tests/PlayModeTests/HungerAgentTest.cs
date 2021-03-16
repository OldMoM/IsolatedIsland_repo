using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using Siwei;
using UniRx;

namespace Tests
{
    public class HungerAgentTest
    {
        HungerAgentDependency CreateHungerAgentDependency()
        {
            var dependency = new HungerAgentDependency();
            dependency.isDay = new BoolReactiveProperty(false);
            dependency.onRainDay = new Subject<Unit>();
            dependency.playerPropertySystem = new PlayerPropertySystem();
            dependency.speed = 5;
            return dependency;
        }
        [UnityTest]
        public IEnumerator HungerAgentTest_HungerAfter6sec_59()
        {
            var dependency = CreateHungerAgentDependency();
            var agent = new HungerAgent(dependency);

            yield return new WaitForSeconds(7);
            Assert.AreEqual(59, dependency.playerPropertySystem.Satiety);
        }
        [UnityTest]
        public IEnumerator HungerAgentTest_HungerLess60_Euclid()
        {
            var dependency = CreateHungerAgentDependency();
            var agent = new HungerAgent(dependency);
            dependency.playerPropertySystem.ChangeSatiety(-30);
            Debug.Log(dependency.playerPropertySystem.SatietyLevel);
            Assert.AreEqual(PropertyLevel.Euclid, dependency.playerPropertySystem.SatietyLevel);

            yield return null;
        }
    }
}
