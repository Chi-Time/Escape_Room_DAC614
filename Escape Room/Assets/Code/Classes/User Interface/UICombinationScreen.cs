using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class UICombinationScreen : MonoBehaviour
{
    [Tooltip ("The maximum length of the combination.")]
    [SerializeField] private int _MaxCombinationLength = 0;

    private string _Combination = "";

    public void PressKey (string symbol)
    {
        if (_Combination.Length + 1 > _MaxCombinationLength)
            return;

        _Combination += symbol;
    }

    public void EnterCombination ()
    {
        //TODO: Enter combination check.
    }
}
