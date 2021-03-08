using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("MyNodes",
                  "Log",
                  "sdfsdf")]
    public class FungusLog : Command
    {
        public string text;
        public override void OnEnter()
        {
            Debug.Log(text);
            Continue();
        }
    }
}
