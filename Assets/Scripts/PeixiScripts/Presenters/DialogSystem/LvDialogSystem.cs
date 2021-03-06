using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Caoye;
using UniRx;
using System;

namespace Peixi
{
    public class LvDialogSystem : MonoBehaviour,IDialogSystem
    {
        public Flowchart flowchart;
        public List<SelfDialogPair> selfDialogList;

        private Subject<string> onDialogStart = new Subject<string>();
        private Subject<string> onDialogEnd = new Subject<string>();
        private bool _isActive = false;
        private string thePlayDialog;

        private StringReactiveProperty dialogueStatus = new StringReactiveProperty("None");

        public IObservable<string> OnDialogStart => onDialogStart;

        public IObservable<string> OnDialogEnd => onDialogEnd;

        public bool isActive
        {
            get => _isActive;
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
                thePlayDialog = dialogId;
                onDialogStart.OnNext(dialogId);
                _isActive = true;
                flowchart.SendFungusMessage(dialogId);
            }
            else
            {
                Debug.Log("当前有对话正在进行！");
            }

        }

        public void EndDialog()
        {
            _isActive = false;
            onDialogEnd.OnNext(thePlayDialog);
            thePlayDialog = string.Empty;
        }

        public void StartSelfDialog(int characterId, string context)
        {
            //check characterId valid
            bool found = false;
            //send context to flowchart
            foreach (SelfDialogPair pair in selfDialogList)
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
