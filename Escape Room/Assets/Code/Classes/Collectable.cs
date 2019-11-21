using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
public class Collectable : MonoBehaviour
{
    [Tooltip ("The item the player will collect from this object.")]
    [SerializeField] private Item _Item = new Item ();
    [Tooltip ("The audio clip to play upon being collected by the player.")]
    [SerializeField] private AudioClip _CollectedClip = null;
    [Tooltip ("The message to play when collecting the item.")]
    [SerializeField] private TextAsset _CollectedMessage = null;

    private AudioSource _AudioSource = null;

    /// <summary>Collects and returns the item. Disables the object in the world.</summary>
    /// <returns>The item that this object contains.</returns>
    public Item Collect ()
    {
        GetComponent<Collider2D> ().enabled = false;
        GetComponent<SpriteRenderer> ().enabled = false;

        if (_CollectedClip != null)
            _AudioSource.PlayOneShot (_CollectedClip);

        Signals.PumpMessage (_CollectedMessage);

        return _Item;
    }

    private void Awake ()
    {
        _Item.Constructor ();
        _AudioSource = GetComponent<AudioSource> ();
        GetComponent<Collider2D> ().isTrigger = true;
    }

    private void OnMouseDown ()
    {
        Signals.Collect (this);
    }
}
