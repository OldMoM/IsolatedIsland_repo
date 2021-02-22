using UnityEngine.AI;
using UniRx;
using UnityEngine;
using System;

namespace Peixi
{
    public class AndroidraNavPresenter 
    {
        private NavMeshAgent _agent;

        private BoolReactiveProperty isMoving = new BoolReactiveProperty();

        private Subject<Unit> onAndroidraStartMove = new Subject<Unit>();
        private Subject<Unit> onAndroidraEndMove = new Subject<Unit>();
        private Vector3 target;
        private IPlayerSystem _playerSystem;
        public IObservable<Unit> OnAndroidraStartMove => onAndroidraStartMove;
        public IObservable<Unit> OnAndroidraEndMove => onAndroidraEndMove;
 
        public AndroidraNavPresenter(NavMeshAgent agent,IPlayerSystem playerSystem)
        {
            _agent = agent;
            _playerSystem = playerSystem;

            //Observable.EveryUpdate()
            //.Where(x=> Input.GetMouseButtonDown(0))
            //.Subscribe(x =>
            //{
            //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    var hit = new RaycastHit();

            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        if (hit.collider.name == "Quad")
            //        {
            //            Vector3 pos = hit.point;
            //            _agent.destination = pos;

            //            var playerPos = _playerSystem.Rigid.position;
            //            var playerFaceDir = _playerSystem.Movement.FaceDirection;
            //            target = playerPos + playerFaceDir * -2;

            //            _agent.destination = target;

            //        }
            //    }
            //});

            Observable.EveryUpdate()
                .Subscribe(x =>
                {
                    var playerPos = _playerSystem.Rigid.position;
                    var playerFaceDir = _playerSystem.Movement.FaceDirection;
                    target = playerPos + playerFaceDir * -0.5f;

                    _agent.destination = target;
                });

            Observable.EveryLateUpdate()
                .Where(x => _agent.velocity.magnitude > 0.01f)
                .Subscribe(x =>
                {
                    isMoving.Value = true;
                });

            Observable.EveryLateUpdate()
                .Where(x => _agent.velocity.magnitude <= 0.01f)
                .Subscribe(x =>
                {
                    isMoving.Value = false;
                });

            isMoving
                .Skip(1)
                .Subscribe(x =>
                {
                    if (x)
                    {
                        onAndroidraStartMove.OnNext(Unit.Default);
                    }
                    else
                    {
                        onAndroidraEndMove.OnNext(Unit.Default);
                    }
                });
        }
    }
}
