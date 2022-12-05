using UnityEngine;

public class Pillars : MonoBehaviour
{
    [SerializeField] int moveSpeed;

    private void Update()
    {
        MovingPillarsLeft();
    }

    private void MovingPillarsLeft()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
