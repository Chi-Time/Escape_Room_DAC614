using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    private static int UIDCounter = 0;

    [Tooltip ("How many items can the player hold at once?")]
    [SerializeField] private int _InventorySize = 25;
    [Tooltip ("The various items the player currently has on them.")]
    [SerializeField] private List<Item> _Items = new List<Item> ();

    public static int GetUniqueID ()
    {
        UIDCounter++;
        return UIDCounter;
    }

    /// <summary>Faux constructor to set object up.</summary>
    public void Constructor ()
    {
        foreach (Item item in _Items)
            item.Constructor ();
    }

    /// <summary>Adds an item to the inventory if there is space left.</summary>
    /// <param name="item">The item to add into the inventory.</param>
    public void AddItem (Item item)
    {
        if (_Items.Count + 1 > _InventorySize)
            return;

        _Items.Add (item);
    }

    /// <summary>Removes an item from the inventory if there is any of them left.</summary>
    /// <param name="itemToRemove">The item to remove from the inventory.</param>
    public void RemoveItem (Item itemToRemove)
    {
        if (_Items.Count <= 0)
            return;

        foreach (Item item in _Items)
            if (item.UID == itemToRemove.UID)
                _Items.Remove (itemToRemove);
    }

    /// <summary>Returns an item by matching it's ID with one in the inventory.</summary>
    /// <param name="ID">The ID of the item in the database.</param>
    /// <returns></returns>
    public Item GetItem (int ID)
    {
        foreach (Item item in _Items)
        {
            if (item.ID == ID)
                return item;
        }

        return null;
    }

    public Item GetItem (string name)
    {
        foreach (Item item in _Items)
        {
            if (item.Name == name)
                return item;
        }

        return null;
    }

    public Item GetItem (Item newItem)
    {
        foreach (Item item in _Items)
            if (item.ID == newItem.ID)
                return item;

        return null;
    }

    public Item GetExactItem (Item newItem)
    {
        foreach (Item item in _Items)
            if (item.UID == newItem.UID)
                return item;

        return null;
    }

    public bool HasItem (Item newItem)
    {
        foreach (Item item in _Items)
            if (item.ID == newItem.ID)
                return true;

        return false;
    }

    public bool HasExactItem (Item newItem)
    {
        foreach (Item item in _Items)
            if (item.UID == newItem.UID)
                return true;

        return false;
    }

    /// <summary>Prints the contents of the player's inventory to the console.</summary>
    public void Print ()
    {
        foreach (Item item in _Items)
        {
            Debug.Log ($"Name: {item.Name} | ID: {item.ID}");
        }
    }
}

[Serializable]
public class Item
{
    public string Name { get { return _Name; } }
    public int ID { get { return _ID; } }
    public int UID { get { return _UID; } }
    public string Description { get { return _Description; } }
    public GameObject ItemPrefab { get { return _ItemPrefab; } }

    [Tooltip ("The name of the current item.")]
    [SerializeField] private string _Name = " ";
    [Tooltip ("The ID of the current item.")]
    [SerializeField] private int _ID = 0;
    [Tooltip ("The Unique ID of the current item.")]
    [SerializeField] private int _UID = 0;
    [Tooltip ("The description of the object when viewed.")]
    [SerializeField] private string _Description = "";
    [Tooltip ("The physical prefab of the item.")]
    [SerializeField] private GameObject _ItemPrefab = null;

    /// <summary>Faux constructor to set object up.</summary>
    public void Constructor ()
    {
        _UID = Inventory.GetUniqueID ();
    }

    public bool Is (Item newItem)
    {
        if (newItem.ID == this.ID)
            return true;

        return false;
    }
}
