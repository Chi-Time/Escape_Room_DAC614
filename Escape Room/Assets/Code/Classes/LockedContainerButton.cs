using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
class LockedContainerButton : Container
{
    [Tooltip ("Is the container currently unlocked.")]
    [SerializeField] private bool _IsUnlocked = false;
    [Tooltip ("The sprite to switch to when the container has been unlocked.\nIf no sprite is provided then the image won't change.")]
    [SerializeField] private Sprite _UnlockedSprite = null;
    [Tooltip ("The clip to play when interacting with the locked object.")]
    [SerializeField] private AudioClip _LockedClip = null;
    [Tooltip ("The clip to play when unlocking the object.")]
    [SerializeField] private AudioClip _UnlockedClip = null;
    [Tooltip ("The message to print when the object is locked.\nIf no message is provided it won't say anything.")]
    [SerializeField] private TextAsset _LockedMessage = null;
    [Tooltip ("The message to print when the object is unlocked.\nIf no message is provided it won't say anything.")]
    [SerializeField] private TextAsset _UnlockedMessage = null;

    public void Unlock ()
    {
        _IsUnlocked = !_IsUnlocked;
        Signals.PumpMessage (_UnlockedMessage);

        if (_UnlockedClip != null)
            _AudioSource.PlayOneShot (_UnlockedClip);

        if (_UnlockedSprite != null)
            _SpriteRenderer.sprite = _UnlockedSprite;
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

            if (_LockedClip != null)
                _AudioSource.PlayOneShot (_LockedClip);
        }
    }
}
