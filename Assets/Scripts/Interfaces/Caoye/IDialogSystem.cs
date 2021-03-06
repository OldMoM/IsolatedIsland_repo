using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Caoye
{
    public interface IDialogSystem 
    {
        void StartDialog(string dialogId);
        void StartSelfDialog(int characterId, string context);
        IObservable<string> OnDialogStart { get; }
        IObservable<string> OnDialogEnd { get; }
        bool isActive { get; }
    }
}
