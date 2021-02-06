using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using Siwei;
using UnityEngine.Assertions;

public class InterfaceArichives : MonoBehaviour, IInterfaceArchive
{
    private static IInterfaceArchive _archive;
    private IPlayerPropertySystem _playerPropertySystem;
    private IBuildSystem _buildSystem;
    private ITimeSystem _timeSystem;
    private IInventorySystem _inventorySystem;
    private IArbitorSystem _arbitorSystem;

    private Hashtable interfaceTable = new Hashtable()
    {
        //{ typeof(IInventorySystem).ToString(),_archive.IinventorySystem}
    };

    public static IInterfaceArchive Archive
    {
        get
        {
            if (_archive == null)
            {
                _archive = FindObjectOfType<InterfaceArichives>();
            }
            return _archive;
        }
    }
    public IBuildSystem IbuildSystem
    {
        set
        {
            if (_buildSystem == null)
            {
                _buildSystem = value;
                return;
            }
            throw new System.Exception("IbuildSystem接口已经被注册过一次，请勿重复注册");
        }
        get
        {
            if (_buildSystem != null)
            {
                return _buildSystem;
            }
            //find IbuildSystem in hierarchy
            _buildSystem = FindObjectOfType<BuildSystem>();
            if (_buildSystem != null)
            {
                interfaceTable.Add("IBuildSystem", _buildSystem);
                return _buildSystem;
            }
            Debug.Log("未能在Hierarchy中找到IbuildSystem接口，确保其中存在BuildSystem，将返回null");
            return _buildSystem;
        }

    }
    public IPlayerPropertySystem IplayerPropertySystem
    {
        get
        {
            if (_playerPropertySystem == null)
            {
                //_playerPropertySystem = FindObjectOfType<PlayerPropertySystem>();
            }
            Debug.LogWarning("未能在Hierarchy中找到IbuildSystem接口，确保其中存在BuildSystem，将返回null");
            return _playerPropertySystem;
        }
        set
        {
            if (_playerPropertySystem == null)
            {
                IplayerPropertySystem = value;
            }
            throw new System.Exception("IPlayerPropertySystem接口已经被注册过一次，请勿重复注册");
        }
    }
    public ITimeSystem ItimeSystem 
    {
        get
        {
            _timeSystem = FindObjectOfType<TimeSystem>();
            interfaceTable.Add("ITimeSystem", _timeSystem);
            if (_timeSystem is null)
            {
                Debug.LogWarning("未能在Hierarchy中找到ITimeSystem接口，将返回null");
            }
            return _timeSystem;
        }
    }
    public IInventorySystem IinventorySystem 
    {
        get 
        {
            if (_inventorySystem is null)
            {
                _inventorySystem = FindObjectOfType<InventorySystem>();
                Assert.IsNotNull(_inventorySystem, "未能在Hierarchy中找到IInventorySystem接口，将返回null");
            }
            return _inventorySystem;
        }
    }
    public IArbitorSystem IarbitorSystem
    {
        get
        {
            _arbitorSystem = FindObjectOfType<ArbitorSystem>();
            interfaceTable.Add("IArbitorSystem", _arbitorSystem);
            if (_arbitorSystem is null)
            {
                Debug.LogWarning("未能在Hierarchy中找到IArbitorSystem接口，将返回null");
            }
            return _arbitorSystem;
        }
    }
    public T GetInterface<T>()
    {
        string type = typeof(T).ToString();
        var hasInterface = interfaceTable.ContainsKey(type);
        if (hasInterface)
        {
            return (T)interfaceTable[type];
        }
        else
        {
            RegisterInterface(type);
            return (T)interfaceTable[type];
        }
        throw new System.Exception();
    }

    void RegisterInterface(string name)
    {
        if (name == typeof(IInventorySystem).ToString())
        {
            _inventorySystem = FindObjectOfType<InventorySystem>();
            interfaceTable.Add(typeof(IInventorySystem).ToString(), _inventorySystem);
        }
    }
}
