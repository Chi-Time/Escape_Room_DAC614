using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LockedContainerCode : Container, ICodeLockable
{
    public bool IsUnlocked { get; set; }

    [Tooltip ("The combination required to open")]
    [SerializeField] private string _RequiredCombination = null;
    [Tooltip ("The UI to display for entering the combination.")]
    [SerializeField] private UICombinationScreen _CombinationUI = null;

    protected override void Awake ()
    {
        base.Awake ();

        IsUnlocked = false;

        if (_CombinationUI != null)
        {
            _CombinationUI = Instantiate (_CombinationUI.gameObject).GetComponent<UICombinationScreen> ();
            _CombinationUI.gameObject.SetActive (false);
        }
    }

    protected override void Start ()
    {
        base.Start ();

        if (_CombinationUI != null)
        {
            var canvasHolder = FindObjectOfType<Canvas> ().GetComponent<RectTransform> ();
            var _UIRect = _CombinationUI.GetComponent<RectTransform> ();
            _UIRect.SetParent (canvasHolder, false);
        }
    }

    protected override void OnMouseDown ()
    {
        if (IsUnlocked)
        {
            Clicked ();
        }
        else
        {
            if (_CombinationUI != null)
            {
                _CombinationUI.DisplayKeypad (_RequiredCombination, this);
                Signals.ChangeGameState (GameState.Combination);
            }
        }
    }

    /// <summary>Attempts to unlock the object and returns true or false whether it's successful.</summary>
    /// <param name="inputCombination">The combination to attempt to unlock the object.</param>
    public bool Unlock (string inputCombination)
    {
        if (_RequiredCombination == inputCombination)
        {
            IsUnlocked = true;
            //TODO: Implement sprite switch with fade animmation.
            return true;
        }

        return false;
    }
}
