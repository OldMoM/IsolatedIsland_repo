using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caoye;
using Peixi;

namespace Fungus
{
    [CommandInfo("DialogSystem",
                 "StartDialog",
                 "开启对话")]
    public class StartDialog : Command
    {
        public string dialogId;
        public override void OnEnter()
        {
            var dialogSystem = InterfaceArichives.Archive.IDialogSystem;
            dialogSystem.StartDialog(dialogId);
        }
    }
}
