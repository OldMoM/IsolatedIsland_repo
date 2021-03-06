using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UniRx;

namespace Caoye
{
    public class DialogueSystem : MonoBehaviour, IDialogSystem
    {
        public Flowchart flowchart;
        public List<SelfDialogPair> selfDialogList;

        private StringReactiveProperty dialogueStatus = new StringReactiveProperty("None");


        public IObservable<string> OnDialogStart => dialogueStatus;

        public IObservable<string> OnDialogEnd => dialogueStatus;

        public bool isActive
        {
            get => dialogueStatus.Value == "on";
        }

        public string ChangeDialogueStatus
        {
            get => dialogueStatus.Value;
            set
            {
                dialogueStatus.Value = value;
            }
        }

        public void dialogueStart()
        {
            dialogueStatus.Value = "on";
        }

        public void dialogueEnd()
        {
            dialogueStatus.Value = "off";
        }

        public void StartDialog(string dialogId)
        {
            //check dialogId Valid

            //send context to flowchart
            if (!isActive)
            {
                flowchart.SendFungusMessage(dialogId);
            } else
            {
                Debug.Log("当前有对话正在进行！");
            }
            
        }

        public void StartSelfDialog(int characterId, string context)
        {
            //check characterId valid
            bool found = false;
            //send context to flowchart
            foreach(SelfDialogPair pair in selfDialogList)
            {
                if (pair.characterId == characterId)
                {
                    found = true;
                    pair.characterFlowchart.SendFungusMessage(context);
                }
            }

            if (!found)
            {
                print("characterId Not found");
            }
        }
    }

    [Serializable]
    public struct SelfDialogPair
    {
        public int characterId;
        public string characterName;
        public Flowchart characterFlowchart;
    }
}
