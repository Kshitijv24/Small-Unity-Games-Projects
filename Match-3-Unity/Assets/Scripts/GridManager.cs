using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField] int width, height;
	[SerializeField] float cellSize;
	[SerializeField] Tile tilePrefab;

    private void Start() => GenerateGrid();

    private void GenerateGrid()
	{
		for (int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				Tile spawnedTile = Instantiate(tilePrefab, GetWorldPosition(x, y), Quaternion.identity);
				spawnedTile.name = $"Tile {x} {y}";
			}
		}
	}

    private Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * cellSize;
}
