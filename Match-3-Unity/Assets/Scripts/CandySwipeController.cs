using UnityEngine;

public class CandySwipeController : MonoBehaviour
{
    public float swipeAngle = 0;
    public int targetX, targetY;
    public int column, row;

    Vector2 firstTouchPosition;
    Vector2 finalTouchPosition;
    Vector2 tempPosition;
    GameObject otherCandey;

    private void Start()
    {
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;

        row = targetY;
        column = targetX;
    }

    private void Update() => MoveCandeyPices();

    private void MoveCandeyPices()
    {
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > 0.1f)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
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
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            GridManager.Instance.allCandiesArray[column, row] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

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
        if(swipeAngle > -45 && swipeAngle <= 45 && column < GridManager.Instance.width)
        {
            // Right Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column + 1, row];
            otherCandey.GetComponent<CandySwipeController>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < GridManager.Instance.height)
        {
            // Up Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column, row + 1];
            otherCandey.GetComponent<CandySwipeController>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            // Left Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column - 1, row];
            otherCandey.GetComponent<CandySwipeController>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Down Swipe Detected
            otherCandey = GridManager.Instance.allCandiesArray[column, row - 1];
            otherCandey.GetComponent<CandySwipeController>().row += 1;
            row -= 1;
        }
    }
}
