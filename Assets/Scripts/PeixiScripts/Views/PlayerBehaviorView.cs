using UniRx;
using UnityEngine;

namespace Peixi
{
    public class PlayerBehaviorView : MonoBehaviour
    {
        Rigidbody rigid;
        Animator anim;

        public MeshRenderer meshRenderer;
        public Texture2D[] texture2Ds;
 
        [SerializeField]
        bool isWalk;

        IPlayerSystem playerSystem;

        private void Awake()
        {
   
        }

        private void Start()
        {
            playerSystem = GetComponent<PlayerSystem>();
            rigid = GetComponent<Rigidbody>();

            Observable.EveryFixedUpdate()
                .Subscribe(x =>
                {
                    var _velocity = playerSystem.Movement.Velocity;
                    rigid.velocity = _velocity;
                });
        }

        private void Update()
        {

        }
        float FilterVelocityData(float axisSpeed)
        {
            if (axisSpeed > 0)
            {
                return 1;
            }
            else if (axisSpeed < 0)
            {
                return -1;
            }
            return 0;
        }

       

  
        void UpdateAnimator()
        {
            Observable.EveryUpdate()
                .Subscribe(x =>
                {
                    anim.SetBool("IsWalk", isWalk);
                });
        }
        void IsWalking()
        {
            Observable.EveryUpdate()
                .Where(x => isWalk)
                .Subscribe(x =>
                {
                    UpdateAnimator();
                });
        }
        void IsIdling()
        {
            Observable.EveryUpdate()
                .Where(x => !isWalk)
                .Subscribe(x =>
                {

                });
        }
        void OnVelocityChanged(Vector3 velocity)
        {
            rigid.velocity = velocity;
        }
        void SetMaterialTex(int textNum)
        {
            meshRenderer.material.SetTexture("_BaseMap", texture2Ds[textNum]);
        }
    }
}
