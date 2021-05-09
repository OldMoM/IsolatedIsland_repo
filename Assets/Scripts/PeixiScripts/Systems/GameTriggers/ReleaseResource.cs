using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Peixi
{
    public static class ReleaseResource
    {
        private static Dictionary<string, IDisposable> resource = new Dictionary<string, IDisposable>();
        public static void RegisterInterface(string name,IDisposable disposable)
        {
            resource.Add(name, disposable);
        }
        public static void RemoveResource(string name)
        {
            resource[name].Dispose();
            resource.Remove(name);
        }
    }
}
