using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class UICombinationScreen : MonoBehaviour
{
    private string _Combination = "";
    private int _MaxCombinationLength = 0;
    private ICodeLockable _LockedObject = null;

    private void ResetCombination ()
    {
        _Combination = "";
    }

    public void DisplayKeypad (string correctCombination, ICodeLockable lockedObject)
    {
        _LockedObject = lockedObject;
        _MaxCombinationLength = correctCombination.Length;
    }

    public void PressKey (string symbol)
    {
        if (_Combination.Length + 1 > _MaxCombinationLength)
            return;

        _Combination += symbol;
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
        _Combination = "";
        //TODO: Display label to user that combination failed.
        //TODO: Play sound that combination failed.
    }
}
