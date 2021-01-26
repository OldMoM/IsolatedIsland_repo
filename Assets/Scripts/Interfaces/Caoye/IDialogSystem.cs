using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Caoye
{
    public interface IDialogSystem 
    {
        void StartDialog(string dialogId);
        IObservable<string> OnDialogStart { get; }
        IObservable<string> OnDialogEnd { get; }

    }
}
