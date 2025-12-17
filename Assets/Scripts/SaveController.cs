using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    private hotbarController hotbarController;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindFirstObjectByType<InventoryController>();
        hotbarController = FindFirstObjectByType<hotbarController>();

        loadGame();
    }

    public void saveGame() {
        SaveData saveData = new SaveData {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.gameObject.name,
            inventorySaveData = inventoryController.GetInventoryItems(),
            hotbarSaveData = hotbarController.GetHotbarItems(),
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    // public void loadGame() {
    //     if (File.Exists(saveLocation)) {

    //         SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));


    //         GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
    //         FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D = 
    //         GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
    //         inventoryController.SetInventoryItems(saveData.inventorySaveData);
    //         hotbarController.SetHotbarItems(saveData.hotbarSaveData);

    //     } else {
    //         saveGame();
    //     }
    // }







public void loadGame() {
    if (File.Exists(saveLocation)) {

        SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

        // Move Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            player.transform.position = saveData.playerPosition;
        }

        // Assign Cinemachine Boundary - FIX APPLIED HERE
        CinemachineConfiner2D confiner = FindFirstObjectByType<CinemachineConfiner2D>();
        GameObject boundaryObj = GameObject.Find(saveData.mapBoundary);

        if (confiner != null && boundaryObj != null) {
            // Updated from m_BoundingShape2D to BoundingShape2D
            confiner.BoundingShape2D = boundaryObj.GetComponent<PolygonCollider2D>();
            
            // This ensures the camera updates its bounds immediately
            confiner.InvalidateBoundingShapeCache(); 
        }

        // Inventory and Hotbar
        if (inventoryController != null) {
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
        
        if (hotbarController != null) {
            hotbarController.SetHotbarItems(saveData.hotbarSaveData);
        }

    } else {
        saveGame();
    }
}



}
