using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
//using Siwei;
using UnityEngine.Assertions;
using System;

public class InterfaceArichives : MonoBehaviour, IInterfaceArchive
{
    private static IInterfaceArchive _archive;
    private IPlayerPropertySystem _playerPropertySystem;
    private IBuildSystem _buildSystem;
    private ITimeSystem _timeSystem;
    private IInventorySystem _inventorySystem;
    private IArbitorSystem _arbitorSystem;
    private IInGameUIComponentsInterface inGameUiComponentsManager;

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
    public IBuildSystem IBuildSystem
    {
        get
        {
            if (_buildSystem is null)
            {
                _buildSystem = FindObjectOfType<BuildSystem>();
            }
            return _buildSystem;
        }
    }
    public IPlayerPropertySystem IPlayerPropertySystem
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
    }
    public ITimeSystem ITimeSystem 
    {
        get
        {
            _timeSystem = FindObjectOfType<TimeSystem>();
            if (_timeSystem is null)
            {
                Debug.LogWarning("未能在Hierarchy中找到ITimeSystem接口，将返回null");
            }
            return _timeSystem;
        }
    }
    public IInventorySystem IInventorySystem 
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
    public IArbitorSystem IArbitorSystem
    {
        get
        {
            _arbitorSystem = FindObjectOfType<ArbitorSystem>();
            if (_arbitorSystem is null)
            {
                Debug.LogWarning("未能在Hierarchy中找到IArbitorSystem接口，将返回null");
            }
            return _arbitorSystem;
        }
    }
    public IInGameUIComponentsInterface InGameUIComponentsManager
    {
        get
        {
            if (inGameUiComponentsManager is null)
            {
                inGameUiComponentsManager = FindObjectOfType<InGameUIComponentInterface>();
                Assert.IsNotNull(inGameUiComponentsManager, "IInGameUIComponentsManager");
            }
            return inGameUiComponentsManager;
        }    
    }
}
