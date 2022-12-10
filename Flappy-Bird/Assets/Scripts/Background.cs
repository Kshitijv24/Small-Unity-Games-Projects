using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Sprite[] backgrounds;

    SpriteRenderer spriteRenderer;
    int nextBackground = 1;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeBackground()
    {
        spriteRenderer.sprite = backgrounds[nextBackground];
        nextBackground++;

        if(nextBackground == 6)
        {
            nextBackground = 0;
        }
    }
}
