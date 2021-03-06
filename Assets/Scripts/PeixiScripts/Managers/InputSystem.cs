﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class InputSystem : MonoBehaviour, IKeyboardInput
{
    static IKeyboardInput _instance;
    Subject<string> onInteractBtnPressed = new Subject<string>();
    Subject<string> onInteractBtnReleased = new Subject<string>();
    Subject<string> onInteractBtnPressing = new Subject<string>();
    public static IKeyboardInput Singleton
    {
        get  
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InputSystem>();
            }
            return _instance;
        }
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }
    #region//interface implement
    public Subject<string> OnInteractBtnPressed { get => onInteractBtnPressed; }
    public Subject<string> OnInteractBtnReleased { get => onInteractBtnReleased; }
    public Subject<string> OnInteractBtnPressing { get => onInteractBtnPressing; }
    #endregion

    private void Awake()
    {
        _instance = this;

        Observable.EveryUpdate()
            .Where(x => Input.GetButtonDown("Interact"))
            .Subscribe(x =>
            {
                onInteractBtnPressed.OnNext("e");
            });

        Observable.EveryUpdate()
            .Where(x => Input.GetButtonUp("Interact"))
            .Subscribe(x =>
            {
                onInteractBtnReleased.OnNext("e");
            });

        Observable.EveryUpdate()
            .Where(x => Input.GetButton("Interact"))
            .Subscribe(x =>
            {
                onInteractBtnPressing.OnNext("e");
            });
    }
}
