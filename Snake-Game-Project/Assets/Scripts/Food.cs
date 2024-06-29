using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;

    private void Start() => RandomizePosition();

    private void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        float xBoundArea = Random.Range(bounds.min.x, bounds.max.x);
        float yBoundArea = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(xBoundArea), Mathf.Round(yBoundArea), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            RandomizePosition();
    }
}
