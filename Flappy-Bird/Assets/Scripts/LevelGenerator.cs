using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject pillarPrefab;
    [SerializeField] float nextPillarSpawnDelay;

    private void Start()
    {
        StartCoroutine(GeneratorNextPillar());
    }

    IEnumerator GeneratorNextPillar()
    {
        while (true)
        {
            Instantiate(pillarPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(nextPillarSpawnDelay);
        }
    }
}
