using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class PleasureAgentTest
    {
        AgentDependency CreateHungerAgentDependency()
        {
            var dependency = new AgentDependency();
            dependency.isDay = new BoolReactiveProperty(false);
            dependency.onRainDay = new Subject<Unit>();
            dependency.playerPropertySystem = new PlayerPropertySystem();
            dependency.speed = 5;
            return dependency;
        }
        [UnityTest]
        public IEnumerator PleasureAgentTest_OnRainDay_Lost10Pleasure()
        {
            var dependency = CreateHungerAgentDependency();
            var onRainDay = new Subject<Unit>();
            dependency.onRainDay = onRainDay;
            var agent = new PleasureAgent(dependency);

            onRainDay.OnNext(Unit.Default);
            Assert.AreEqual(30, dependency.playerPropertySystem.Pleasure);

            yield return null;
        }
    }
}
