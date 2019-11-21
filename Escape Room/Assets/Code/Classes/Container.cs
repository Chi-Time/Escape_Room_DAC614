using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
public class Container : MonoBehaviour
{
    [Tooltip ("The clip to play when opening the object.")]
    [SerializeField] protected AudioClip _OpenClip = null;
    [Tooltip ("The view to display with the contents of the container.")]
    [SerializeField] protected GameObject _ContainerContents = null;

    protected AudioSource _AudioSource = null;
    protected SpriteRenderer _SpriteRenderer = null;

    protected virtual void Awake ()
    {
        _AudioSource = GetComponent<AudioSource> ();
        _SpriteRenderer = GetComponent<SpriteRenderer> ();

        if (_ContainerContents != null)
        {
            _ContainerContents = Instantiate (_ContainerContents, new Vector3 (0, 0, -1f), Quaternion.identity);
            _ContainerContents.SetActive (false);
        }
    }

    protected virtual void Start ()
    {
        if (_ContainerContents != null)
        {
            var containerHolder = GameObject.Find ("Containers Holder");
            _ContainerContents.transform.SetParent (containerHolder.transform);
        }
    }

    protected virtual void OnMouseDown ()
    {
        Clicked ();
    }

    protected virtual void Clicked ()
    {
        if (_ContainerContents != null)
        {
            if (_OpenClip != null)
                _AudioSource.PlayOneShot (_OpenClip);

            _ContainerContents.SetActive (true);
            Signals.ChangeGameState (GameState.Container);
        }
    }
}
