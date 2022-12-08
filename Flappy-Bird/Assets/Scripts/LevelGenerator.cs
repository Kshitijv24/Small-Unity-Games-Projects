using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] pillarPrefab;
    
    public float nextPillarSpawnDelay = 2;

    private void Start()
    {
        StartCoroutine(GeneratorNextPillar());
    }

    private void Update()
    {
        nextPillarSpawnDelay -= 0.01f * Time.deltaTime;
    }

    IEnumerator GeneratorNextPillar()
    {
        while (true)
        {
            int randomPillarPrefab = Random.Range(0, pillarPrefab.Length);
            Vector2 randomSpawnHightPosition = new Vector2(0, Random.Range(0, 5));
            Instantiate(
                pillarPrefab[randomPillarPrefab], randomSpawnHightPosition, transform.rotation);
            yield return new WaitForSeconds(nextPillarSpawnDelay);
        }
    }
}
