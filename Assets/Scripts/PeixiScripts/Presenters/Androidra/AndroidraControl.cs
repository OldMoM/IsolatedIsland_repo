using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class AndroidraControl
    {
        /// <summary>
        /// Deliver facility's type at string, position at Vector2Int when build message is received
        /// </summary>
        public IObservable<ValueTuple<string, Vector2Int>> OnBuildMsgReceived => onBuildMsgReceived;

        private Subject<ValueTuple<string,Vector2Int>> onBuildMsgReceived = new Subject<ValueTuple<string, Vector2Int>>();
        public void BuildAt(string type, Vector2Int pos)
        {
            //Debug.Log("Androidra will build " + type + " at " + pos);
            onBuildMsgReceived.OnNext(new ValueTuple<string, Vector2Int>(type, pos));
        }
    }
}
