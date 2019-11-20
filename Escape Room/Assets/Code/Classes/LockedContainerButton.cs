using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LockedContainerButton : Container
{
    [Tooltip ("The message to print when the object is locked.\nIf no message is provided it won't say anything.")]
    [SerializeField] private TextAsset _LockedMessage = null;
    [Tooltip ("The message to print when the object is unlocked.\nIf no message is provided it won't say anything.")]
    [SerializeField] private TextAsset _UnlockedMessage = null;
    [Tooltip ("Is the container currently unlocked.")]
    [SerializeField] private bool _IsUnlocked = false;

    public void Unlock ()
    {
        _IsUnlocked = !_IsUnlocked;
        Signals.PumpMessage (_UnlockedMessage);

        //TODO: Play sound/animation based on this happening.
    }

    protected override void OnMouseDown ()
    {
        if (_IsUnlocked)
        {
            Clicked ();
        }
        else
        {
            Signals.PumpMessage (_LockedMessage);
        }
    }
}
