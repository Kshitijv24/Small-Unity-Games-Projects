using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField] int width, height;
	[SerializeField] float cellSize;
	[SerializeField] BackgroundTile[] backgroundTile;

	BackgroundTile[,] tilesArray;

    private void Start() => GenerateGrid();

    private void GenerateGrid()
	{
		tilesArray = new BackgroundTile[width, height];

		for (int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				int randomBackgroundTile = Random.Range(0, backgroundTile.Length);

				BackgroundTile spawnedBackgroundTile = 
					Instantiate(
						backgroundTile[randomBackgroundTile], 
						GetWorldPosition(x, y), 
						Quaternion.identity, 
						transform);

				spawnedBackgroundTile.name = $"Tile ({x},{y})";
			}
		}
	}

    private Vector2 GetWorldPosition(int x, int y) => new Vector2(x, y) * cellSize;
}
