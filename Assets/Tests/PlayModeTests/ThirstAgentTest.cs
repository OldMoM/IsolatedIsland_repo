using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using Siwei;
using UniRx;
using System.Collections;
using NUnit.Framework;

public class ThirstAgentTest : MonoBehaviour
{
    AgentDependency CreateThirstAgentDependency()
    {
        var dependency = new AgentDependency();
        dependency.isDay = new BoolReactiveProperty(false);
        dependency.onRainDay = new Subject<Unit>();
        dependency.playerPropertySystem = new PlayerPropertySystem();
        dependency.speed = 5;
        return dependency;
    }

    [UnityTest]
    public IEnumerator ThirstAgentTest_ThirstAfter4sec_79()
    {
        var dependency = CreateThirstAgentDependency();
        var agent = new ThirstAgent(dependency);

        yield return new WaitForSeconds(4);
        Assert.AreEqual(79, dependency.playerPropertySystem.Thirst);
    }

    [UnityTest]
    public IEnumerator ThirstAgentTest_ThirstLess70_Euclid()
    {
        var dependency = CreateThirstAgentDependency();
        var agent = new ThirstAgent(dependency);

        dependency.playerPropertySystem.ChangeSatiety(-10);
        Debug.Log(dependency.playerPropertySystem.ThirstLevel);
        Assert.AreEqual(PropertyLevel.Euclid, dependency.playerPropertySystem.ThirstLevel);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ThirstAgentTest_ThirstEqual0_Speed2()
    {
        var dependency = CreateThirstAgentDependency();
        var agent = new ThirstAgent(dependency);

        dependency.playerPropertySystem.ChangeThirst(-90);
        Debug.Log("ThisrtLevel:" + dependency.playerPropertySystem.ThirstLevel);
        Assert.AreEqual(2, dependency.speed);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ThirstAgentTest_ThirstEqualsZero_Keter()
    {
        var dependency = CreateThirstAgentDependency();
        var agent = new ThirstAgent(dependency);

        dependency.playerPropertySystem.ChangeThirst(-90);
        Assert.AreEqual(PropertyLevel.Keter, dependency.playerPropertySystem.ThirstLevel);

        yield return null;
    }
    [UnityTest]
    public IEnumerator ThirstAgentTest_ThirstEquals80_Safe()
    {
        var dependency = CreateThirstAgentDependency();
        var agent = new ThirstAgent(dependency);

        dependency.playerPropertySystem.ChangeThirst(10);
        Assert.AreEqual(PropertyLevel.Safe, dependency.playerPropertySystem.ThirstLevel);
        yield return null;
    }
}
