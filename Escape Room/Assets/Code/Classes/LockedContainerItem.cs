using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LockedContainerItem : Container, IRequirable
{
    public Item RequiredItem { get { return _RequiredItem; } }

    [Tooltip ("Is the item currently locked and requires an item?")]
    [SerializeField] private bool _IsLocked = true;
    [Tooltip ("The item required to open or use this.")]
    [SerializeField] private Item _RequiredItem = null;

    protected override void Awake ()
    {
        base.Awake ();

        _RequiredItem.Constructor ();
    }

    public void Unlock (Item item)
    {
        if (item.ID == _RequiredItem.ID)
        {
            _IsLocked = false;
        }
    }

    public void Give (Item item)
    {
        if (item != null)
        {
            if (item.Is (_RequiredItem))
            {
                _IsLocked = false;
                //TODO: Add an unlocked message / SFX.
            }
        }
        else
        {
            //TODO: Add in locked message.
        }
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

            print ("I'm unlocked");
        }
    }
}
