using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//TODO: Make switch able to change main environment.

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
class Switch : MonoBehaviour
{
    [Tooltip ("Will pressing the switch relock the container?")]
    [SerializeField] private bool _CanRelock = false;
    [Tooltip ("The audio clip to play when the object is clicked.")]
    [SerializeField] private AudioClip _ClickClip = null;
    [Tooltip ("The locked container to open upon pressing the switch.")]
    [SerializeField] private LockedContainerButton _LockedContainer = null;

    private AudioSource _AudioSource = null;

    private void Awake ()
    {
        _AudioSource = GetComponent<AudioSource> ();
    }

    private void OnMouseDown ()
    {
        if (_CanRelock == false)
        {
            _LockedContainer.Unlock ();
            this.enabled = false;

            if (_ClickClip != null)
                _AudioSource.PlayOneShot (_ClickClip);
        }
        else
        {
            _LockedContainer.Unlock ();
        }
    }
}
