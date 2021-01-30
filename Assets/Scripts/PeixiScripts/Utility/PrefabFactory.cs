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

        private Dictionary<string, GameObject> searchCache = new Dictionary<string, GameObject>();

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
                return Instantiate(go);
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
