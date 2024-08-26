using System.Collections;
using UnityEngine;

public class Candey : MonoBehaviour
{
    public CandeyType candeyType;
    
    [HideInInspector] public bool isMatched;
    [HideInInspector] public int column, row;

    [SerializeField] float candeyMoveSpeed = 10f;

    int targetX, targetY;
    int previousColumn, previousRow;
    float swipeAngle = 0;
    float swipeResist = 0.5f;

    Vector2 firstTouchPosition;
    Vector2 finalTouchPosition;
    Vector2 tempPosition;
    GameObject otherCandey;
    SpriteRenderer spriteRenderer;

    private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

    private void Start()
    {
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;

        row = targetY;
        column = targetX;
        previousRow = row;
        previousColumn = column;
    }

    private void Update()
    {
        if (isMatched)
            spriteRenderer.color = new Color(1, 1, 1, 0.2f);

        MoveCandeyPices();
        HandleCandeyMatches();
    }

    private void OnMouseDown() => 
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateSwipeAngle();
    }

    private void MoveCandeyPices()
    {
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > 0.1f)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, candeyMoveSpeed * Time.deltaTime);

            if (Board.Instance.allCandies2DArray[column, row] != this.gameObject)
                Board.Instance.allCandies2DArray[column, row] = this.gameObject;
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }
        if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, candeyMoveSpeed * Time.deltaTime);

            if (Board.Instance.allCandies2DArray[column, row] != this.gameObject)
                Board.Instance.allCandies2DArray[column, row] = this.gameObject;
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    private void HandleSwipe()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < Board.Instance.width - 1)
        {
            // Right Swipe Detected
            otherCandey = Board.Instance.allCandies2DArray[column + 1, row];
            otherCandey.GetComponent<Candey>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < Board.Instance.height - 1)
        {
            // Up Swipe Detected
            otherCandey = Board.Instance.allCandies2DArray[column, row + 1];
            otherCandey.GetComponent<Candey>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            // Left Swipe Detected
            otherCandey = Board.Instance.allCandies2DArray[column - 1, row];
            otherCandey.GetComponent<Candey>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Down Swipe Detected
            otherCandey = Board.Instance.allCandies2DArray[column, row - 1];
            otherCandey.GetComponent<Candey>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckIfMovePossible());
    }

    private void CalculateSwipeAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist ||
            Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(
            finalTouchPosition.y - firstTouchPosition.y,
            finalTouchPosition.x - firstTouchPosition.x)
            * 180 / Mathf.PI;

            HandleSwipe();
        }
    }

    private void HandleCandeyMatches()
    {
        HorizontalMatches();
        VerticalMatches();
    }

    private void HorizontalMatches()
    {
        if (column > 0 && column < Board.Instance.width - 1)
        {
            // check left side of candey
            GameObject leftCandey = Board.Instance.allCandies2DArray[column - 1, row];
            // check right side of candey
            GameObject rightCandey = Board.Instance.allCandies2DArray[column + 1, row];

            if (leftCandey != null && rightCandey != null)
            {
                if (leftCandey.GetComponent<Candey>().candeyType == candeyType &&
                    rightCandey.GetComponent<Candey>().candeyType == candeyType)
                {
                    leftCandey.GetComponent<Candey>().isMatched = true;
                    rightCandey.GetComponent<Candey>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }

    private void VerticalMatches()
    {
        if (row > 0 && row < Board.Instance.height - 1)
        {
            // check up side of candey
            GameObject upCandey = Board.Instance.allCandies2DArray[column, row + 1];
            // check down side of candey
            GameObject downCandey = Board.Instance.allCandies2DArray[column, row - 1];

            if (upCandey != null && downCandey != null)
            {
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

    public IEnumerator CheckIfMovePossible()
    {
        yield return new WaitForSeconds(0.5f);

        if(otherCandey != null)
        {
            if(!isMatched && !otherCandey.GetComponent<Candey>().isMatched)
            {
                otherCandey.GetComponent<Candey>().row = row;
                otherCandey.GetComponent<Candey>().column = column;
                row = previousRow;
                column = previousColumn;
            }
            else
            {
                Board.Instance.FindMatchesToBeDestroyed();
            }
            otherCandey = null;
        }
    }
}
