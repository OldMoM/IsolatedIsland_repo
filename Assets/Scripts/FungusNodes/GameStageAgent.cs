using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UniRx;

namespace Peixi
{
    [CommandInfo("GameStageAgent",
                 "OnStageStart",
                 ""
        )]
    public class GameStageAgent : Command
    {
        public string filterString;
        public override void OnEnter()
        {
            GameStageManager.OnStageStart
                .Where(x => x == filterString)
                .First()
                .Subscribe(x =>
                {
                    Continue();
                }).AddTo(this);
        }
    }
}
