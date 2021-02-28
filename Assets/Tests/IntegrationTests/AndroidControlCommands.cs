using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;
using Peixi;
using UnityEngine.Assertions;
public class AndroidControlCommands : MonoBehaviour
{
    // Start is called before the first frame update

    private IAndroidraSystem androidra => InterfaceArichives.Archive.IAndroidraSystem;
    void Start()
    {
        DebugLogConsole.AddCommand<string, Vector2Int>("androidra.buildat", "", androidra.Control.BuildAt);
    }

}
