using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Siwei;
namespace Peixi
{
    [CommandInfo("DialogSystem",
                 "ChatBubble",
                 "" )]
    public class DialogBox : Command
    {
        public string[] chat;
        public Transform chat_trans;
        public override void OnEnter()
        {
            IChatBubble chatBubble = chat_trans.GetComponent<ChatBubble>();
            IPlayerSystem playerSystem = InterfaceArichives.Archive.PlayerSystem;
            chatBubble.StartChat(chat, playerSystem.OnPlayerPositionChanged);
            Continue();
        }
    }
}
