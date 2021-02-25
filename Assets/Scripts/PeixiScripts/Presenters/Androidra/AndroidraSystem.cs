using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UniRx;

namespace Peixi
{
    public class AndroidraSystem : MonoBehaviour
    {
        private AndroidraStateController state;
        private AndroidraNavPresenter nav;

        private IPlayerSystem playerSystem => InterfaceArichives.Archive.PlayerSystem;

        // Start is called before the first frame update
        void Start()
        {

            var navAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(navAgent);
            nav = new AndroidraNavPresenter(navAgent,playerSystem);
            state = new AndroidraStateController(playerSystem, nav);
        }
    }
}
