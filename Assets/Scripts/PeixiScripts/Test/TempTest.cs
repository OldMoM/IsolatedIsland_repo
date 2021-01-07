using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Peixi
{
    public class TempTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var garbage = FindObjectOfType<GarbagePresenter>();
            garbage.Active(7, Vector3.left);
        }
    }
}
