using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public int ID;
    public string Name;
    //public ScriptableObject useItemScript; //what i thought would be used

    public virtual void PickUp() {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if (ItemPopupUiController.Instance != null) {
            ItemPopupUiController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }

    public virtual void useItem() {
        Debug.Log($"Using item {Name}");
    }
}
