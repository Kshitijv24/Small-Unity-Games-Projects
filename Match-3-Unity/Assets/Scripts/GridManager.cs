using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public GameObject[,] candies2dArray;
    public int width, height;

	[SerializeField] float cellSize;
	[SerializeField] BackgroundTile[] backgroundTileArray;
    [SerializeField] GameObject[] candiesArray;

    BackgroundTile[,] allBackgroundTilesArray;
    int maxIterations;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.Log("There are more than one " + this.GetType() + " Instances", this);
            return;
        }
    }

    private void Start() => GenerateGrid();

    private void GenerateGrid()
    {
        allBackgroundTilesArray = new BackgroundTile[width, height];
        candies2dArray = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                InitializeBackgroundTiles(x, y);
                InitializeCandies(x, y);
            }
        }
    }

    private void InitializeBackgroundTiles(int x, int y)
    {
        int randomBackgroundTile = Random.Range(0, backgroundTileArray.Length);

        BackgroundTile spawnedBackgroundTile =
            Instantiate(
                backgroundTileArray[randomBackgroundTile],
                GetWorldPosition(x, y),
                Quaternion.identity,
                transform);

        spawnedBackgroundTile.name = $"Background Tile ({x},{y})";
    }

    private void InitializeCandies(int x, int y)
    {
        int randomCandy = Random.Range(0, candiesArray.Length);

        while(RemoveStartingBoardMatches(x, y, candiesArray[randomCandy]) && maxIterations < 100)
        {
            randomCandy = Random.Range(0, candiesArray.Length);
            maxIterations++;
            Debug.Log(maxIterations);
        }
        maxIterations = 0;

        GameObject spawnedCandy =
            Instantiate(
                candiesArray[randomCandy],
                GetWorldPosition(x, y),
                Quaternion.identity,
                transform);

        spawnedCandy.name = $"Candey ({x},{y})";
        candies2dArray[x , y] = spawnedCandy;
    }

    private Vector2 GetWorldPosition(int x, int y) => new Vector2(x, y) * cellSize;

    private bool RemoveStartingBoardMatches(int column, int row, GameObject candeyPiece)
    {
        if (column > 1 && row > 1)
        {
            // Colums Check
            if (candies2dArray[column - 1, row].GetComponent<Candey>().candeyType 
                == candeyPiece.GetComponent<Candey>().candeyType &&
                candies2dArray[column - 2, row].GetComponent<Candey>().candeyType 
                == candeyPiece.GetComponent<Candey>().candeyType)
            {
                return true;
            }
            // Row Check
            if (candies2dArray[column, row - 1].GetComponent<Candey>().candeyType
                == candeyPiece.GetComponent<Candey>().candeyType &&
                candies2dArray[column, row - 2].GetComponent<Candey>().candeyType
                == candeyPiece.GetComponent<Candey>().candeyType)
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (candies2dArray[column, row - 1].GetComponent<Candey>().candeyType
                == candeyPiece.GetComponent<Candey>().candeyType &&
                candies2dArray[column, row - 2].GetComponent<Candey>().candeyType
                == candeyPiece.GetComponent<Candey>().candeyType)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (candies2dArray[column - 1, row].GetComponent<Candey>().candeyType
                == candeyPiece.GetComponent<Candey>().candeyType &&
                candies2dArray[column - 2, row].GetComponent<Candey>().candeyType
                == candeyPiece.GetComponent<Candey>().candeyType)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
