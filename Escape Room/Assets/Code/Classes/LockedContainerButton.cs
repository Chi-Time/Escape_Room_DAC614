using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LockedContainerButton : Container
{
    [Tooltip ("Is the container currently unlocked.")]
    [SerializeField] private bool _IsUnlocked = false;

    public void Unlock ()
    {
        _IsUnlocked = !_IsUnlocked;

        //TODO: Play sound/animation based on this happening.
    }

    protected override void OnMouseDown ()
    {
        if (_IsUnlocked)
        {
            print ("Unlocked");
            Clicked ();
        }
        else
        {
            //TODO: Print that it's locked.
        }
    }
}
