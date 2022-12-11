using UnityEngine;

public class Pillars : MonoBehaviour
{
    public static float moveSpeed = 2;
    [SerializeField] float maxMoveSpeed = 6;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        MovingPillarsLeft();
    }

    private void MovingPillarsLeft()
    {
        if(moveSpeed < maxMoveSpeed)
        {
            moveSpeed += 0.01f * Time.deltaTime;
        }

        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Bird")
        {
            FindObjectOfType<ScoreSystem>().IncrementScore();
            audioSource.Play();
        }
    }
}
