using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
	[SerializeField] GameObject[] candiesArray;

    private void Start() => Initialize();

    private void Initialize()
	{
		int randomCandy = Random.Range(0, candiesArray.Length);

		GameObject spawnedCandy = 
			Instantiate(
				candiesArray[randomCandy], 
				transform.position, 
				Quaternion.identity, 
				transform);

		spawnedCandy.name = gameObject.name;
	}
}
