using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using IngameDebugConsole;

public class TimeSystemTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DebugLogConsole.AddCommand("timeSystem.StartNight", "", () =>
        {
            var timeSystem = InterfaceArichives.Archive.ITimeSystem;
            timeSystem.StartNight();
        });
    }

}
