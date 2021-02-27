using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;
namespace Peixi
{
    public class PrefabFactory : MonoBehaviour,IPrefabFactory
    {
        public List<GameObject> items;
        public static IPrefabFactory singleton 
        {
            get
            {
                if (_singleton is null)
                {
                    _singleton = FindObjectOfType<PrefabFactory>();
                }
                return _singleton;
            }
        }

        private static IPrefabFactory _singleton;


        private Transform activatedObjects_tran;
        private Transform dormantObjects_tran;
        private Dictionary<int, GameObject> activedGameObjects = new Dictionary<int, GameObject>();
        private Dictionary<string, GameObject> searchCache = new Dictionary<string, GameObject>();


        private void Awake()
        {
            activatedObjects_tran = transform.Find("acivatedObjects");
            dormantObjects_tran = transform.Find("dormantObjects");

            Assert.IsNotNull(activatedObjects_tran);
            Assert.IsNotNull(dormantObjects_tran);
        }

        public GameObject creatGameobject(string name)
        {
            if (searchCache.ContainsKey(name))
            {
                return Instantiate(searchCache[name]);
            }
            else
            {
                var go = searchEntity(name);
                searchCache.Add(name, go);
                return Instantiate(go,activatedObjects_tran);
            }
        }
        public void recycleGameobject(int instanceId)
        {
            throw new System.NotImplementedException();
        }
        public void recycleGameobject(GameObject gameobject)
        {
            throw new System.NotImplementedException();
        }
        private GameObject searchEntity(string name)
        {
            var go = items.Find(x => x.name == name);
            Assert.IsNotNull(go, "at PrefabFactory.cs, go is empty");
            return go;
        }
    }
}
