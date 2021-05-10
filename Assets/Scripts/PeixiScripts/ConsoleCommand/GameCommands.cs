using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi.Commands
{
    public static class GameCommands 
    {
        /// <summary>
        /// Pauses the game.
        /// </summary>
        public static void PauseGame()
        {
            var onGamePaused = Entity.gameTriggers["onGamePaused"] as IObserver<Unit>;
            onGamePaused.OnNext(Unit.Default);
        }
        public static void ResumeGame()
        {
            var onGameResume = Entity.gameTriggers["onGameResumed"] as IObserver<Unit>;
            onGameResume.OnNext(Unit.Default);
        }
    }
}
