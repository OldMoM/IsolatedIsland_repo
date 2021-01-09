using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;

namespace Peixi
{
    [HelpURL("https://spicegamestudio.pingcode.com/wiki/spaces/5fd0cd48fee2547e79b1b100/pages/5ff966023b6d6b6281b64b4e")]
    public class MsgWidgeView : MonoBehaviour
    {
        [Header("消息通知栏设置")]
        public GameObject msgTextPrefab;
        [Tooltip("消息通知栏中最多出现的消息条数")]
        public int maxMsgPrefabs = 5;
        [Tooltip("消息清理时间间隔")]
        public int msgClearTime = 2;

        Transform workNode;
        Transform sleepNode;
   
        Queue<GameObject> msgSleepQuene = new Queue<GameObject>();

        IInventorySystem iinventory;

        // Start is called before the first frame update
        void Start()
        {
            workNode = transform.Find("MsgPrefabWorkNode");
            sleepNode = transform.Find("MsgPrefabSleepNode");
            Assert.IsNotNull(workNode);
            Assert.IsNotNull(sleepNode);

            for (int i = 0; i < maxMsgPrefabs; i++)
            {
                CreatMesComponent();
            }

            iinventory = FindObjectOfType<InventorySystem>();
            Assert.IsNotNull(iinventory, "MsgWidgeView is depend on InventorySystem, make sure that InventorySystem is in Hierarchy");

            iinventory
                .OnInventoryChanged
                .Subscribe(x =>
                {
                    if (msgSleepQuene.Count > 0)
                    {
                        var handleMsgComponent = msgSleepQuene.Dequeue();
                        handleMsgComponent.transform.SetParent(workNode);
                        handleMsgComponent.GetComponent<Text>().text = x.NewValue.Name + " +" + x.NewValue.Amount;
                        ReturnMsgComponent(handleMsgComponent);
                    }
                    else
                    {
                        var _newMsgComponent = CreatMesComponent();
                        _newMsgComponent.transform.SetParent(workNode);
                        _newMsgComponent.GetComponent<Text>().text = x.NewValue.Name + " +" + x.NewValue.Amount;
                        ReturnMsgComponent(_newMsgComponent);
                    }
                });  
        }

        void ReturnMsgComponent(GameObject pendingMsgComponent)
        {
            Observable
            .Timer(System.TimeSpan.FromSeconds(msgClearTime))
            .First()
            .Subscribe(y =>
            {
                pendingMsgComponent.transform.SetParent(sleepNode);
                pendingMsgComponent.GetComponent<Text>().text = string.Empty;
                msgSleepQuene.Enqueue(pendingMsgComponent);
            });
        }

        GameObject CreatMesComponent()
        {
            var _msgPrefab = GameObject.Instantiate(msgTextPrefab, sleepNode);
            _msgPrefab.GetComponent<Text>().text = "";
            msgSleepQuene.Enqueue(_msgPrefab);
            return _msgPrefab;
        }
    }
}
