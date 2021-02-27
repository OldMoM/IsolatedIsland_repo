using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    //控制玩家动画播放
    public class PlayerAnimationView 
    {
        //-----依赖注入变量-----
        private IObservable<Vector3>  faceDirection_screen;
        private Transform             animtionRunRoot;
        private PlayerStateController state;
        private Animator              anim;
        //-----内部方法-----
        private PlayerAnimationView React(Action action)
        {
            action();
            return this;
        }
        //-----响应条件-----
        private void OnPlayerStateChanged()
        {
            state.onStateChanged
                .Where(x => x == PlayerState.IdleState)
                .Subscribe(x =>
                {
                    anim.SetBool("isWalk", false);
                });

            state.onStateChanged
                .Where(x => x == PlayerState.MotionState)
                .Subscribe(x =>
                {
                    anim.SetBool("isWalk", true);
                });
        }
        private void OnFaceDirectionChanged()
        {
            faceDirection_screen.Subscribe(x =>
            {
                if (x.x > 0)
                {
                    animtionRunRoot.localRotation = Quaternion.Euler(0, 45, 0);
                }
                else if (x.x < 0)
                {
                    animtionRunRoot.localRotation = Quaternion.Euler(0, -135, 0);
                }
            });
        }
        private void OnInteractStart()
        {
            state.onStateChanged
                .Where(x => x == PlayerState.InteractState)
                .Subscribe(x =>
                {
                    anim.SetBool("isWalk", false);
                });
        }
        //-----运行入口-----
        public PlayerAnimationView(Animator              animator, 
                                   PlayerStateController stateController,
                                   IObservable<Vector3>  faceDirection)
        {
            //config animation controller view
            anim = animator;
            state = stateController;
            animtionRunRoot = anim.transform;
            faceDirection_screen = faceDirection;

            React(OnPlayerStateChanged)
                .React(OnFaceDirectionChanged)
                .React(OnInteractStart);
        }
    }
}
