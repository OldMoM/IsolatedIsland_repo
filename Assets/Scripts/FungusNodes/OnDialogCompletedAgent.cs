using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Caoye;
using UniRx;
namespace Peixi
{
    [CommandInfo("DialogSystemAgents",
                 "OnDialogCompleted",
                 "")]
    public class OnDialogCompletedAgent : Command
    {
        public string endDialogId;

        private IDialogSystem dialogSystem;
        public override void OnEnter()
        {
            dialogSystem = InterfaceArichives.Archive.IDialogSystem;
            dialogSystem.OnDialogEnd
                .Where(x => x == endDialogId)
                .Subscribe(x =>
                {
                    Continue();
                });
        }
    }
}
