using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip ("The current state that the game is currently in.")]
    [SerializeField] private GameState _CurrentState = GameState.MainScreen;
    [Tooltip ("The inventory that the player can currently access.")]
    [SerializeField] private Inventory Inventory = new Inventory ();

    private GameObject _Containers = null;

    private void Awake ()
    {
        CreateContainerHolder ();
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
                if (Input.GetMouseButtonDown (1))
                    Signals.ChangeGameState (GameState.MainScreen);
                break;
            case GameState.Combination:
                break;
            case GameState.Paused:
                break;
        }
    }

    private void OnEnable ()
    {
        Signals.OnCollected += OnCollected;
        Signals.OnInspected += OnInspected;
        Signals.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnCollected (Collectable collectable)
    {
        Item item = collectable.Collect ();
        Inventory.AddInventoryItem (item);
    }

    private void OnInspected (Inspectable inspectable)
    {
        inspectable.Inspect ();
    }

    private void OnGameStateChanged (GameState gameState)
    {
        _CurrentState = gameState;

        switch (_CurrentState)
        {
            case GameState.MainScreen:
                foreach (Transform container in _Containers.transform)
                    container.gameObject.SetActive (false);
                break;
            case GameState.Container:
                break;
            case GameState.Combination:
                break;
            case GameState.Paused:
                break;
        }
    }
        
    private void OnDisable ()
    {
        Signals.OnCollected -= OnCollected;
        Signals.OnInspected -= OnInspected;
    }
}
