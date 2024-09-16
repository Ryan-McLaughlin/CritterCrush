using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterMover : MonoBehaviour
{
    [SerializeField] private float timeToWait;
    private float timeWaited;

    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool up;
    [SerializeField] private float upOffset;
    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool down;
    [SerializeField] private float downOffset;
    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool left;
    [SerializeField] private float leftOffset;
    [Tooltip("True to allow movement in this direction")]
    [SerializeField] private bool right;
    [SerializeField] private float rightOffset;

    private bool[] directionsAllowed = new bool[4];
    private bool isVacant;
    public bool IsVacant { get => isVacant; set => isVacant = value; }

    private GameObject critter;

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

    private void Awake()
    {
        directionsAllowed[0] = up;
        directionsAllowed[1] = down;
        directionsAllowed[2] = left;
        directionsAllowed[3] = right;

        timeWaited = 0f;

        isVacant = true;
    }

    private void Update()
    {

        if (timeWaited >= timeToWait)
        {
            bool directionOk = false;
            while (!directionOk)
            {
                int roll = Random.Range(0, 4);

                if (directionsAllowed[roll])
                {
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

                    directionOk = true;
                }
            }

            timeWaited = 0;
        }
        else
        {
            timeWaited += Time.deltaTime;
        }
        //Debug.Log($"CritterMover.Update() - timeWated: {timeWaited}");

    }


    private void MoveCritter(float x, float y)
    {

    }

    private void ChooseDirection()
    {
        Debug.Log($"CritterMover.ChooseDirection()");
    }
}
