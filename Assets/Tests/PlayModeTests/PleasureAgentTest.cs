using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using Siwei;
using UniRx;
using System.Collections;
using NUnit.Framework;

public class PleasureAgentTest
{
    AgentDependency CreatePleasureAgentDependency()
    {
        var dependency = new AgentDependency();
        dependency.isDay = new BoolReactiveProperty(false);
        dependency.onRainDay = new Subject<Unit>();
        dependency.playerPropertySystem = new PlayerPropertySystem();
        dependency.speed = 5;
        return dependency;
    }

    AgentDependency CreatePleasureRainAgentDependency()
    {
        var dependency = new AgentDependency();
        dependency.isDay = new BoolReactiveProperty(false);
        dependency.onRainDay = new Subject<Unit>(Unit.Default);
        dependency.playerPropertySystem = new PlayerPropertySystem();
        dependency.speed = 5;
        return dependency;
    }



    [UnityTest]
    public IEnumerator PleasureAgentTest_PleasureInitial_40_Euclid()
    {
        var dependency = CreatePleasureAgentDependency();
        var agent = new PleasureAgent(dependency);

        Assert.AreEqual(40, dependency.playerPropertySystem.Pleasure);
        Assert.AreEqual(PropertyLevel.Euclid, dependency.playerPropertySystem.PleasureLevel);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PleasureAgentTest_PleasureMoreThan50_Safe()
    {
        var dependency = CreatePleasureAgentDependency();
        var agent = new PleasureAgent(dependency);
        dependency.playerPropertySystem.ChangePleasure(11);
        Assert.AreEqual(PropertyLevel.Safe, dependency.playerPropertySystem.PleasureLevel);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PleasureAgentTest_PleasureLessThan30_Keter()
    {
        var dependency = CreatePleasureAgentDependency();
        var agent = new PleasureAgent(dependency);
        dependency.playerPropertySystem.ChangePleasure(-11);
        Assert.AreEqual(PropertyLevel.Keter, dependency.playerPropertySystem.PleasureLevel);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PleasureAgentTest_PleasureEqual0_Speed2()
    {
        var dependency = CreatePleasureAgentDependency();
        var agent = new PleasureAgent(dependency);

        dependency.playerPropertySystem.ChangePleasure(-90);
        Assert.AreEqual(2, dependency.speed);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator PleasureAgentTest_PleasureOnRainDay_PleasureMinus10()
    {
        var dependency = CreatePleasureRainAgentDependency();
        var agent = new PleasureAgent(dependency);
        Assert.AreEqual(30, dependency.playerPropertySystem.Pleasure);
        yield return null;
    }
}
