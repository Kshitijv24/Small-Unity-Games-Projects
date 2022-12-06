using UnityEngine;

public class SpawnPillars : MonoBehaviour
{
    [SerializeField] GameObject pillarPrefab;

    private void Start()
    {
        Instantiate(pillarPrefab, transform.position, Quaternion.identity);
    }
}
