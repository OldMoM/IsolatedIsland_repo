using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using Siwei;
using UniRx;

public class HealthAgentTest
{
    AgentDependency CreateHealthAgentDependency()
    {
        var dependency = new AgentDependency();
        dependency.isDay = new BoolReactiveProperty(false);
        dependency.onRainDay = new Subject<Unit>();
        dependency.playerPropertySystem = new PlayerPropertySystem();
        dependency.speed = 5;
        return dependency;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_Health0_isDied()
    {
        var dependency = CreateHealthAgentDependency();
        var agent = new HealthAgent(dependency);
        dependency.playerPropertySystem.ChangeHealth(-50);
        Assert.AreEqual(true, dependency.playerPropertySystem.OnPlayerDied);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_Pleasure60_MaxHealth100()
    {
        var dependency = CreateHealthAgentDependency();
        var pleasureAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangePleasure(20);
        Assert.AreEqual(100, dependency.playerPropertySystem.MaxHealth);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_Pleasure40_MaxHealth80()
    {
        var dependency = CreateHealthAgentDependency();
        var pleasureAgent = new HealthAgent(dependency);
        Assert.AreEqual(80, dependency.playerPropertySystem.MaxHealth);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_Pleasure20_MaxHealth60()
    {
        var dependency = CreateHealthAgentDependency();
        dependency.playerPropertySystem.ChangePleasure(-20);
        var pleasureAgent = new HealthAgent(dependency);
        Assert.AreEqual(60, dependency.playerPropertySystem.MaxHealth);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_Hunger59Thirst80_Health51()
    {
        var dependency = CreateHealthAgentDependency();
        var pleasureAgent = new HealthAgent(dependency);
        dependency.playerPropertySystem.ChangeSatiety(-1);
        yield return new WaitForSeconds(12);
        Assert.AreEqual(51, dependency.playerPropertySystem.Health);
        yield return null;
    }
}
