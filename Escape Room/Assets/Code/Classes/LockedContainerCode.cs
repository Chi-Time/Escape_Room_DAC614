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
    [Tooltip ("The clip to play when interacting with the locked object.")]
    [SerializeField] private AudioClip _LockedClip = null;
    [Tooltip ("The clip to play when unlocking the object.")]
    [SerializeField] private AudioClip _UnlockedClip = null;
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
            DisplayUnlocked ();

            return true;
        }

        DisplayLocked ();

        return false;
    }

    private void DisplayUnlocked ()
    {
        Signals.PumpMessage (_UnlockedMessage);

        if (_UnlockedClip != null)
            _AudioSource.PlayOneShot (_UnlockedClip);

        if (_UnlockedSprite != null)
            _SpriteRenderer.sprite = _UnlockedSprite;
    }

    private void DisplayLocked ()
    {
        Signals.PumpMessage (_LockedMessage);

        if (_LockedClip != null)
            _AudioSource.PlayOneShot (_LockedClip);
    }
}
