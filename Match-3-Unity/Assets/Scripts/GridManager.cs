using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public GameObject[,] allCandiesArray;
    public int width, height;

	[SerializeField] float cellSize;
	[SerializeField] BackgroundTile[] backgroundTileArray;
    [SerializeField] GameObject[] candiesArray;

    BackgroundTile[,] allBackgroundTilesArray;

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
        allCandiesArray = new GameObject[width, height];

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

        GameObject spawnedCandy =
            Instantiate(
                candiesArray[randomCandy],
                GetWorldPosition(x, y),
                Quaternion.identity,
                transform);

        spawnedCandy.name = $"Candey ({x},{y})";
        allCandiesArray[x , y] = spawnedCandy;
    }

    private Vector2 GetWorldPosition(int x, int y) => new Vector2(x, y) * cellSize;
}
