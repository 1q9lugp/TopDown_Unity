using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Start(){
        itemDictionary = FindFirstObjectByType<ItemDictionary>();

        /*
        for (int i = 0; i < slotCount; i++) {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();

            if (i < itemPrefabs.Length) { 
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                slot.currentItem = item;
            }
        } */ // useless?
    }

    public bool AddItem(GameObject itemPrefab) {
        foreach (Transform slotTransform in inventoryPanel.transform) {
            Slot slot = slotTransform.GetComponent<Slot>();

            if (slot != null &&  slot.currentItem == null) { 
                GameObject newItem =Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Inv is full"); //replace with a visual cue in game
        return false;
    }

    public List<InventorySaveData> GetInventoryItems() {
        List<InventorySaveData> invData = new List<InventorySaveData>();

        foreach (Transform slotTransform in inventoryPanel.transform) { 

            Slot slot = slotTransform.GetComponent<Slot>();

            if (slot.currentItem != null) {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slot.transform.GetSiblingIndex() });
            }
        }

        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData) {
        //clear inventory panel

        foreach (Transform child in inventoryPanel.transform) {
            Destroy(child.gameObject);
        }

        //create new slots

        for (int i = 0; i < slotCount; i++) {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        //fill items into slots

        foreach (InventorySaveData data in inventorySaveData) {

            if (data.slotIndex < slotCount) {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();

                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);

                if (itemPrefab != null) {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }

        }
    }

}
