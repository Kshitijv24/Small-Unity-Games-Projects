using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Sprite[] backgrounds;

    SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        int randomBackground = Random.Range(0, backgrounds.Length);
        spriteRenderer.sprite = backgrounds[randomBackground];
    }
}
