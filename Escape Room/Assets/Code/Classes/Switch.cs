using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//TODO: Make switch able to change main environment.

class Switch : MonoBehaviour
{
    [Tooltip ("Will pressing the switch relock the container?")]
    [SerializeField] private bool _CanRelock = false;
    [Tooltip ("The locked container to open upon pressing the switch.")]
    [SerializeField] private LockedContainerButton _LockedContainer = null;

    private void OnMouseDown ()
    {
        if (_CanRelock == false)
        {
            _LockedContainer.Unlock ();
            this.enabled = false;
        }
        else
        {
            _LockedContainer.Unlock ();
        }
    }
}
