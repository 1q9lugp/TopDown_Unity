using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    public float minDropDistance = 2f;
    public float maxDropDistance = 3f;

    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        originalParent = transform.parent; //saves OG parent
        transform.SetParent(transform.root); //above other canvases

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; //semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position; //follow mouse
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true; //can click on(?)
        canvasGroup.alpha = 1f;

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        if (dropSlot == null) { 
            GameObject dropItem =eventData.pointerEnter;
            if (dropItem != null) {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }

        Slot originalSlot = originalParent.GetComponent<Slot>();


        if (dropSlot != null) { //placed in slot

            if (dropSlot.currentItem != null) { //swap 
                dropSlot.currentItem.transform.SetParent(originalSlot.transform); 
                originalSlot.currentItem = dropSlot.currentItem; 

                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                
            } else { //delete original item(?)
                originalSlot.currentItem = null;
               
            }

            //place into new slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        } else { //no slot

            //mouse outside of inventory page
            if (!IsWithinInventory(eventData.position)) {
                //drop Item
                DropItem(originalSlot);

            } else { 
                //put back into og slot
                transform.SetParent(originalParent);
            }

        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //center
    }


    bool IsWithinInventory(Vector2 mousePosition) {

        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
    }

    void DropItem(Slot originalSlot) {
        originalSlot.currentItem = null;

        //find player
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null) {
            Debug.LogError("object with a tag 'Player' doesnt exist");
            return;
        }

        //random drop position

        Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
        
        Vector2 dropPosition = (Vector2)playerTransform.position + dropOffset;

        //instantiate dropped item and bounce :3
        GameObject dropItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        dropItem.GetComponent<BounceEffect>().StartBounce();

        //destroy UI item (aka holding)
        Destroy(gameObject);
    }
    
}
