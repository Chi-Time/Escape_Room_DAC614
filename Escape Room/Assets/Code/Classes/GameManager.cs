using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip ("The inventory that the player can currently access.")]
    [SerializeField] private Inventory Inventory = new Inventory ();

    private void OnEnable ()
    {
        Signals.OnCollected += OnCollected;
        Signals.OnInspected += OnInspected;
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

    private void OnDisable ()
    {
        Signals.OnCollected -= OnCollected;
        Signals.OnInspected -= OnInspected;
    }
}
