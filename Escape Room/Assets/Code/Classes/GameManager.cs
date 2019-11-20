using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Inventory Inventory { get { return _Inventory; } }
    public GameState CurrentState { get { return _CurrentState; } }

    [Tooltip ("The current state that the game is currently in.")]
    [SerializeField] private GameState _CurrentState = GameState.MainScreen;
    [Tooltip ("The inventory that the player can currently access.")]
    [SerializeField] private Inventory _Inventory = new Inventory ();

    private GameObject _Canvas = null;
    private GameObject _Containers = null;

    private void Awake ()
    {
        CreateContainerHolder ();
        _Inventory.Constructor ();
        _Canvas = GetComponentInChildren<Canvas> ().gameObject;
    }

    private void CreateContainerHolder ()
    {
        var containerHolder = new GameObject ("Containers Holder");
        _Containers = containerHolder;
    }

    private void Update ()
    {
        switch (_CurrentState)
        {
            case GameState.MainScreen:
                break;
            case GameState.Container:
            case GameState.Combination:
                if (Input.GetMouseButtonDown (1))
                    Signals.ChangeGameState (GameState.MainScreen);
                break;
            case GameState.Paused:
                break;
        }
    }

    private void OnEnable ()
    {
        Signals.OnCollected += OnCollected;
        Signals.OnItemRequired += OnItemRequired;
        Signals.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnCollected (Collectable collectable)
    {
        Item item = collectable.Collect ();
        _Inventory.AddItem (item);
    }

    private void OnItemRequired (IRequirable requirer)
    {
        var item = Inventory.GetItem (requirer.RequiredItem);

        requirer.Give (item);
    }

    private void OnGameStateChanged (GameState gameState)
    {
        _CurrentState = gameState;

        switch (_CurrentState)
        {
            case GameState.MainScreen:
                DisplayMainScreen ();
                break;
            case GameState.Container:
                break;
            case GameState.Combination:
                break;
            case GameState.Paused:
                break;
        }
    }

    /// <summary>Loops through and de-activates all overlayed containers and screens.</summary>
    private void DisplayMainScreen ()
    {
        foreach (Transform container in _Containers.transform)
            container.gameObject.SetActive (false);

        foreach (Transform uiWindow in _Canvas.transform)
            uiWindow.gameObject.SetActive (false);
    }
        
    private void OnDisable ()
    {
        Signals.OnCollected -= OnCollected;
        Signals.OnItemRequired -= OnItemRequired;
        Signals.OnGameStateChanged -= OnGameStateChanged;
    }
}
