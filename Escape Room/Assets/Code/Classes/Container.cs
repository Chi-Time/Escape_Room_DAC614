using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
public class Container : MonoBehaviour
{
    [Tooltip ("The view to display with the contents of the container.")]
    [SerializeField] protected GameObject _ContainerContents = null;
    [Tooltip ("The various audio clips that the container can play.")]
    [SerializeField] protected SFXClips _SFXClips = new SFXClips ();

    protected AudioSource _AudioSource = null;
    protected SpriteRenderer _SpriteRenderer = null;

    protected virtual void Awake ()
    {
        _AudioSource = GetComponent<AudioSource> ();
        _SpriteRenderer = GetComponent<SpriteRenderer> ();
        _SFXClips.Constructor (_AudioSource);

        if (_ContainerContents != null)
        {
            _ContainerContents = Instantiate (_ContainerContents);
            _ContainerContents.SetActive (false);
        }
    }

    protected virtual void Start ()
    {
        if (_ContainerContents != null)
        {
            var containerHolder = GameObject.Find ("Containers Holder");
            _ContainerContents.transform.SetParent (containerHolder.transform, false);
        }
    }

    protected virtual void OnMouseDown ()
    {
        if (GameManager.CurrentState != GameState.Subroom)
            return;

        Clicked ();
    }

    protected virtual void Clicked ()
    {
        if (_ContainerContents != null)
        {
            _SFXClips.PlayClip (ClipTypes.Opened);

            _ContainerContents.SetActive (true);
            Signals.ChangeGameState (GameState.Container);
        }
    }
}
