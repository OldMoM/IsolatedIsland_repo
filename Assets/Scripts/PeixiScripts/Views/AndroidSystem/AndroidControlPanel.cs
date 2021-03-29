﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Assertions;
using Extensions;
using System;
using UniRx;
using UniRx.Triggers;
using Siwei;

namespace Peixi
{
    public class AndroidControlPanel : MonoBehaviour
    {
        private GameObject mainControl_go;
        private GameObject openInventory_go;
        private GameObject startBuild_go;
        private GameObject checkSketch_go;
        private List<GameObject> childGameobjects = new List<GameObject>();

        private Button mainControlBtn;
        private Button openInventoryBtn;
        private Button startBuildBtn;
        private Button checkSketchBtn;

        private bool active;
        private bool inventoryState;

        private IBuildSketch buildSketch;

        // Start is called before the first frame update
        void Start()
        {
            Config()
                .React(onMainControlBtnPressed)
                .React(onCheckSketchBtnPressed)
                .React(onStartBuildBtnPressed)
                .React(onOpenInventoryBtnPressed);
        }
        AndroidControlPanel Config()
        {
            var mainControl_trans = transform.Find("mainControlBtn");
            var openInventory_trans = mainControl_trans.Find("openInventoryBtn");
            var startBuild_trans = mainControl_trans.Find("startBuildBtn");
            var checkSketch_trans = mainControl_trans.Find("checkSketchBtn");
            buildSketch = FindObjectOfType<BuildSketch>();

            mainControl_go = mainControl_trans.gameObject;
            openInventory_go = openInventory_trans.gameObject;
            startBuild_go = startBuild_trans.gameObject;
            checkSketch_go = checkSketch_trans.gameObject;

            childGameobjects
                .AddItem(openInventory_go)
                .AddItem(startBuild_go)
                .AddItem(checkSketch_go);

            childGameobjects.ForEach(x =>
            {
                Assert.IsNotNull(x, x.name + " is empty");
            });

            mainControlBtn = mainControl_trans.GetComponent<Button>();
            openInventoryBtn = openInventory_trans.GetComponent<Button>();
            startBuildBtn = startBuild_trans.GetComponent<Button>();
            checkSketchBtn = checkSketch_trans.GetComponent<Button>();

            Assert.IsNotNull(mainControlBtn, "mainControlBtn is null");
            Assert.IsNotNull(openInventoryBtn, "openInventoryBtn is null");
            Assert.IsNotNull(startBuildBtn, "startBuildBtn is null");
            Assert.IsNotNull(checkSketchBtn, "checkSketchBtn is null");
            Assert.IsNotNull(buildSketch, "buildSketch is null");

            return this;
        }
        AndroidControlPanel Switcher()
        {
            active = !active;
            childGameobjects.ForEach(x =>
            {
                x.SetActive(active);
            });
            return this;
        }
        AndroidControlPanel React(Action action)
        {
            action();
            return this;
        }
        void onMainControlBtnPressed()
        {
            mainControlBtn.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    Switcher();

                    AudioEvents.StartAudio("OnMainControlBtnPressed");
                });
        }
        void onOpenInventoryBtnPressed()
        {
            openInventoryBtn.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    inventoryState = !inventoryState;
                    InterfaceArichives
                        .Archive
                        .InGameUIComponentsManager
                        .InventoryGui
                        .SetActive(inventoryState);

                    AudioEvents.StartAudio("OnNormalBtnPressed");
                });
        }
        void onStartBuildBtnPressed()
        {
            //startBuildBtn.OnPointerClickAsObservable()
            //    .Subscribe(x =>
            //    {
            //        var activeState = buildSketch.SetBuildMode;
            //        activeState = !activeState;
            //        buildSketch.SetBuildMode = activeState;
            //        Debug.Log("build btn pressed");

            //        AudioEvents.StartAudio("OnNormalBtnPressed");
            //    });

            startBuildBtn.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    var activeState = buildSketch.SetBuildMode;
                    activeState = !activeState;
                    buildSketch.SetBuildMode = activeState;

                    AudioEvents.StartAudio("OnNormalBtnPressed");
                });
        }
        void onCheckSketchBtnPressed()
        {
            checkSketchBtn.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    print("check sktech");
                });
        }
        public void OnOpenInventoryBtnPressed()
        {
            Debug.Log("inventoryBtn pressed");
            inventoryState = !inventoryState;
            InterfaceArichives
                .Archive
                .InGameUIComponentsManager
                .InventoryGui
                .SetActive(inventoryState);

            AudioEvents.StartAudio("OnNormalBtnPressed");
        }
    }
}
