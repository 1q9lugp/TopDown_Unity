using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class mapTransition : MonoBehaviour
{


    [SerializeField] PolygonCollider2D mapBoundary;

    CinemachineConfiner2D confiner;


    private void Awake() {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) { 
            confiner.BoundingShape2D = mapBoundary;
        }
    }

}
