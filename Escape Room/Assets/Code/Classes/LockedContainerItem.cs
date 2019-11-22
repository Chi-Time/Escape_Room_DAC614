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

            _SFXClips.PlayClip (ClipTypes.Locked);
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
        Signals.Unlock (this);
        Signals.PumpMessage (_UnlockedMessage);
        _SFXClips.PlayClip (ClipTypes.Unlocked);

        if (_UnlockedSprite != null)
            _SpriteRenderer.sprite = _UnlockedSprite;
    }

    protected override void OnMouseDown ()
    {
        if (GameManager.CurrentState != GameState.Subroom)
            return;

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
