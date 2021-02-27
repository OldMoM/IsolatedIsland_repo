using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    //玩家移动数值逻辑计算模块
    [Serializable]
    public class PlayerMovementPresenter 
    {
        //-----公共方法-----
        /// <summary>
        /// 屏幕坐标系下，面部方向改变时触发此事件
        /// </summary>
        public IObservable<Vector3> OnFaceDirectionScreenChanged => faceDir_screen;
        /// <summary>
        /// 玩家ISO坐标系下面部朝向
        /// </summary>
        public Vector3 FaceDirection => faceDir;
        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed = 5;
        /// <summary>
        /// 当玩家速度改变时触发此事件
        /// </summary>
        public IObservable<Vector3> OnVelocityChanged => velocity;
        /// <summary>
        /// 玩家当前的速度
        /// </summary>
        public Vector3 Velocity => velocity.Value;

        //-----内部变量-----
        private Vector3ReactiveProperty velocity = new Vector3ReactiveProperty();
        private Vector3ReactiveProperty faceDir_screen = new Vector3ReactiveProperty();
        private PlayerMovementModel movementModel = new PlayerMovementModel();
        private PlayerStateModel stateModel = new PlayerStateModel();
        private IPlayerSystem m_playerSystem;
        private Vector3 faceDir;

        //-----内部方法-----
        private void Init()
        {
            React(OnPlayerInput)
                .React(OnInteractStart);
        }
        private PlayerMovementPresenter React(Action action)
        {
            action();
            return this;
        }
        /// <summary>
        /// ISO坐标朝向
        /// </summary>
        /// <param name="moveDir"></param>
        private void ComputeFaceDir_ISO(Vector3 moveDir)
        {
            if (moveDir == Vector3.zero)
            {
                return;
            }

            faceDir = moveDir;

        }
        /// <summary>
        /// 计算玩家在屏幕坐标中面部方向
        /// </summary>
        /// <param name="moveDirection"></param>
        /// <returns></returns>
        private Vector3 ComputeFaceDirection_screen(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
            {
                return faceDir_screen.Value;
            }
            return moveDirection;
        }
        /// <summary>
        /// 旋转XOZ平面的移动速度
        /// </summary>
        /// <param name="velocity">原始输入数据</param>
        /// <param name="degree">顺时针旋转为负</param>
        /// <returns></returns>
        private Vector3 RotateVelocityBy(Vector3 velocity, float degree)
        {
            var _velocity = velocity;
            var x_rotated = _velocity.x * Mathf.Cos(degree * Mathf.Rad2Deg) - _velocity.z * Mathf.Sin(degree * Mathf.Rad2Deg);
            x_rotated = -x_rotated;
            var y = m_playerSystem.Rigid.velocity.y;
            var z_rotated = _velocity.x * Mathf.Sin(degree * Mathf.Rad2Deg) + _velocity.z * Mathf.Cos(degree * Mathf.Rad2Deg);
            z_rotated = -z_rotated;
            var velocity_rotated = new Vector3(x_rotated, y, z_rotated);
            return velocity_rotated;
        }

        //-----响应事件-----
        private void OnPlayerInput()
        {
            Observable.EveryFixedUpdate()
              .Where(x => PlayerState != PlayerState.InteractState)
              .Subscribe(x =>
              {
                  Vector3 _direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                  _direction = -_direction.normalized;
                  faceDir_screen.Value = ComputeFaceDirection_screen(_direction);
                  _direction = RotateVelocityBy(_direction, -60);

                  var _velocity = _direction * moveSpeed;
                  //velocity.Value = RotateVelocityBy(_velocity, -60);
                  velocity.Value = _velocity;
                  ComputeFaceDir_ISO(_direction);
              });
        }
        private void OnInteractStart()
        {
            OnPlayerStateChanged
               .Where(x => x == PlayerState.InteractState)
               .Subscribe(x =>
               {
                   velocity.Value = Vector3.zero;
               });
        }

        //-----内部属性，获取引用地址-----
        private IObservable<PlayerState> OnPlayerStateChanged => m_playerSystem.StateController.onStateChanged;
        private PlayerState PlayerState => m_playerSystem.StateController.playerState.Value;

        //-----运行入口-----
        public PlayerMovementPresenter(IPlayerSystem playerSystem)
        {
            m_playerSystem = playerSystem;
            Init();
        }
    }
}
