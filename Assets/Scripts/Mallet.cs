using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEngine;

public class Mallet : MonoBehaviour
{
    public float rotationAngle = 90f;
    public float rotationTime = 0.5f;

    private Quaternion initialRotation;

    private bool rotateObjectOverTimeFinished = false;

    private void Start()
    {
        initialRotation = transform.rotation;
        StartCoroutine(RotateObjectOverTime());
        //Swing();
    }

    IEnumerator RotateObjectOverTime()
    {
        // rotate to target rotation
        float elapsedTime = 0f;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, rotationAngle);

        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationTime);
            rotateObjectOverTimeFinished = true;
            yield return null;
        }

        // rotate back to initial rotation
        //elapsedTime = 0f;
        //targetRotation = initialRotation;

        //while (elapsedTime < rotationTime)
        //{
        //    elapsedTime += Time.deltaTime;
        //    transform.rotation = Quaternion.Slerp(targetRotation, initialRotation, elapsedTime / rotationTime);
        //    yield return null;
        //}
    }

    private void Update()
    {
        if (rotateObjectOverTimeFinished)
        {
//            Debug.Log("rotateObjectOverTimeFinished finished");
        }
    }

    public void Swing()
    {
        //float elapsedTime = 0f;
        //Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, rotationAngle);

        //while (elapsedTime < rotationTime)
        //{
        //    elapsedTime += Time.deltaTime;
        //    transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationTime);
        //}

        //// isSwingFinished = true;
    }

    private void FaceCritter()
    {

    }

    private void Rotate90()
    {

    }
}
