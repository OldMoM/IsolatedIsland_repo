using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Peixi;

public class AndroidraNavComponentTest : MonoBehaviour
{
    private IPlayerSystem player;
    void Start()
    {
        player = InterfaceArichives.Archive.PlayerSystem;
    }
    private void Update()
    {
        var playerPos = player.Rigid.position;
        var playerFaceDir = player.Movement.FaceDirection;
        var target = playerPos + playerFaceDir * -2;
        transform.position = target;
    }
}
