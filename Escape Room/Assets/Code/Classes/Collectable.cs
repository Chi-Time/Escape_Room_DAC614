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
    [Tooltip ("The message to play when collecting the item.")]
    [SerializeField] private TextAsset _CollectedMessage = null;
    [Tooltip ("The various audio clips that the container can play.")]
    [SerializeField] protected SFXClips _SFXClips = new SFXClips ();

    private AudioSource _AudioSource = null;

    /// <summary>Collects and returns the item. Disables the object in the world.</summary>
    /// <returns>The item that this object contains.</returns>
    public Item Collect ()
    {
        GetComponent<Collider2D> ().enabled = false;
        GetComponent<SpriteRenderer> ().enabled = false;

        _SFXClips.PlayClip (ClipTypes.Collected);

        Signals.PumpMessage (_CollectedMessage);

        return _Item;
    }

    private void Awake ()
    {
        _Item.Constructor ();
        _AudioSource = GetComponent<AudioSource> ();
        GetComponent<Collider2D> ().isTrigger = true;

        _SFXClips.Constructor (_AudioSource);
    }

    private void OnMouseDown ()
    {
        Signals.Collect (this);
    }
}
