using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
//using UnityEditor;
namespace Peixi {
    public class GarbageGenerator : MonoBehaviour
    {
        ReactiveProperty<bool> isActive = new ReactiveProperty<bool>(false);
        System.IDisposable generateGarbageMicrotime;
        float garbageGenerateIntervalTime = 2;
        [Range(1,4)]
        public float floatSpeed = 2.5f;

        public Transform garbageDes;
        public GameObject[] garbageModels;
        public List<Transform> generateBoundPoints;
       
        //-----Thesa vars are for Editor-----
        internal List<Vector3> randomPoints;
        internal List<Vector3> directionVecs = new List<Vector3>();

        // Start is called before the first frame update
        void Start()
        { 
            Observable
                .Interval(System.TimeSpan.FromSeconds(5))
                .Subscribe(x =>
                {
                    var spawnPosition = GenerateSpawnPointRandomly();
                    var direction = (Vector3.zero - spawnPosition).normalized;
                    var garbage_prefab = GameObject.Instantiate(
                        garbageModels[0],
                        spawnPosition,
                        Quaternion.Euler(direction));
                    var garbage_script = garbage_prefab.GetComponent<GarbagePresenter>();
                    garbage_script.Active(floatSpeed, direction);
                    garbage_prefab.transform.right = direction;
                });

        }
        Vector3 GenerateSpawnPointRandomly()
        {
            int maxPointNum = generateBoundPoints.Count - 1;
            int point1Num = Random.Range(0, generateBoundPoints.Count);
            int point2Num = point1Num + 1;
            if (point2Num > maxPointNum)
            {
                point2Num = 0;
            }
            var lerpValue = Random.Range(0, 1.0f);
            var point1 = generateBoundPoints[point1Num].position;
            var point2 = generateBoundPoints[point2Num].position;
            var spawnPoint = Vector3.Lerp(point1, point2, lerpValue);
            return spawnPoint;
        }
    }
}
