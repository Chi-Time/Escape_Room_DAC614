using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof (AudioSource))]
class RoomTransition : MonoBehaviour
{
    [Tooltip ("The room to transition to.")]
    [SerializeField] private GameObject _Room = null;
    [SerializeField] private SFXClips _Clips = new SFXClips ();

    private AudioSource _AudioSource = null;

    private void Awake ()
    {
        _AudioSource = GetComponent<AudioSource> ();
    }

    private void OnMouseUp ()
    {
        _Room.SetActive (true);
        Signals.ChangeGameState (GameState.Subroom);
    }
} 
