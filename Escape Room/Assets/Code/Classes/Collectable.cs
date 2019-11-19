using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]
public class Collectable : MonoBehaviour
{
    [Tooltip ("The item the player will collect from this object.")]
    [SerializeField] private Item _Item = new Item ();

    private void Awake ()
    {
        GetComponent<Collider2D> ().isTrigger = true;
    }

    public Item Collect ()
    {
        this.gameObject.SetActive (false);
        return _Item;
    }

    public void OnMouseDown ()
    {
        Signals.Collect (this);
    }
}
