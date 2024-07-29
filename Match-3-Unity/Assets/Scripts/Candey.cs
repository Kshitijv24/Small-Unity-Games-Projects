using UnityEngine;

public class Candey : MonoBehaviour
{
    public float swipeAngle = 0;
    public int targetX, targetY;
    public int column, row;
    public CandeyType candeyType;

    [SerializeField] float candeyMoveSpeed = 10f;

    Vector2 firstTouchPosition;
    Vector2 finalTouchPosition;
    Vector2 tempPosition;
    GameObject otherCandey;
    bool isMatched;
    SpriteRenderer spriteRenderer;

    private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

    private void Start()
    {
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;

        row = targetY;
        column = targetX;
    }

    private void Update()
    {
        if (isMatched)
            spriteRenderer.color = new Color(1, 1, 1, 0.2f);

        MoveCandeyPices();
        HandleCandeyMatches();
    }

    private void MoveCandeyPices()
    {
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > 0.1f)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, candeyMoveSpeed * Time.deltaTime);
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            GridManager.Instance.allCandiesArray[column, row] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, candeyMoveSpeed * Time.deltaTime);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            GridManager.Instance.allCandiesArray[column, row] = this.gameObject;
        }
    }

    private void OnMouseDown() => 
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateSwipeAngle();
    }

    private void CalculateSwipeAngle()
    {
        swipeAngle = Mathf.Atan2(
            finalTouchPosition.y - firstTouchPosition.y,
            finalTouchPosition.x - firstTouchPosition.x)
            * 180 / Mathf.PI;

        HandleSwipe();
    }

    private void HandleSwipe()
    {
        if(swipeAngle > -45 && swipeAngle <= 45 && column < GridManager.Instance.width - 1)
        {
            // Right Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column + 1, row];
            otherCandey.GetComponent<Candey>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < GridManager.Instance.height - 1)
        {
            // Up Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column, row + 1];
            otherCandey.GetComponent<Candey>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            // Left Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column - 1, row];
            otherCandey.GetComponent<Candey>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Down Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column, row - 1];
            otherCandey.GetComponent<Candey>().row += 1;
            row -= 1;
        }
    }

    private void HandleCandeyMatches()
    {
        HorizontalMatches();
        VerticalMatches();
    }

    private void HorizontalMatches()
    {
        if (column > 0 && column < GridManager.Instance.width - 1)
        {
            // check left side of candey
            GameObject leftCandey = GridManager.Instance.allCandiesArray[column - 1, row];
            // check right side of candey
            GameObject rightCandey = GridManager.Instance.allCandiesArray[column + 1, row];

            if (leftCandey.GetComponent<Candey>().candeyType == candeyType &&
                rightCandey.GetComponent<Candey>().candeyType == candeyType)
            {
                leftCandey.GetComponent<Candey>().isMatched = true;
                rightCandey.GetComponent<Candey>().isMatched = true;
                isMatched = true;
            }
        }
    }

    private void VerticalMatches()
    {
        if (row > 0 && row < GridManager.Instance.height - 1)
        {
            // check up side of candey
            GameObject upCandey = GridManager.Instance.allCandiesArray[column, row + 1];
            // check down side of candey
            GameObject downCandey = GridManager.Instance.allCandiesArray[column, row - 1];

            if (upCandey.GetComponent<Candey>().candeyType == candeyType &&
                downCandey.GetComponent<Candey>().candeyType == candeyType)
            {
                upCandey.GetComponent<Candey>().isMatched = true;
                downCandey.GetComponent<Candey>().isMatched = true;
                isMatched = true;
            }
        }
    }
}
