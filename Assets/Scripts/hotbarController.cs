using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class hotbarController : MonoBehaviour
{

    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 8; //1-8 on the keyboard

    private ItemDictionary itemDictionary;

    private Key[] hotbarKeys;

    private void Awake() {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();

        hotbarKeys = new Key[slotCount];

        for (int i = 0; i < slotCount; i++) {
            hotbarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slotCount; i++) {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame) {
                useItemInSlot(i);
            }
        }
    }

    void useItemInSlot(int index) {
        Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if (slot.currentItem != null) {
            Item item = slot.currentItem.GetComponent<Item>();
            item.useItem();
        }
    }


    public List<InventorySaveData> GetHotbarItems() {
        List<InventorySaveData> hotbarData = new List<InventorySaveData>();

        foreach (Transform slotTransform in hotbarPanel.transform) {

            Slot slot = slotTransform.GetComponent<Slot>();

            if (slot.currentItem != null) {
                Item item = slot.currentItem.GetComponent<Item>();
                hotbarData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slot.transform.GetSiblingIndex() });
            }
        }

        return hotbarData;
    }

    public void SetHotbarItems(List<InventorySaveData> hotbarSaveData) {
        //clear inventory panel

        foreach (Transform child in hotbarPanel.transform) {
            Destroy(child.gameObject);
        }

        //create new slots

        for (int i = 0; i < slotCount; i++) {
            Instantiate(slotPrefab, hotbarPanel.transform);

            Slot slot = hotbarPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
        }

        //fill items into slots
        int fi = 0;
        foreach (InventorySaveData data in hotbarSaveData) {

            if (data.slotIndex < slotCount) {
                Slot slot = hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();


                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);

                if (itemPrefab != null) {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
            fi++;

        }
    }

}
