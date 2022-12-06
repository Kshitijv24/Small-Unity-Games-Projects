using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pillars")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Bird")
        {
            SceneManager.LoadScene(0);
        }
    }
}
