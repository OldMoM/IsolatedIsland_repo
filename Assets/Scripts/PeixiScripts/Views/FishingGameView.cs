using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class FishingGameView : MonoBehaviour
    {
        public float pointerMoveSpeed = 10;

        private Transform back_tran;
        private Transform pointArea_tran;
        private Transform pointers_tran;

        private System.IDisposable moveLoopMicrotine1;
        private System.IDisposable moveLoopMicrotine2;
        private System.IDisposable randomMovePointer;

        private Dictionary<string, float> _config;

        private FishPointInteractHandleUnit unit;

        private void ShowChildren(bool active)
        {
            float _alpha = active ? 1f : 0;
            back_tran.gameObject.SetActive(active);
            pointArea_tran.gameObject.SetActive(active);
            pointers_tran.gameObject.SetActive(active);
        }
        private void EndFishGame()
        {
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

            ShowChildren(false);
            unit.endInteract(new FishingResult());
        }
        private void PointerLoopMoving(RectTransform trans, Vector3 startPos, Vector3 endPos)
        {
            trans.localPosition = startPos;
            Vector3 target = endPos;
            float _pointerMoveSpeed = _config["fishGame_pointerMoveSpeed"];

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

            float _diceIntervalTime = _config["fishGame_diceIntervalTime"];
            float _diceTurnReusltPro = _config["fishGame_diceTurnReusltPro"];

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
        private void JudgePlayerScore(RectTransform pointer, Vector3 pointAreaStartPos, Vector3 pointAreaEndPos)
        {
            InputSystem.Singleton.OnInteractBtnPressed
                .First()
                .Subscribe(x =>
                {
                    var pointer_x = pointer.localPosition.x;
                    if (pointer_x > pointAreaStartPos.x && pointer_x < pointAreaEndPos.x)
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
        private void RunFishGame(bool active)
        {

            ShowChildren(active);
            RectTransform _backTrans = back_tran.GetComponent<RectTransform>();
            float _backLength = _backTrans.sizeDelta.x;
            RectTransform _pointAreaTrans = pointArea_tran.GetComponent<RectTransform>();
            //-----set point area-----
            float _pointAreaLengthMax = _config["fishGame_pointAreaLengthMax"];
            float _pointAreaLengthMin = _config["fishGame_pointAreaLengthMin"];
            float _pointAreaLength = Random.Range(_pointAreaLengthMin, _pointAreaLengthMax);
            var _pointAreaSize = _pointAreaTrans.sizeDelta;
            _pointAreaSize.x = _pointAreaLength;
            _pointAreaTrans.sizeDelta = _pointAreaSize;

            float _pointAreaPos_x = Random.Range(-_backLength / 2 + _pointAreaLength / 2, _backLength / 2 - _pointAreaLength / 2);
            var _pointAreaPos = _pointAreaTrans.localPosition;
            _pointAreaPos.x = _pointAreaPos_x;
            _pointAreaTrans.localPosition = _pointAreaPos;

            //-----move the points-----
            RectTransform _pointersTrans = pointers_tran.GetComponent<RectTransform>();
            Vector3 _startPos = _backTrans.localPosition - new Vector3(_backLength / 2, _backTrans.localPosition.y, _backTrans.localPosition.z);
            Vector3 _endPos = _backTrans.localPosition + new Vector3(_backLength / 2, _backTrans.localPosition.y, _backTrans.localPosition.z);

            PointerLoopMoving(_pointersTrans, _startPos, _endPos);

            var _pointAreaStartPos = _pointAreaTrans.localPosition - new Vector3(_pointAreaLength / 2, _pointersTrans.localPosition.y, _pointersTrans.localPosition.z);
            var _pointAreaEndPos = _pointAreaTrans.localPosition + new Vector3(_pointAreaLength / 2, _pointersTrans.localPosition.y, _pointersTrans.localPosition.z);
            JudgePlayerScore(_pointersTrans, _pointAreaStartPos, _pointAreaEndPos);

        }
        private void Reactive()
        {
            unit.onInteractStart
                .Subscribe(x =>
                {
                    RunFishGame(true);
                });
        }
        private FishingGameView Init()
        {
            back_tran = transform.Find("background");
            pointArea_tran = transform.Find("pointAera");
            pointers_tran = transform.Find("pointers");
            unit = InterfaceArichives.Archive.IarbitorSystem.facilityInteractionHandle.fishUnit;
            return this;
        }
        private FishingGameView Config()
        {
            ShowChildren(false);
            if (Debug.isDebugBuild)
            {
                _config = GameConfig.Singleton.InteractionConfig;
                if (_config == null)
                {
                    Debug.Log("config is null,this made the FishignGameView.cs crashed");
                }
                else
                {
                    Debug.Log("config is correct");
                }
            }
            return this;
        }

        private void Start()
        {
            Init()
                .Config()
                .Reactive();
        }
    }
}
