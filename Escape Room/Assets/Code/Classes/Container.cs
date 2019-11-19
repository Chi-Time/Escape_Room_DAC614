using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Container : MonoBehaviour
{
    [Tooltip ("The view to display with the contents of the container.")]
    [SerializeField] protected GameObject _ContainerContents = null;

    protected virtual void Awake ()
    {
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
            _ContainerContents.SetActive (true);
            Signals.ChangeGameState (GameState.Container);
        }
    }
}
