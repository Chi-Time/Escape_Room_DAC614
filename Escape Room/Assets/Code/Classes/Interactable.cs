using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LockedInteractable : MonoBehaviour
{
    [Tooltip ("Is the item currently locked and requires an item?")]
    [SerializeField] private bool _IsLocked = true;
    [Tooltip ("The item required to open or use this.")]
    [SerializeField] private Item _RequiredItem = null;

    public void Unlock (Item item)
    {
        if (item.ID == _RequiredItem.ID)
        {
            _IsLocked = false;
        }
    }

    public void OnMouseDown ()
    {
        
    }
}
