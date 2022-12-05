using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pillars")
        {
            Destroy(collision.gameObject);
        }
    }
}
