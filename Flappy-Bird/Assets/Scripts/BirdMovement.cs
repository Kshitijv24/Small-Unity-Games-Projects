using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] int moveAmount;

    Rigidbody2D rb;

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
            GameEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Big")
        {
            MakeBirdBig(collision);
        }
        else if(collision.gameObject.tag == "Small")
        {
            MakeBirdSmall(collision);
        }
        else if(collision.gameObject.tag == "Normal")
        {
            MakeBirdNormal(collision);
        }
    }

    private void GameEnd()
    {
        SceneManager.LoadScene(0);
        Pillars.moveSpeed = 2;
    }

    private void MakeBirdBig(Collider2D collision)
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Destroy(collision.gameObject);
    }

    private void MakeBirdSmall(Collider2D collision)
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(collision.gameObject);
    }

    private void MakeBirdNormal(Collider2D collision)
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Destroy(collision.gameObject);
    }
}
