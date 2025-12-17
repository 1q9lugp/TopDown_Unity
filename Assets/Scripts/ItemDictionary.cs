using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{

    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;

    private void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();

        //auto Incrementation
        for (int i = 0; i < itemPrefabs.Count; i++) {
            if (itemPrefabs[i] != null) {
                itemPrefabs[i].ID = i +1;
            }
        }

        foreach (Item item in itemPrefabs) {
            itemDictionary[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int ItemID) {
        itemDictionary.TryGetValue(ItemID, out GameObject prefab);
        if (prefab == null) {
            Debug.LogWarning($"Item with ID {ItemID} not found.");
        }
        return prefab;
    }
}
