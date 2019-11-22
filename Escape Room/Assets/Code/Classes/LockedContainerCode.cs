using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
class LockedContainerCode : Container, ICodeLockable
{
    public bool IsUnlocked { get; set; }

    [Tooltip ("The sprite to switch to when the container has been unlocked.\nIf no sprite is provided then the image won't change.")]
    [SerializeField] private Sprite _UnlockedSprite = null;
    [Tooltip ("The message to print when the object is locked.\nIf no message is provided it won't say anything.")]
    [SerializeField] private TextAsset _LockedMessage = null;
    [Tooltip ("The message to print when the object is unlocked.\nIf no message is provided it won't say anything.")]
    [SerializeField] private TextAsset _UnlockedMessage = null;
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
        if (GameManager.CurrentState != GameState.Subroom)
            return;

        if (IsUnlocked)
        {
            Clicked ();
        }
        else
        {
            //DisplayLocked ();

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
            DisplayUnlocked ();

            return true;
        }

        return false;
    }

    private void DisplayUnlocked ()
    {
        Signals.Unlock (this);
        //Signals.PumpMessage (_UnlockedMessage);

        _SFXClips.PlayClip (ClipTypes.Unlocked);

        if (_UnlockedSprite != null)
            _SpriteRenderer.sprite = _UnlockedSprite;
    }

    private void DisplayLocked ()
    {
        //Signals.PumpMessage (_LockedMessage);

        _SFXClips.PlayClip (ClipTypes.Locked);
    }
}
