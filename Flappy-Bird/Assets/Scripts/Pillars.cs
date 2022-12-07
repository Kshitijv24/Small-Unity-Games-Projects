using UnityEngine;

public class Pillars : MonoBehaviour
{
    public static int moveSpeed = 2;

    private void Update()
    {
        MovingPillarsLeft();
    }

    private void MovingPillarsLeft()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Bird")
        {
            FindObjectOfType<ScoreSystem>().IncrementScore();
        }
    }
}
