using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Clickable : MonoBehaviour
{
    private void OnMouseDown ()
    {
        SendMessage ("Clicked");
    }
}
