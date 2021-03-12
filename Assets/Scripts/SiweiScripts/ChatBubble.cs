using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Threading;

namespace Siwei
{
    public class ChatBubble : MonoBehaviour, IChatBubble
    {
        private bool active = false;
        private Text text;
        public int duration;
        private Vector3 offset = new Vector3(0, 10, 0);
        //public Transform dialogueBox;

        public void StartChat(string[] msg, IObservable<Vector3> onPlayerPositionChanged) {
            /*
             if (dialogueBox == null)
                {
                    dialogueBox = transform.Find("dialogueBox");
                }
            */
            active = true;

            text = GetComponentInChildren<Text>(true);

            onPlayerPositionChanged.TakeWhile(_=>active)
                .Subscribe(pos =>
                {
                    transform.position = SetDialogueBoxPos(pos);
                    if (!gameObject.activeSelf)
                    {
                        gameObject.SetActive(true);
                    }
                });

            var interval = Observable
                .Timer(TimeSpan.Zero,TimeSpan.FromSeconds(duration))
                .Take(msg.Length+1)
                .Subscribe(x =>
                    {
                        if(x < msg.Length)
                        {
                            text.text = msg[x];
                        }                        
                    },()=> { active = false; gameObject.SetActive(false); }).AddTo(this);
            
            
        }
        public bool Active { get { return active; } }

        private Vector3 SetDialogueBoxPos(Vector3 pos) {
            //return Camera.main.WorldToScreenPoint(pos) + offset;
            return pos+offset;
        }

        
    }
}

