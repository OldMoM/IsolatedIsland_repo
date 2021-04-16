using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioRegistration
{
    public static Dictionary<string, string> audioTable = new Dictionary<string, string> {
        // BGM
        {"MainMenu", "1_BGM_MainMenu"},
        {"OnDayTime", "2_BGM_InGame"},
        // temporary for 1, 2


        { "OnRainDay", "3_BGM_Rain"},
        { "OnNightTime", ""},
        // UI 
        {"OnMainControlBtnPressed", "1_UI_RobotButton"},
        { "OnNormalBtnPressed", "2_UI_ButtonClick"},
        { "OnItemUsed", "3_UI_Props_Eat_Drink"},
        { "OnIslandBuildStart", "4_UI_IslandBuild"},
        { "OnIslandBuildCompleted", "4_UI_IslandBuild"},
        { "OnTipPopped", "5_UI_GuidancePop"},
        { "OnDialogStart", ""},
        { "OnPlayerGetHungery", "6_UI_InitialHunger"},
        { "OnPlayerGetExtremeHungery", "7_UI_ExtremeHunger"},
        { "OnPlayerGetExtremeThirsty", "8_UI_ExtremeThirst"},
        { "OnPlayerGetExhausted", "9_UI_Exhaust"},
        { "OnStopMenuEnabled", "10_UI_Pause"},
        { "OnChatBubbleEnabled", ""},
        // Scene 
        { "OnFishingStart", "1_Scene_Fishing"},
        { "OnCatcherStart", "2_Scene_FishingFacility"},
        { "OnFoodPlantStart", "3_Scene_Cooking"},
        { "OnWaterPuifierStart", "4_Scene_Distiller"},
        { "OnRestStart", "5_Scene_Rest"},
        { "OnPlayerPickFruit", "6_Scene_GainResource"},
        { "OnPlayerPickFloatingGarbage", "6_Scene_GainResource"},
        { "OnAndroidraActived", "6_Scene_GainResource"},
        { "OnRestoreIslandStart", "7_Scene_IslandFix"}
        
        
    };
}
