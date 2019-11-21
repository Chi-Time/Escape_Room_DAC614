using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class UICombinationScreen : MonoBehaviour
{
    [Tooltip ("The label to use for the combination.")]
    [SerializeField] private Text _CombinationLabel = null;
    [Tooltip ("The text to display when the user encounters the combination.")]
    [SerializeField] private string _StartText = "Enter Code";
    [Tooltip ("The text to display when the user fails the combination.")]
    [SerializeField] private string _ErrorText = "Wrong Code";
    [Tooltip ("The audio clip to play when the code is wrong.")]
    [SerializeField] private AudioClip _ErrorClip = null;
    [Tooltip ("The audio clip to play when the code is correct.")]
    [SerializeField] private AudioClip _ButtonClip = null;
    [Tooltip ("The audio clip to play when a button is pressed.")]
    [SerializeField] private AudioClip _ConfirmationClip = null;

    private string _Combination = "";
    private int _MaxCombinationLength = 0;
    private ICodeLockable _LockedObject = null;
    private AudioSource _AudioSource = null;

    public void ResetKeypad ()
    {
        ClearCombination ();

        if (_ErrorClip != null)
            _AudioSource.PlayOneShot (_ErrorClip);
    }

    public void DisplayKeypad (string correctCombination, ICodeLockable lockedObject)
    {
        this.gameObject.SetActive (true);

        ClearCombination ();
        _LockedObject = lockedObject;
        _CombinationLabel.text = "Enter Code";
        _MaxCombinationLength = correctCombination.Length;
    }

    public void PressKey (string symbol)
    {
        UpdateCombination (ref symbol);
        UpdateLabel ();

        if (_ButtonClip != null)
            _AudioSource.PlayOneShot (_ButtonClip);
    }

    private void Awake ()
    {
        _AudioSource = GetComponent<AudioSource> ();
    }

    private void UpdateCombination (ref string symbol)
    {
        if (_Combination.Length + 1 > _MaxCombinationLength)
            return;

        _Combination += symbol;
    }

    private void UpdateLabel ()
    {
        if (_CombinationLabel.text == _StartText || _CombinationLabel.text == _ErrorText)
            _CombinationLabel.text = "";
        
        _CombinationLabel.text = _Combination;
    }

    public void EnterCombination ()
    {
        if (_LockedObject.Unlock (_Combination) == false)
        {
            ResetKeypad ();
            _CombinationLabel.text = _ErrorText;
        }
        else
        {
            if (_ConfirmationClip != null)
            {
                _AudioSource.PlayOneShot (_ConfirmationClip);
                Invoke ("Close", _ConfirmationClip.length);
            }
        }
    }

    private void Close ()
    {
        this.gameObject.SetActive (false);
        Signals.ChangeGameState (GameState.MainScreen);
    }

    private void ClearCombination ()
    {
        _Combination = "";
        _CombinationLabel.text = "";
    }
}
