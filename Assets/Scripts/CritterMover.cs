using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterMover : MonoBehaviour
{
    [SerializeField] private float timeToWait;
    //private float timeWaited;

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

    [SerializeField] private float duration;

    private bool[] directionAllowed = new bool[4];
    private bool movingToEnd;
    private bool isVacant;
    public bool IsVacant { get => isVacant; set => isVacant = value; }

    private float elapsedTime;

    [SerializeField] private float moveTotalTime;

    private GameObject critter;
    private Vector2 startPosition;
    private Vector2 endPosition;

    private void Awake()
    {
        directionAllowed[0] = up;
        directionAllowed[1] = down;
        directionAllowed[2] = left;
        directionAllowed[3] = right;

        //timeWaited = 0f;
        //moveDurationTime = 0f;

        isVacant = true;

        startPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        NewEndPosition(this.transform.position.x + rightOffset, this.transform.position.y);
    }

    private void Update()
    {
        // Return if there is no Critter
        if (critter == null)
        {
            movingToEnd = true;
            return;
        }

        elapsedTime += Time.deltaTime;

        // Verify the Critter can move in the chosen direction
        bool directionOk = false;
        while (!directionOk)
        {
            int roll = Random.Range(0, 4);

            // Move Critter in chosen direction
            if (directionAllowed[roll])
            {
                if (movingToEnd)
                {
                    // Move critter to end position
                    critter.transform.position = Vector2.Lerp(startPosition, endPosition, (elapsedTime / duration));
                    if (elapsedTime >= duration)
                    {
                        movingToEnd = false;
                        elapsedTime= 0f;
                    }
                }
                else
                {
                    // Move critter to start position
                    critter.transform.position = Vector3.Lerp(endPosition, startPosition, (elapsedTime / duration));
                    if (elapsedTime >= duration)
                    {
                        movingToEnd = true;
                        elapsedTime = 0f;
                    }
                }
                directionOk = true;
                /*
                Debug.Log($"random: {roll}");
                switch (roll)
                {
                    case 0:
                        MoveCritter(upOffset, 0f);
                        break;

                    case 1:
                        MoveCritter(downOffset, 0f);
                        break;

                    case 2:
                        MoveCritter(0f, leftOffset);
                        break;
                    case 3:
                        MoveCritter(0f, rightOffset);
                        break;
                }
                */
            }
        }


        //timeWaited = 0;

        //moveDurationTime += Time.deltaTime;
    }

    public void NewCritter(GameObject critterPrefab)
    {
        if (critter == null)
        {
            critter = Instantiate(critterPrefab, this.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("CritterMover.NewCritter(): critter != null");
        }
    }

    void NewEndPosition(float x, float y)
    {
        endPosition = new Vector2(x, y);
    }

    /*
    private void MoveCritter(float x, float y)
    {
        //Vector2 endPosition = new Vector2(x, y);
        bool moveToStart = false;
        if (moveToStart)
        {
            critter.transform.position = Vector2.Lerp(startPosition, endPosition, (moveDurationTime / moveTotalTime));
        }
        else // move to end
        {
            critter.transform.position = Vector2.Lerp(endPosition, startPosition, x);
        }

    }
    */

    private void ChooseDirection()
    {
        Debug.Log($"CritterMover.ChooseDirection()");
    }
}
