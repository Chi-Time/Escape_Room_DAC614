using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SFXClips
{
    [Tooltip ("The various audio clips this object contains.")]
    [SerializeField] private List<SFXClip> _Clips = new List<SFXClip> ();

    private AudioSource _AudioSource = null;

    public void Constructor (AudioSource audioSource)
    {
        _AudioSource = audioSource;
    }

    /// <summary>Plays the audio clip for the given action if it exists.</summary>
    /// <param name="clipType">The clip action type to play.</param>
    public void PlayClip (ClipTypes clipType)
    {
        foreach (SFXClip clip in _Clips)
        {
            if (clip.ClipType == clipType && clip.AudioClip != null)
            {
                if (clip.ShouldLoop)
                {
                    _AudioSource.loop = true;
                    _AudioSource.clip = clip.AudioClip;
                    _AudioSource.Play ();
                }
                else
                {
                    _AudioSource.PlayOneShot (clip.AudioClip);
                }
            }
        }
    }
}

[Serializable]
public class SFXClip
{
    public string Name { get { return _Name; } }
    public ClipTypes ClipType { get { return _ClipType; } }
    public bool ShouldLoop { get { return _ShouldLoop; } }
    public AudioClip AudioClip { get { return _AudioClip; } }

    [Tooltip ("What is the name of this clip?")]
    [SerializeField] private string _Name = "Locked";
    [Tooltip ("What type of clip is it? (When should it occur?)")]
    [SerializeField] private ClipTypes _ClipType = ClipTypes.Opened;
    [Tooltip ("Should this clip be played as a loopable piece?")]
    [SerializeField] private bool _ShouldLoop = false;
    [Tooltip ("The audio clip to play when this type is called.")]
    [SerializeField] private AudioClip _AudioClip = null;
}
