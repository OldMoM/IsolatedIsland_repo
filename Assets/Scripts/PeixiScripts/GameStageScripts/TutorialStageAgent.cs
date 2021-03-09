﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class TutorialStageAgent : MonoBehaviour
    {
        readonly string onPropertySpecification_Start = "onPropertySpecification_Start";
        readonly string onPropertySpecification_GetFruit = "onPropertySpecification_GetFruit";
        readonly string onPropertySpecification_OpenInventory = "onPropertySpecification_OpenInventory";
        readonly string onPropertySpecification_EatFruit = "onPropertySpecification_EatFruit";
        readonly string onPropertySpecification_Completed = "onPropertySpecification_Completed";
        readonly string onPlayerTouchWithTent = "onPlayerTouchWithTent";

        readonly string on2ndDayStart = "on2ndDayStart";
        readonly string onPlayerPickUpMaterial = "onPlayerPickUpMaterial";
        readonly string onWaterCollectAvailable = "onWaterCollectAvailable";
        readonly string onWaterCollectCompleted = "onWaterCollectCompleted";

        private IInventorySystem inventory => InterfaceArichives.Archive.IInventorySystem;
        private IInventoryGui inventoryGui;
        private Tent tent;


        private void Start()
        {
            inventoryGui = InterfaceArichives.Archive.InGameUIComponentsManager.InventoryGui;
            tent = FindObjectOfType<Tent>();

            inventory.OnInventoryChanged
                .Where(x => x.NewValue.Name == "Apple")
                .Subscribe(x => { GameStageManager.StartStage(onPropertySpecification_GetFruit); });

            inventoryGui.OnOpenStateChanged
                .Where(x => x)
                .Subscribe(x => { GameStageManager.StartStage(onPropertySpecification_OpenInventory); });

            inventoryGui.OnItemUsed
                .Where(x => x == "Apple")
                .Subscribe(x => { GameStageManager.StartStage(onPropertySpecification_EatFruit); });

            tent.CollectableAgent.OnPlayerTouch
                 .First()
                 .Subscribe(x =>{ GameStageManager.StartStage(onPlayerTouchWithTent); });
                
        }
    }
}
