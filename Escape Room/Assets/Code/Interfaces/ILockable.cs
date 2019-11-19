using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICodeLockable
{
    bool IsUnlocked { get; set; }

    bool Unlock (string inputCombination);
}
