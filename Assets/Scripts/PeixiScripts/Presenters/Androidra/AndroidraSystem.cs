using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UniRx;
using System;

namespace Peixi
{
    public class AndroidraSystem : MonoBehaviour,IAndroidraSystem
    {
        private AndroidraStateController state;
        private AndroidraNavPresenter nav;
        private AndroidraControl control;
        private AndroidraBuildAnimationPresenter buildAnim;

        private IPlayerSystem playerSystem => InterfaceArichives.Archive.PlayerSystem;

        public AndroidraControl Control
        {
            get
            {
                if (control is null)
                {
                    control = new AndroidraControl();
                }
                return control;
            }
        }
        public AndroidraStateController androidState => state;
        public AndroidraNavPresenter navPresenter => nav;
        public AndroidraBuildAnimationPresenter BuildAnim
        {
            get
            {
                if (buildAnim is null)
                {
                    var navAgent = GetComponent<NavMeshAgent>();
                    Assert.IsNotNull(navAgent);
                    nav = new AndroidraNavPresenter(navAgent, playerSystem, this);
                    buildAnim = new AndroidraBuildAnimationPresenter(nav);
                }
                return buildAnim;
            }
        }
        public AndroidraStateControllerModel createAndroidraStateControllerModel()
        {
            var controllerModel = new AndroidraStateControllerModel();
            controllerModel.OnBuildMsgReceived = control.OnBuildMsgReceived;
            return controllerModel;
        }
    
        // Start is called before the first frame update
        void Awake()
        {
            var navAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(navAgent);
            nav = new AndroidraNavPresenter(navAgent,playerSystem,this);
            buildAnim = new AndroidraBuildAnimationPresenter(nav);
            state = new AndroidraStateController(playerSystem, nav,createAndroidraStateControllerModel(),this);
        }
    }
}
