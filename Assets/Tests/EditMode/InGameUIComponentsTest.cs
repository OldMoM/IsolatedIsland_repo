using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
namespace Tests
{
    public class InGameUIComponentsTest
    {
        // A Test behaves as an ordinary method
        IInGameUIComponents CreateInGameHudCanvas()
        {
            var inGameUICompoentsPrefab = LoadPrefab.GetUIPrefab(UIComponentsTags.InGameHudCanvas);
            return inGameUICompoentsPrefab.GetComponent<InGameUIComponents>();
        }

        [Test]
        public void GetChatBubble_notNull()
        {

            var testObject = CreateInGameHudCanvas();
            Assert.IsNotNull(testObject.ChatBubble);
        }
        [Test]
        public void GetInventoryGui_notNull()
        {
            var testObject = CreateInGameHudCanvas();
            Assert.IsNotNull(testObject.InventoryGui);
        }
        [Test]
        public void GetPlayerPropertyHUD_NotNull()
        {
            var testObject = CreateInGameHudCanvas();
            Assert.IsNotNull(testObject.PlayerPropertyHUD);
        }
    }
}
