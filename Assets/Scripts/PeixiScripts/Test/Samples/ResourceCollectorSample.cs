﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ResourceCollectorSample : MonoBehaviour,IInteractableResourceCollector
{
    public GameObject Icon;
    public int resourceAccount_foodMaterial = 3;
    public int resourceAccount_buildingMaterial = 4;
    public string resourceType = "FoodMaterial";
    public string interactObjectType = "ResourceCollector";
    public int ResourceAccount_Food { get => resourceAccount_foodMaterial; }
    public string ResourceType { get => resourceType; }
    public string InteractObjectType { get => interactObjectType; }

    public int ResourceAccount_Build => resourceAccount_buildingMaterial;

    public Vector3 IconOffset = new Vector3(0, 7, 0);
    bool isSick = false;

    private void Start()
    {
        Icon = FindObjectOfType<IconManager>().CollectFoodIcon;
        if (Icon == null)
        {
            Debug.LogError("Icon haven't been assigned to IconManager");
        }
        GameEvents.Sigton.onNPCSicked
         .Subscribe(x =>
         {
             isSick = true;
         });
        GameEvents.Sigton.onNPCSickedEnd
             .Subscribe(x =>
             {
                 isSick = false;
             });
        GameEvents.Sigton.onPlayerSicked
             .Subscribe(x =>
             {
                 isSick = true;
             });
        GameEvents.Sigton.onPlayerSickedEnd
             .Subscribe(x =>
             {
                 isSick = false;
             });
    }

    private void Update()
    {
        Icon.transform.position = Camera.main.WorldToScreenPoint(transform.position + IconOffset);
    }

    public void EndContact()
    {
        if (!isSick)
        {
            Mediator.Sigton.EndInteract();
        }
    }

    public void EndInteract(object result)
    {

        bool _res = (bool)result;
        if (_res)
        {
            resourceAccount_foodMaterial = 0;
        }
        
    }

    public void StartInteract()
    {
        
    }

    public void StartContact()
    {

        if (resourceAccount_foodMaterial > 0 && !isSick)
        {
            //向Mediator通知要进行的互动行为
            Mediator.Sigton.StartInteraction(this);
        }
    }

    public void ShowIcon()
    {
        Icon.SetActive(true);
    }

    public void HideIcon()
    {
        Icon.SetActive(false);
    }

}
