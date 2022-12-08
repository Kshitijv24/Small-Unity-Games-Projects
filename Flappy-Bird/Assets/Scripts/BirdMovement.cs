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

    private void GameEnd()
    {
        SceneManager.LoadScene(0);
        Pillars.moveSpeed = 2;
    }
}
