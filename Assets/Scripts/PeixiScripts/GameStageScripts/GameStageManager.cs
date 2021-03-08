using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public static class GameStageManager 
    {
        public static IObservable<string> OnStageStart => onStageStart;
        public static IObservable<string> OnStageCompleted => OnStageCompleted;

        static Subject<string> onStageStart = new Subject<string>();
        static Subject<string> onStageCompleted = new Subject<string>();
        static string theRunningStage;

        public static void StartStage(string stageId)
        {
            onStageStart.OnNext(stageId);
        }

        public static void CompleteStage(string stageId)
        {
            onStageCompleted.OnNext(stageId);
        }
    }
}
