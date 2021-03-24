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
        dependency.onGameEnd
            .Subscribe(x =>
            {
                Assert.AreEqual(Unit.Default, x);
            });
        dependency.playerPropertySystem.ChangeHealth(-50);
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_Pleasure60_MaxHealth100()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangePleasure(20);

        Assert.AreEqual(60, dependency.playerPropertySystem.Pleasure);
        Assert.AreEqual(100, dependency.playerPropertySystem.MaxHealth);
        yield return null;
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_Pleasure40_MaxHealth80()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);
        var pleasureAgent = new PleasureAgent(dependency);

        Assert.AreEqual(PropertyLevel.Euclid, dependency.playerPropertySystem.PleasureLevel);
        Assert.AreEqual(40, dependency.playerPropertySystem.Pleasure);
        Assert.AreEqual(80, dependency.playerPropertySystem.MaxHealth);
        yield return null;
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_Pleasure20_MaxHealth60()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);
        //缺少一个PleasureAgent去修改PleasureLevel
        var pleasureAgent = new PleasureAgent(dependency);

        dependency.playerPropertySystem.ChangePleasure(-20);
        Debug.Log(dependency.GetHashCode());

        Assert.AreEqual(20, dependency.playerPropertySystem.Pleasure);
        Assert.AreEqual(PropertyLevel.Keter, dependency.playerPropertySystem.PleasureLevel);
        Assert.AreEqual(60, dependency.playerPropertySystem.MaxHealth);
        yield return null;
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_ThirstEquals80_HealthChangeRateEquals5()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangeThirst(30);
        yield return null;
        Assert.AreEqual(5, healthAgent.thirstBuff);
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_ThirstEquals50_HealthChangeEquals0()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangeThirst(-30);
        yield return null;
        Assert.AreEqual(50, dependency.playerPropertySystem.Thirst);
        Assert.AreEqual(0, healthAgent.thirstBuff);
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_ThirstEquals0_HealthChangeEqualsNegative10()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangeThirst(-80);
        yield return null;
        Assert.AreEqual(0, dependency.playerPropertySystem.Thirst);
        Assert.AreEqual(-10, healthAgent.thirstBuff);
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_SatietyEquals70_SatietyBuffEquals5()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangeSatiety(10);

        Assert.AreEqual(70, dependency.playerPropertySystem.Satiety);
        Assert.AreEqual(5, healthAgent.satietyBuff);

        yield return null;
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_SatietyEquals30_SatietyBuffEquals0()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangeSatiety(-30);

        Assert.AreEqual(30, dependency.playerPropertySystem.Satiety);
        Assert.AreEqual(0, healthAgent.satietyBuff);

        yield return null;
    }
    [UnityTest]
    public IEnumerator HealthAgentTest_SatietyEquals0_SatietyBuffEqualsNegative10()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangeSatiety(-80);

        Assert.AreEqual(0, dependency.playerPropertySystem.Satiety);
        Assert.AreEqual(-10, healthAgent.satietyBuff);

        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_SatietyEquals70AndThirstEquals80_HealthChangeRate10()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        dependency.playerPropertySystem.ChangeSatiety(10);
        dependency.playerPropertySystem.ChangeThirst(20);

        Assert.AreEqual(10, healthAgent.changeRate);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthAgentTest_OnPlayerDied_false()
    {
        var dependency = CreateHealthAgentDependency();
        var healthAgent = new HealthAgent(dependency);

        healthAgent.IsAlive
            .Where(x => !x)
            .Subscribe(x =>
            {
                Debug.Log("Player died");
                Assert.IsFalse(x);
            });

        dependency.playerPropertySystem.ChangeHealth(-100);

        yield return null;
    }
}
