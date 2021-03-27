using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioRegistration
{
    public static Dictionary<string, string> audioTable = new Dictionary<string, string> {
        {"OnMainControlBtnPressed", ""},
        { "OnNormalBtnPressed", ""},
        { "OnItemUsed", ""},
        { "OnIslandBuildStart", ""},
        { "OnIslandBuildCompleted", ""},
        { "OnTipPopped", ""},
        { "OnDialogStart", ""},
        { "OnPlayerGetHungery", ""},
        { "OnPlayerGetExtremeHungery", ""},
        { "OnPlayerGetExtremeThirsty", ""},
        { "OnPlayerGetExhausted", ""},
        { "OnStopMenuEnabled", ""},
        { "OnChatBubbleEnabled", ""},
        { "OnFishingStart", ""},
        { "OnCatcherStart", ""},
        { "OnFoodPlantStart", ""},
        { "OnWaterPuifierStart", ""},
        { "OnRestStart", ""},
        { "OnPlayerPickFruit", ""},
        { "OnPlayerPickFloatingGarbage", ""},
        { "OnAndroidraActived", ""},
        { "OnRestoreIslandStart", ""},
        { "OnRainDay", ""},
        { "OnNightTime", ""}
    };
}
