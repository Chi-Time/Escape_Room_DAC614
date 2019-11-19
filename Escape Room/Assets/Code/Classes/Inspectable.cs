using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Inspectable : MonoBehaviour
{
    //TODO: Make it so that a UI window is displayed upon clicking an object.

    public void Inspect ()
    {

    }

    public void OnMouseDown ()
    {
        Signals.Inspect (this);
    }
}
