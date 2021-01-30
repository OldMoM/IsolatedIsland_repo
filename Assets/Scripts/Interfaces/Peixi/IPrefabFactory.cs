using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi
{
    public interface IPrefabFactory 
    {
        GameObject creatGameobject(string name);
        void recycleGameobject(int instanceId);
        void recycleGameobject(GameObject gameobject); 
    }
}
