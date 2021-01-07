using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ListExtension 
    {
        public static List<T> AddItem<T>(this List<T> list,T item)
        {
            list.Add(item);
            return list;
        }
    }
}


