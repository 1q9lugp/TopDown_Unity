using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData
{

    public Vector3 playerPosition;
    public string mapBoundary; //boundary Name
    public List<InventorySaveData> inventorySaveData;
    public List<InventorySaveData> hotbarSaveData;
}
