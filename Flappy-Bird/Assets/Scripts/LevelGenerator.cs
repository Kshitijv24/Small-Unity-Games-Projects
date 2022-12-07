using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject pillarPrefab;
    
    public float nextPillarSpawnDelay;

    private void Start()
    {
        StartCoroutine(GeneratorNextPillar());
    }

    IEnumerator GeneratorNextPillar()
    {
        while (true)
        {
            Vector2 randomSpawnPosition = new Vector2(0, Random.Range(0, 5));
            Instantiate(pillarPrefab, randomSpawnPosition, transform.rotation);
            yield return new WaitForSeconds(nextPillarSpawnDelay);
        }
    }
}
