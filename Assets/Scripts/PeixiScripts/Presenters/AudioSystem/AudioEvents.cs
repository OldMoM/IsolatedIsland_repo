using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    /// <summary>
    ///   <para>音效事件</para>
    ///   <para>用法见AudioEventTest</para>
    /// </summary>
    public static class AudioEvents 
    {
        private static Subject<string> audioEvent = new Subject<string>();
        public static IObservable<string> OnAudioStart => audioEvent;
        public static void StartAudio(string audioEventName)
        {
            audioEvent.OnNext(audioEventName);
        }
    }
}
