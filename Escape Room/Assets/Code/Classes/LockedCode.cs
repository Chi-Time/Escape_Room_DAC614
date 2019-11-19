using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LockedCode : MonoBehaviour, ICodeLockable
{
    public bool IsUnlocked { get; set; }

    [Tooltip ("The combination required to open")]
    [SerializeField] private string _RequiredCombination = null;
    [Tooltip ("The view to display with the contents of the container.")]
    [SerializeField] private GameObject _ContainerContents = null;
    [Tooltip ("The UI to display for entering the combination.")]
    [SerializeField] private UICombinationScreen _CombinationUI = null;

    private void Awake ()
    {
        IsUnlocked = true;
        
        if (_ContainerContents != null)
        {
            _ContainerContents = Instantiate (_ContainerContents, new Vector3 (0, 0, -1f), Quaternion.identity);
            _ContainerContents.SetActive (false);
        }

        if (_CombinationUI != null)
        {
            _CombinationUI = Instantiate (_CombinationUI.gameObject).GetComponent<UICombinationScreen> ();
            _CombinationUI.gameObject.SetActive (false);
        }
    }

    private void Start ()
    {
        if (_ContainerContents != null)
        {
            var containerHolder = GameObject.Find ("Containers Holder");
            _ContainerContents.transform.SetParent (containerHolder.transform);
        }

        if (_CombinationUI != null)
        {
            var canvasHolder = FindObjectOfType<Canvas> ().transform;
            _ContainerContents.transform.SetParent (canvasHolder);
        }
    }

    public void OnMouseDown ()
    {
        if (IsUnlocked)
        {
            if (_ContainerContents != null)
            {
                _ContainerContents.SetActive (true);
                Signals.ChangeGameState (GameState.Container);
            }
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
