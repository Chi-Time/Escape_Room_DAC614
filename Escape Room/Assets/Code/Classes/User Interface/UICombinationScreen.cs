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

    private string _Combination = "";
    private int _MaxCombinationLength = 0;
    private ICodeLockable _LockedObject = null;

    public void ClearCombination ()
    {
        _Combination = "";
        _CombinationLabel.text = "";
    }

    public void DisplayKeypad (string correctCombination, ICodeLockable lockedObject)
    {
        ClearCombination ();
        _LockedObject = lockedObject;
        _CombinationLabel.text = "Enter Code";
        _MaxCombinationLength = correctCombination.Length;

        this.gameObject.SetActive (true);
    }

    public void PressKey (string symbol)
    {
        UpdateCombination (ref symbol);
        UpdateLabel ();
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
        }
        else
        {
            this.gameObject.SetActive (false);
            Signals.ChangeGameState (GameState.MainScreen);
        }
    }

    private void ResetKeypad ()
    {
        ClearCombination ();
        _CombinationLabel.text = _ErrorText;
        //TODO: Play sound that combination failed.
    }
}
