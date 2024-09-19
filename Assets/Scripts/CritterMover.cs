using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterMover: MonoBehaviour
{
    [SerializeField] private GameObject startPosition;
    [SerializeField] private GameObject endPosition;
    private GameObject critter;
    private GameObject critterHolder;

    [SerializeField] private float BASE_TRAVERSE;
    private float traverseDuration;
    [SerializeField] private float traverseVariance;
    private float traverseTimer;

    [SerializeField] private float critterSizeVariance;

    [SerializeField] private int layerOrder;

    private bool movingToEnd;
    private bool isVacant;

    public bool IsVacant { get => isVacant; set => isVacant = value; }


    private void Awake()
    {
        isVacant = true;
        critterHolder = GameObject.Find("Critter Holder");
    }

    private void Update()
    {
        // Return if there is no Critter
        if(critter == null)
        {
            movingToEnd = true;
            return;
        }

        traverseTimer += Time.deltaTime;

        MoveCritter();
    }

    /// <summary>
    /// Instantiates critter prefab at random positions within a specified area.
    /// </summary>
    /// <param name="critterPrefab">The prefab of the critter to instantiate.</param>
    /// <param name="number">A string representing the number (id) of the critter to instantiate.</param>

    public bool NewCritter(GameObject critterPrefab, string number)
    {
        if(critter == null)
        {
            // Reset movement timer
            traverseTimer = 0f;

            // Instantiate critter
            critter = Instantiate(critterPrefab, this.transform.position, Quaternion.identity);

            // Name critter
            critter.name = $"{critterPrefab.name} {number}";

            // Size critter 
            // get local scale from x to set min / max
            // randomize scale from min / max
            float scale = Random.Range((critter.transform.localScale.x - critterSizeVariance), critter.transform.localScale.x + critterSizeVariance);
            critter.transform.localScale = new Vector2(scale, scale);

            // Set Critter sorting order to one less than this
            critter.GetComponent<SpriteRenderer>().sortingOrder = layerOrder - 1;

            // Set critter parent in scene
            critter.transform.SetParent(critterHolder.transform);

            // Randomize the critter's traverse duration
            //traverseDuration = BASETRAVERSEDURATION;
            traverseDuration = BASE_TRAVERSE + UnityEngine.Random.Range(-traverseVariance, traverseVariance);

            return true;
        }
        else
        {
            // There is already a critter here!
            return false;
        }
    }

    /// <summary>
    /// Animates the critter's movement between the specified start and end positions.
    /// </summary>
    private void MoveCritter()
    {
        if(movingToEnd)
        {
            // Move critter to end position
            critter.transform.position = Vector2.Lerp(startPosition.transform.position, endPosition.transform.position, (traverseTimer / traverseDuration));
            if(traverseTimer >= traverseDuration)
            {
                movingToEnd = false;
                traverseTimer = 0f;
            }
        }
        else
        {
            // Move critter to start position
            critter.transform.position = Vector3.Lerp(endPosition.transform.position, startPosition.transform.position, (traverseTimer / traverseDuration));
            if(traverseTimer >= traverseDuration)
            {
                movingToEnd = true;
                traverseTimer = 0f;
            }
        }
    }
}
