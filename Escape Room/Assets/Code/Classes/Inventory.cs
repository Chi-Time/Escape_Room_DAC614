using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    [Tooltip ("The various items the player currently has on them.")]
    [SerializeField] private List<Item> Items = new List<Item> ();

    public void AddInventoryItem (Item item)
    {
        Items.Add (item);
    }

    public Item GetInventoryItem (int ID)
    {
        foreach (Item item in Items)
        {
            if (item.ID == ID)
                return item;
        }

        return null;
    }

    public Item GetInventoryItem (string name)
    {
        foreach (Item item in Items)
        {
            if (item.Name == name)
                return item;
        }

        return null;
    }

    public void Print ()
    {
        foreach (Item item in Items)
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
    public string Description { get { return _Description; } }
    public GameObject ItemPrefab { get { return _ItemPrefab; } }

    [Tooltip ("The name of the current item.")]
    [SerializeField] private string _Name = " ";
    [Tooltip ("The ID of the current item.")]
    [SerializeField] private int _ID = 0;
    [Tooltip ("The description of the object when viewed.")]
    [SerializeField] private string _Description = "";
    [Tooltip ("The physical prefab of the item.")]
    [SerializeField] private GameObject _ItemPrefab = null;
}
