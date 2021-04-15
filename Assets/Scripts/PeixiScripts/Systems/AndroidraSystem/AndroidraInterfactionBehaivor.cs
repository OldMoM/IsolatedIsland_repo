using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public static class AndroidraInterfactionBehaivor 
    {
        private static IObservable<Unit> MoveToTargetPos(Vector2           islandPos,
                                                         IObservable<bool> onReachTarget,
                                                         AndroidraState    state  )
        {
            return null;
        }
        public static IObservable<Unit> StartRestoreIsland(float restoreTime)
        {
            var time = new ConcurrentTimer();
            time.StartTimeCountdown(restoreTime);
            return time.OnTimerEnd;
        }

        public static void RestoreIslnd(Vector2Int islandPos)
        {

        }
    }
}
