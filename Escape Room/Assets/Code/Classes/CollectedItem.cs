using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CollectedItem : MonoBehaviour
{
    [Tooltip ("The item which needs to be found before this object will activate itself.")]
    [SerializeField] private Item _CurrentItem = null;

    private void Awake ()
    {
        this.gameObject.SetActive (false);

        Signals.OnCollected += OnCollected;
    }

    private void OnCollected (Collectable collectable)
    {
        var item = collectable.Collect ();

        if (item.Is (_CurrentItem))
            this.gameObject.SetActive (false);
    }

    private void OnDestroy ()
    {
        Signals.OnCollected -= OnCollected;
    }
}
