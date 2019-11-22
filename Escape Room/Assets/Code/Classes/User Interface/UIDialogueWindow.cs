using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class UIDialogueWindow : MonoBehaviour
{
    [Tooltip ("How fast should the text be printed to the screen?")]
    [SerializeField] private float _PrintSpeed = 0.0f;
    [Tooltip ("The text to use for all dialogue in the game.")]
    [SerializeField] private Text _DialogueLabel = null;

    private bool _Printing = false;

    private void Awake ()
    {
        Signals.OnMessagePumped += OnMessagePumped;
        this.gameObject.SetActive (false);
    }

    private void OnEnable ()
    {
        _Printing = false;
    }

    public void OnMessagePumped (TextAsset message)
    {
        // Check to see if there is a message to print first. If not, just exit.
        if (message == null || _Printing)
            return;

        this.gameObject.SetActive (true);
        StartCoroutine (DisplayMessage (message.text));
    }

    private IEnumerator DisplayMessage (string message)
    {
        _Printing = true;

        foreach (char character in message)
        {
            _DialogueLabel.text += character;
            yield return new WaitForSeconds (_PrintSpeed);
        }

        _Printing = false;
    }

    private void OnDestroy ()
    {
        Signals.OnMessagePumped -= OnMessagePumped;
    }

    public void Close ()
    {
        this.gameObject.SetActive (false);
    }

    private void OnDisable ()
    {
        _Printing = false;
        StopAllCoroutines ();
        _DialogueLabel.text = "";
    }
}
