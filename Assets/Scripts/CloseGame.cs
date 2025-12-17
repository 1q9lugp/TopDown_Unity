using UnityEngine;

public class CloseGame : MonoBehaviour{ 
    public void doExitGame() {
        Debug.Log("Closing game");
        Application.Quit();
    }

}
