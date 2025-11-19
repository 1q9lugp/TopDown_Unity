using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransation : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundry;
    private CinemachineConfiner2D confiner;

    [SerializeField] private Direction direction;

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Change camera bounds
            confiner.BoundingShape2D = mapBoundry;
            confiner.InvalidateCache(); // good practice after changing bounds

            // Move player to new position based on direction
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += 2f;
                break;

            case Direction.Down:
                newPos.y -= 2f;
                break;

            case Direction.Left:
                newPos.x -= 2f;
                break;

            case Direction.Right:
                newPos.x += 2f;
                break;
        }

        player.transform.position = newPos;
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
