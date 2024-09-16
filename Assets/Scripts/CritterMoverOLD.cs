using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// moves critter it is assigned
// 

public class CritterMoverOLD : MonoBehaviour
{
    /*/ should have critter prefab passed in
    public GameObject critterPrefab;

    // points to move between
    public Transform startPoint;
    public Transform endPoint;

    // DEBUG
    private bool isDebugging;

    private bool isOperating;
    private bool isVacant;

    private float baseSpeed;
    private float currentSpeed;
    //private float maxSpeed;
    //private float minSpeed;

    private void Start()
    {
        isDebugging = false;
        if (isDebugging) { Debug.Log("   !!! DEBUGGING !!!   "); }

        isOperating = false;
        isVacant = false;

        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        Debug.Log($"{this.name}.Update()");

        // Linear Interpolation (Lerp)
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, currentSpeed);

        if (isOperating)
        {
            // update operationg
        }

        if (isVacant)
        {
            // update vacant;
        }
    }

    public void NewCritter(GameObject prefab)
    {
        Debug.Log($"CritterMover.NewCritter() {prefab.name}");
    }

    private void Reset()
    {
        Debug.Log($"CritterMover.Reset(): {this.name}");

        isVacant = true;
        // position
        currentSpeed = baseSpeed;
        isOperating= false;
        // reset position back to the base postion



        isOperating = false;
        isVacant = true;
        // position = startPosition;
        currentSpeed = baseSpeed;
        critterPrefab = null;
    }

    // Debugging
    /*
    void CheckDebugging()
    {
        Debug.Log($"{this.name}.CheckDebugging()");

        // New critter
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (isVacant)
            {
                NewCritter(critterPrefab);
            }
            else
            {
                Debug.Log("Not Vacanct");
            }
        }

        // Crush critter
        if (Input.GetKeyDown(KeyCode.C))
        {
            // crush critter
        }

        // Flip isOperating
        if (Input.GetKeyDown(KeyCode.O))
        {
            isVacant = !isOperating;
            Debug.Log($"isOperating: {isOperating}");
        }

        // Flip isVacant
        if (Input.GetKeyDown(KeyCode.V))
        {
            isVacant = !isVacant;
            Debug.Log($"isVacant: {isVacant}");
        }

        // Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }
    */
}