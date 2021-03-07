using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using Siwei;
using UnityEngine.Assertions;
using System;
using Caoye;

public class InterfaceArichives : MonoBehaviour, IInterfaceArchive
{
    private static IInterfaceArchive _archive;
    private IPlayerPropertySystem _playerPropertySystem;
    private IBuildSystem _buildSystem;
    private ITimeSystem _timeSystem;
    private IInventorySystem _inventorySystem;
    private IArbitorSystem _arbitorSystem;
    private IInGameUIComponentsInterface inGameUiComponentsManager;
    private IPlayerSystem playerSystem;
    private IPlayerPropertySystem propertySystem;
    private IAndroidraSystem androidraSystem;
    private IBuildSketch buildSketch;
    private IDialogSystem dialogSystem;

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
            Assert.IsNotNull(_buildSystem);
            return _buildSystem;
        }
    }
    public IPlayerPropertySystem IPlayerPropertySystem => PlayerSystem.PlayerPropertySystem;
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
            }

            if (_inventorySystem is null)
            {
                throw new Exception("未在Hierarchy中部署GameSystems.prefab");
            }
            return _inventorySystem;
        }
    }
    public IArbitorSystem IArbitorSystem
    {
        get
        {
            if (_arbitorSystem is null)
            {
                _arbitorSystem = FindObjectOfType<ArbitorSystem>();
            }

            if (_arbitorSystem is null)
            {
                throw new Exception("未在Hierarchy中部署GameSystems.prefab");
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
    public IPlayerSystem PlayerSystem 
    {
        get
        {
            if (playerSystem is null)
            {
                playerSystem = FindObjectOfType<PlayerSystem>();
            }
            Assert.IsNotNull(playerSystem, "未在Hierarchy中部署PlayerHandle.prefab");
            return playerSystem;
        }
    }
    public IAndroidraSystem IAndroidraSystem
    {
        get
        {
            if (androidraSystem is null)
            {
                androidraSystem = FindObjectOfType<AndroidraSystem>();
            }
            Assert.IsNotNull(androidraSystem, "未在Hierarchy中部署Androidra.prefab");
            return androidraSystem;
        }
    }
    public IBuildSketch IBuildSketch
    {
        get
        {
            if (buildSketch is null)
            {
                buildSketch = FindObjectOfType<BuildSketch>();
            }
            Assert.IsNotNull(buildSketch, "未在Hierarchy中部署BuildSketch.prefab");
            return buildSketch;
        }
    }

    public IDialogSystem IDialogSystem
    {
        get
        {
            if (dialogSystem is null)
            {
                dialogSystem = FindObjectOfType<LvDialogSystem>();
            }
            Assert.IsNotNull(dialogSystem, "未在Hierarchy中部署DialogueSystem.prefab");
            return dialogSystem;
        }
    }
}
