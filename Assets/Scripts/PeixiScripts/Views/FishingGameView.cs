﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine;
using UniRx;

public class FishingGameView : MonoBehaviour
{

    public GameObject back;

    public GameObject pointArea;

    public GameObject pointers;
    [SerializeField]
    private float pointerMoveSpeed = 10;

    [Header("-----TEST VARIABLE BLOCK-----")]
    [SerializeField]
    [Header("钓鱼小游戏运行次数")]
    int runTimesInReality;
    [SerializeField]
    [Header("收到信号的次数")]
    int receiveSignalTime;

    [SerializeField]
    bool isActive;

    System.IDisposable moveLoopMicrotine1;
    System.IDisposable moveLoopMicrotine2;
    System.IDisposable randomMovePointer;

    Dictionary<string,float> config;
    // Start is called before the first frame update
    void ShowChildren(bool active)
    {
        float _alpha = active ? 1f : 0;
        back.GetComponent<RawImage>().color = new Color(1, 1, 1, _alpha);
        pointArea.GetComponent<RawImage>().color = new Color(1, 1, 1, _alpha);
        pointers.GetComponentInChildren<RawImage>().color = new Color(1, 1, 1, _alpha);
    }
    void Start()
    {
        //-----get game config-----
        if (Debug.isDebugBuild)
        {
            config = GameConfig.Singleton.InteractionConfig;
            if (config == null)
            {
                Debug.Log("config is null,this made the FishignGameView.cs crashed");
            }
            else
            {
                Debug.Log("config is correct");
            } 
        }
        ShowChildren(false);

        GUIEvents.Singleton.PlayerStartFishing
            .Throttle(System.TimeSpan.FromSeconds(1))
            .Where(x=>!isActive)
            .Subscribe(x =>
            {
                GUIEvents.Singleton.BroadcastInteractTipMessage.OnNext("fishing game view 收到消息");
                isActive = true;
                receiveSignalTime++;
                RunFishGame(true);
            });
    }
  
    void EndFishGame()
    {
        runTimesInReality++;
        if (moveLoopMicrotine1 != null)
        {
            moveLoopMicrotine1.Dispose();
        }
        if (moveLoopMicrotine2 != null)
        {
            moveLoopMicrotine2.Dispose();
        }
        if (randomMovePointer != null)
        {
            randomMovePointer.Dispose();
        }
   
        Observable.Timer(System.TimeSpan.FromSeconds(1))
            .First()
            .Subscribe(x =>
            {
                isActive = false;
                ShowChildren(false);
            });
    }
    void PointerLoopMoving(RectTransform trans, Vector3 startPos,Vector3 endPos)
    {
        trans.localPosition = startPos;
        Vector3 target = endPos;
        float _pointerMoveSpeed = config["fishGame_pointerMoveSpeed"];

        moveLoopMicrotine1 = Observable.EveryFixedUpdate()
            .Where(x => target == endPos)
            .Subscribe(x =>
            {
                var dis_vec = target - trans.localPosition;
                trans.localPosition += dis_vec.normalized * _pointerMoveSpeed;
                float dis = dis_vec.sqrMagnitude;
                if (dis <= 0.05f)
                {
                    trans.localPosition = target;
                    target = startPos;
                }
            });

        moveLoopMicrotine2 = Observable.EveryFixedUpdate()
            .Where(x => target == startPos)
            .Subscribe(x =>
            {
                var dis_vec = target - trans.localPosition;
                trans.localPosition += dis_vec.normalized * pointerMoveSpeed;
                float dis = dis_vec.sqrMagnitude;
                if (dis <= 0.05f)
                {
                    trans.localPosition = target;
                    target = endPos;
                }
            });

        float _diceIntervalTime = config["fishGame_diceIntervalTime"];
        float _diceTurnReusltPro = config["fishGame_diceTurnReusltPro"];

        randomMovePointer = Observable.Interval(System.TimeSpan.FromSeconds(_diceIntervalTime))
            .Subscribe(x =>
            {
                //-----dice-----
                int _dice = Random.Range(1, 100);
                if (_dice <= _diceTurnReusltPro)
                {
                    if (target == endPos)
                    {
                        target = startPos;
                    }
                    else
                    {
                        target = endPos;
                    }
                }
            });
    }
    void JudgePlayerScore(RectTransform pointer,Vector3 pointAreaStartPos,Vector3 pointAreaEndPos)
    {
        InputSystem.Singleton.OnInteractBtnPressed
            .First()
            .Subscribe(x =>
            {
                var pointer_x = pointer.localPosition.x;
                if (pointer_x>pointAreaStartPos.x && pointer_x< pointAreaEndPos.x)
                {
                    GUIEvents.Singleton.PlayerEndFishing.OnNext(true);
                    AudioManager.Singleton.PlayAudio("Interact_positiveCollectResourceComplete");
                }
                else
                {
                    GUIEvents.Singleton.PlayerEndFishing.OnNext(false);
                }
                EndFishGame();
            });
    }
    void RunFishGame(bool active)
    {
        
        ShowChildren(active);
        RectTransform _backTrans = back.GetComponent<RectTransform>();
        float _backLength = _backTrans.sizeDelta.x;
        RectTransform _pointAreaTrans = pointArea.GetComponent<RectTransform>();
        //-----set point area-----
        float _pointAreaLengthMax = config["fishGame_pointAreaLengthMax"];
        float _pointAreaLengthMin = config["fishGame_pointAreaLengthMin"];
        float _pointAreaLength = Random.Range(_pointAreaLengthMin, _pointAreaLengthMax);
        var _pointAreaSize = _pointAreaTrans.sizeDelta;
        _pointAreaSize.x = _pointAreaLength;
        _pointAreaTrans.sizeDelta = _pointAreaSize;

        float _pointAreaPos_x = Random.Range(-_backLength / 2 + _pointAreaLength / 2, _backLength / 2 - _pointAreaLength / 2);
        var _pointAreaPos = _pointAreaTrans.localPosition;
        _pointAreaPos.x = _pointAreaPos_x;
        _pointAreaTrans.localPosition = _pointAreaPos;

        //-----move the points-----
        RectTransform _pointersTrans = pointers.GetComponent<RectTransform>();
        Vector3 _startPos = _backTrans.localPosition - new Vector3(_backLength / 2, _backTrans.localPosition.y, _backTrans.localPosition.z);
        Vector3 _endPos = _backTrans.localPosition + new Vector3(_backLength / 2, _backTrans.localPosition.y, _backTrans.localPosition.z);

        PointerLoopMoving(_pointersTrans, _startPos,_endPos);

        var _pointAreaStartPos = _pointAreaTrans.localPosition - new Vector3(_pointAreaLength / 2, _pointersTrans.localPosition.y, _pointersTrans.localPosition.z);
        var _pointAreaEndPos = _pointAreaTrans.localPosition + new Vector3(_pointAreaLength / 2, _pointersTrans.localPosition.y, _pointersTrans.localPosition.z);
        JudgePlayerScore(_pointersTrans, _pointAreaStartPos, _pointAreaEndPos);

    }
}
