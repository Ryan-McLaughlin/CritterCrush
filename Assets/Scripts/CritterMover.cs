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
    [SerializeField] private GameObject startPosition;
    [SerializeField] private GameObject endPosition;

    [SerializeField] private int layerOrder;

    private void Awake()
    {
        isVacant = true;

        //startPosition = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    private void Update()
    {
        // Return if there is no Critter
        if(critter == null)
        {
            movingToEnd = true;
            return;
        }

        elapsedTime += Time.deltaTime;

        MoveCritter();
    }

    /// <summary>
    /// Instantiates multiple critter prefabs at random positions within a specified area.
    /// </summary>
    /// <param name="critterPrefab">The prefab of the critter to instantiate.</param>
    /// <param name="number">A string representing the number of critters to instantiate.</param>

    public bool NewCritter(GameObject critterPrefab, string number)
    {
        if(critter == null)
        {
            // Reset timer
            elapsedTime = 0f;

            // Set Critter sorting order to one less than this
            critter = Instantiate(critterPrefab, this.transform.position, Quaternion.identity);
            critter.GetComponent<SpriteRenderer>().sortingOrder = layerOrder;// this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            critter.transform.SetParent(this.transform);
            critter.name= $"{critterPrefab.name} {number}";

            return true;
        } else
        {
            //Debug.Log("CritterMover.NewCritter(): critter != null");
            return false;
        }
    }

    private void MoveCritter()
    {
        if(movingToEnd)
        {
            // Move critter to end position
            critter.transform.position = Vector2.Lerp(startPosition.transform.position, endPosition.transform.position, (elapsedTime / duration));
            if(elapsedTime >= duration)
            {
                movingToEnd = false;
                elapsedTime = 0f;
            }
        } else
        {
            // Move critter to start position
            critter.transform.position = Vector3.Lerp(endPosition.transform.position, startPosition.transform.position, (elapsedTime / duration));
            if(elapsedTime >= duration)
            {
                movingToEnd = true;
                elapsedTime = 0f;
            }
        }
    }
}
