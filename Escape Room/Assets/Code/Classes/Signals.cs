using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class Signals
{
    public static event Action<Collectable> OnCollected;
    public static void Collect (Collectable collectable) { OnCollected?.Invoke (collectable); }

    public static event Action<Inspectable> OnInspected;
    public static void Inspect (Inspectable inspectable) { OnInspected?.Invoke (inspectable); }

    public static event Action<IRequirable> OnItemRequired;
    public static void Require (IRequirable requirer) { OnItemRequired?.Invoke (requirer); }

    public static event Action<GameState> OnGameStateChanged;
    public static void ChangeGameState (GameState newState) { OnGameStateChanged?.Invoke (newState); }




    public static event Action<IClickable> OnThingClicked;
    public static void Thing (IClickable thing) { OnThingClicked?.Invoke (thing); }
 
    

    public static event Action<string> OnCombinationEntered;
    public static void EnterCombination (string combination) { OnCombinationEntered?.Invoke (combination); }


}
