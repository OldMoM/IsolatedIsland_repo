using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using Siwei;

public class InterfaceArichives : MonoBehaviour, IInterfaceArchive
{
    private static IInterfaceArchive _archive;
    private IPlayerPropertySystem _playerPropertySystem;
    private IBuildSystem _buildSystem;
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
                return _buildSystem;
            }
            Debug.Log("未能在Hierarchy中找到IbuildSystem接口，确保其中存在BuildSystem，将返回null");
            return _buildSystem;
        }
    
    }
    public IPlayerPropertySystem playerPropertySystem 
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
                playerPropertySystem = value;
            }
            throw new System.Exception("IPlayerPropertySystem接口已经被注册过一次，请勿重复注册");
        } 
    }
}
