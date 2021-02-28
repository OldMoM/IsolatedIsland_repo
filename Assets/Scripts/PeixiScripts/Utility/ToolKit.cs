
namespace Peixi
{
    public static class ToolKit 
    {
        private static IPrefabFactory _prefabFactory;
        /// <summary>
        /// 预制体工厂
        /// </summary>
        public static IPrefabFactory prefabFactory
        {
            get
            {
                if (_prefabFactory == null)
                {
                    _prefabFactory = PrefabFactory.singleton;
                }
                return _prefabFactory;
            }  
        }
    }
}
