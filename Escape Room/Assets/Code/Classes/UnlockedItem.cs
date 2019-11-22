using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class UnlockedItem : MonoBehaviour
{
    [Tooltip ("The container which needs to be unlocked before this object will activate itself.")]
    [SerializeField] private Container _Container = null;
    [Tooltip ("Should the item be displayed after being collected? Or hidden?")]
    [SerializeField] private bool _ShouldDisplay = false;

    private void Awake ()
    {
        if (_ShouldDisplay == false)
            this.gameObject.SetActive (false);

        Signals.OnContainerUnlocked += OnContainerUnlocked;
    }

    private void OnContainerUnlocked (Container container)
    {
        if (container != _Container)
            return;

        if (_ShouldDisplay == false)
            this.gameObject.SetActive (true);
        else
            this.gameObject.SetActive (false);
    }

    private void OnDestroy ()
    {
        Signals.OnContainerUnlocked -= OnContainerUnlocked;
    }
}
