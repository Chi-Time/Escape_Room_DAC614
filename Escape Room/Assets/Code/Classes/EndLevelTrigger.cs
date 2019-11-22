using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class EndLevelTrigger : MonoBehaviour
{
    private void OnEnable ()
    {
        Signals.ChangeGameState (GameState.Paused);
    }

    private void OnDisable ()
    {
        Signals.ChangeGameState (GameState.Subroom);
    }
}
