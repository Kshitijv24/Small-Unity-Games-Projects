using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] int moveAmount;

    Rigidbody2D rb;
    float timerLength = 2f;
    float timePassed = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            MoveBirdUP();
        }
    }

    private void MoveBirdUP()
    {
        rb.velocity = Vector2.up * moveAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pillars")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            timePassed += Time.deltaTime;
            if(timePassed >= timerLength)
            {
                timePassed = 0f;
                Pillars.moveSpeed = 5;
                FindObjectOfType<LevelGenerator>().nextPillarSpawnDelay = 1;
            }
            else
            {
                Pillars.moveSpeed = 2;
                FindObjectOfType<LevelGenerator>().nextPillarSpawnDelay = 2;
            }
        }
    }
}
