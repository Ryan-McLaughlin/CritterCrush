using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterMover: MonoBehaviour
{
    //[SerializeField] private float timeToWait;
    /*
    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool up;
    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool down;
    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool left;
    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool right;

    [SerializeField] private float upOffset;
    [SerializeField] private float downOffset;
    [SerializeField] private float leftOffset;
    [SerializeField] private float rightOffset;
    */
    [SerializeField] private float duration;


    // use move direction to choose where the critter popes out of
    // gonna get rid of all but one offset, and remove the bool array
    // bool array should be replaced with this
    /*
    private enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    [SerializeField] private MoveDirection moveDirection;
    */
    //[SerializeField] private float xOffset;
    //[SerializeField] private float yOffset;

    //private bool[] directionAllowed = new bool[4];
    private bool movingToEnd;
    private bool isVacant;
    public bool IsVacant { get => isVacant; set => isVacant = value; }

    private float elapsedTime;

    //[SerializeField] private float moveTotalTime;

    private GameObject critter;
    private Vector2 startPosition;
    [SerializeField] private GameObject endPosition;

    private void Awake()
    {
        /*
        directionAllowed[0] = up;
        directionAllowed[1] = down;
        directionAllowed[2] = left;
        directionAllowed[3] = right;
        */

        isVacant = true;

        startPosition = new Vector2(this.transform.position.x, this.transform.position.y);

        //NewEndPosition();
        //endPosition = new Vector2(this.transform.position.x + xOffset, this.transform.position.y + yOffset);
    }

    /*
    private void NewEndPosition() {
        switch(moveDirection) {
            case MoveDirection.Up:
                endPosition = new Vector2(this.transform.position.x, this.transform.position.y + offset);
                break;

            case MoveDirection.Down:
                endPosition = new Vector2(this.transform.position.x, this.transform.position.y + offset);
                break;

            case (MoveDirection.Left):
                endPosition = new Vector2(this.transform.position.x + offset, this.transform.position.y);
                break;

            case MoveDirection.Right:
                endPosition = new Vector2(this.transform.position.x + offset, this.transform.position.y);
                break;

        }
    }
    */

    private void Update()
    {
        // Return if there is no Critter
        if(critter == null)
        {
            movingToEnd = true;
            return;
        }

        elapsedTime += Time.deltaTime;
        // Move Critter
        MoveCritter();

        // Verify the Critter can move in the chosen direction
        /*
        bool directionOk = false;
        while(!directionOk) {
            int roll = Random.Range(0, 4);

            // Move Critter in chosen direction
            if(directionAllowed[roll]) {
                if(movingToEnd) {
                    // Move critter to end position
                    critter.transform.position = Vector2.Lerp(startPosition, endPosition, (elapsedTime / duration));
                    if(elapsedTime >= duration) {
                        movingToEnd = false;
                        elapsedTime = 0f;
                    }
                } else {
                    // Move critter to start position
                    critter.transform.position = Vector3.Lerp(endPosition, startPosition, (elapsedTime / duration));
                    if(elapsedTime >= duration) {
                        movingToEnd = true;
                        elapsedTime = 0f;
                    }
                }
                directionOk = true;
            }
        */

    }

    public void NewCritter(GameObject critterPrefab)
    {
        if(critter == null)
        {
            // Reset timer
            elapsedTime = 0f;

            // Set Critter sorting order to one less than this
            critter = Instantiate(critterPrefab, this.transform.position, Quaternion.identity);
            critter.GetComponent<SpriteRenderer>().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        } else
        {
            Debug.Log("CritterMover.NewCritter(): critter != null");
        }
    }

    private void MoveCritter()
    {
        if(movingToEnd)
        {
            // Move critter to end position
            critter.transform.position = Vector2.Lerp(startPosition, endPosition.transform.position, (elapsedTime / duration));
            if(elapsedTime >= duration)
            {
                movingToEnd = false;
                elapsedTime = 0f;
            }
        } else
        {
            // Move critter to start position
            critter.transform.position = Vector3.Lerp(endPosition.transform.position, startPosition, (elapsedTime / duration));
            if(elapsedTime >= duration)
            {
                movingToEnd = true;
                elapsedTime = 0f;
            }
        }
    }

    /*
    void NewEndPosition(float x, float y) {
        endPosition = new Vector2(x, y);
    }
    */

    private void ChooseDirection()
    {
        Debug.Log($"CritterMover.ChooseDirection()");
    }
}
