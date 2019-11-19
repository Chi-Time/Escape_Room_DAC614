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
    [Tooltip ("The UI screen to enter the keypad combination from.")]
    [SerializeField] private UICombinationScreen _KeyPadUIPrefab = null;

    private void Awake ()
    {
        _KeyPadUIPrefab = Instantiate (_KeyPadUIPrefab.gameObject).GetComponent<UICombinationScreen> ();
        _KeyPadUIPrefab.gameObject.SetActive (false);
    }

    private void OnMouseDown ()
    {
        if (_KeyPadUIPrefab != null)
        {
            _KeyPadUIPrefab.gameObject.SetActive (true);
        }
    }

    public bool IsCombination (string combination)
    {
        if (_Combination == combination)
            return true;

        return false;
    }
}
