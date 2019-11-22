using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Inventory Inventory { get { return _Inventory; } }
    public static GameState CurrentState { get { return _CurrentState; } }

    [Tooltip ("The current state that the game is currently in.")]
    [SerializeField] private static GameState _CurrentState = GameState.MainScreen;
    [Tooltip ("The inventory that the player can currently access.")]
    [SerializeField] private Inventory _Inventory = new Inventory ();

    [Space (.5f)]
    [Header ("Narrative")]

    [Tooltip ("The message to play when starting the level.")]
    [SerializeField] private TextAsset _StartMessage = null;
    [Tooltip ("The message to play when the level is finished.")]
    [SerializeField] private TextAsset _EndingMessage = null;

    private GameObject _Scene = null;
    private GameObject _Canvas = null;
    private GameObject _Subrooms = null;
    private GameObject _Containers = null;
    private SpriteRenderer _SpriteRenderer = null;

    private void Awake ()
    {
        CreateContainerHolder ();
        _Inventory.Constructor ();
        _Scene = GameObject.Find ("_Scene");
        _Subrooms = GameObject.Find ("Sub Rooms");
        _Canvas = GetComponentInChildren<Canvas> ().gameObject;
        _SpriteRenderer = GetComponent<SpriteRenderer> ();
        _SpriteRenderer.color = new Color (0, 0, 0, 0);
    }

    private void CreateContainerHolder ()
    {
        var containerHolder = new GameObject ("Containers Holder");
        _Containers = containerHolder;
    }

    private void Start ()
    {
        if (_StartMessage != null)
            Signals.PumpMessage (_StartMessage);
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
                    Signals.ChangeGameState (GameState.Subroom);
                break;
            case GameState.Paused:
                break;
            case GameState.Subroom:
                if (Input.GetMouseButtonDown (1))
                    Signals.ChangeGameState (GameState.MainScreen);
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
                _SpriteRenderer.color = new Color (0, 0, 0, 0);
                Time.timeScale = 1.0f;
                break;
            case GameState.Container:
                break;
            case GameState.Combination:
                break;
            case GameState.Paused:
                if (_EndingMessage != null)
                    Signals.PumpMessage (_EndingMessage);

                _SpriteRenderer.color = new Color (255, 255, 255, 1.0f);
                break;
            case GameState.Subroom:
                DisplaySubroom ();
                _SpriteRenderer.color = new Color (0, 0, 0, 0);
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

        foreach (Transform room in _Subrooms.transform)
            room.gameObject.SetActive (false);
    }

    private void DisplaySubroom ()
    {
        foreach (Transform container in _Containers.transform)
            container.gameObject.SetActive (false);

        foreach (Transform uiWindow in _Canvas.transform)
            uiWindow.gameObject.SetActive (false);
    }

    private void DisplayEndScreen ()
    {
        foreach (Transform container in _Containers.transform)
            container.gameObject.SetActive (false);

        foreach (Transform sceneGO in _Scene.transform)
            sceneGO.gameObject.SetActive (false);
    }
        
    private void OnDisable ()
    {
        Signals.OnCollected -= OnCollected;
        Signals.OnItemRequired -= OnItemRequired;
        Signals.OnGameStateChanged -= OnGameStateChanged;
    }
}
