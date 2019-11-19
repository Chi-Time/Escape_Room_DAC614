using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Keypad : MonoBehaviour
{
    [Tooltip ("The combination required to open")]
    [SerializeField] private string _Combination = null;

    public bool IsCombination (string combination)
    {
        if (_Combination == combination)
            return true;

        return false;
    }
}
