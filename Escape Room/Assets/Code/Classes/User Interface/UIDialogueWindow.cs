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

    private void Awake ()
    {
        Signals.OnMessagePumped += OnMessagePumped;
        this.gameObject.SetActive (false);
    }

    public void OnMessagePumped (TextAsset message)
    {
        // Check to see if there is a message to print first. If not, just exit.
        if (message == null)
            return;

        this.gameObject.SetActive (true);
        StartCoroutine (DisplayMessage (message.text));
    }

    private IEnumerator DisplayMessage (string message)
    {
        foreach (char character in message)
        {
            _DialogueLabel.text += character;
            yield return new WaitForSeconds (_PrintSpeed);
        }
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
        StopAllCoroutines ();
        _DialogueLabel.text = "";
    }
}
