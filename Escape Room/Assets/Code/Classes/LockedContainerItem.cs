using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
class LockedContainerItem : Container, IRequirable
{
    public Item RequiredItem { get { return _RequiredItem; } }

    [SerializeField]
    [Tooltip ("Is the item currently locked and requires an item?")]
    private bool _IsLocked = true;
    [SerializeField]
    [Tooltip ("The item required to open or use this.")]
    private Item _RequiredItem = null;
    [SerializeField]
    [Tooltip ("The clip to play when interacting with the locked object.")]
    private AudioClip _LockedClip = null;
    [SerializeField]
    [Tooltip ("The clip to play when unlocking the object.")]
    private AudioClip _UnlockedClip = null;
    [SerializeField]
    [Tooltip ("The sprite to switch to when the container has been unlocked.\nIf no sprite is provided then the image won't change.")]
    private Sprite _UnlockedSprite = null;
    [SerializeField]
    [Tooltip ("The message to print when the object is locked.\nIf no message is provided it won't say anything.")]
    private TextAsset _LockedMessage = null;
    [SerializeField]
    [Tooltip ("The message to print when the object is unlocked.\nIf no message is provided it won't say anything.")]
    private TextAsset _UnlockedMessage = null;

    public void Give (Item item)
    {
        if (item != null)
        {
            if (item.Is (_RequiredItem))
            {
                Unlock ();
            }
        }
        else
        {
            Signals.PumpMessage (_LockedMessage);
            
            if (_LockedClip != null)
                _AudioSource.PlayOneShot (_LockedClip);
        }
    }

    protected override void Awake ()
    {
        base.Awake ();

        _RequiredItem.Constructor ();

    }

    private void Unlock ()
    {
        _IsLocked = false;
        Signals.PumpMessage (_UnlockedMessage);

        if (_UnlockedClip != null)
            _AudioSource.PlayOneShot (_UnlockedClip);

        if (_UnlockedSprite != null)
            _SpriteRenderer.sprite = _UnlockedSprite;
    }

    protected override void OnMouseDown ()
    {
        if (_IsLocked)
        {
            Signals.Require (this);
        }
        else
        {
            Clicked ();
        }
    }
}
