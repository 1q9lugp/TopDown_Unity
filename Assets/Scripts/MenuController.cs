using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    void Start()
    {
        menuCanvas.SetActive(false);
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { 
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
