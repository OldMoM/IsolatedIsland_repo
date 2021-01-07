using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Peixi
{
    public class ItemEditor : BaseItem
    {

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, data.detectRadius);
        }

    }
}
