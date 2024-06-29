using UnityEngine;
using System.Collections.Generic;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] Transform snakeSegmentsPrefab;
    [SerializeField] int initialSize;

    Vector2 direction = Vector2.right;
    List<Transform> snakeSegmentsList = new List<Transform>();

    private void Start() => ResetGameState();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S))
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A))
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D))
            direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        for(int i = snakeSegmentsList.Count - 1; i > 0; i--)
            snakeSegmentsList[i].position = snakeSegmentsList[i - 1].position;

        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0.0f);
    }

    private void GrowSnake()
    {
        Transform snakeSegmentTransform = Instantiate(snakeSegmentsPrefab);
        snakeSegmentTransform.position = snakeSegmentsList[snakeSegmentsList.Count - 1].position;

        snakeSegmentsList.Add(snakeSegmentTransform);
    }

    private void ResetGameState()
    {
        for(int i = 1; i < snakeSegmentsList.Count; i++)
            Destroy(snakeSegmentsList[i].gameObject);

        snakeSegmentsList.Clear();
        snakeSegmentsList.Add(transform);

        for(int i = 1; i < initialSize; i++)
            snakeSegmentsList.Add(Instantiate(snakeSegmentsPrefab));

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
            GrowSnake();
        else if(collision.tag == "Obstacle")
            ResetGameState();
    }
}
