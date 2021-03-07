using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Siwei
{
    public interface IChatBubble 
    {
        void StartChat(string[] msg,IObservable<Vector3> onPlayerPositionChanged);
        bool Active { get; }
    }
}
