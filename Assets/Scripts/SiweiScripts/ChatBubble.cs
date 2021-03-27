using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Threading;
using Peixi;

namespace Siwei
{
    public class ChatBubble : MonoBehaviour, IChatBubble
    {
        private bool active = false;
        private Text text;
        private IDisposable interval;
        private IDisposable posDisposable;
        public int duration;
        [SerializeField]private Vector3 offset = new Vector3(0, 10, 0);
        //public Transform dialogueBox;

        public void StartChat(string[] msg, IObservable<Vector3> onPlayerPositionChanged) {

            active = true;

            text = GetComponentInChildren<Text>(true);
            posDisposable = onPlayerPositionChanged.TakeWhile(_ => active)
                .Subscribe(pos =>
                {
                    transform.position = SetDialogueBoxPos(pos);
                    if (!gameObject.activeSelf)
                    {
                        gameObject.SetActive(true);
                    }
                }).AddTo(this);
            

            interval = Observable
                .Timer(TimeSpan.Zero,TimeSpan.FromSeconds(duration))
                .Take(msg.Length+1)
                .Subscribe(x =>
                    {
                        if(x < msg.Length)
                        {
                            text.text = msg[x];
                        }                        
                    },()=> { 
                        active = false; 
                        gameObject.SetActive(false);
                        interval.Dispose();
                        posDisposable.Dispose();
                    }).AddTo(this);

            AudioEvents.StartAudio("OnChatBubbleEnabled");
        }
        public bool Active { get { return active; } }

        private Vector3 SetDialogueBoxPos(Vector3 pos) {
            return Camera.main.WorldToScreenPoint(pos) + offset;
        }

        
    }
}

