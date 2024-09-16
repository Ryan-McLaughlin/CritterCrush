using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Timers;
using UnityEngine;
//using System.Threading.Tasks;

public class Mallet : MonoBehaviour
{
    public float swingAngle;
    public float swingTime;
    public float timeToWait;

    private Quaternion initialRotation;

    private float waitTimer = 0f;

    private bool isSwingComplete = false;

    private void Start()
    {
        initialRotation = transform.rotation;
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        float swingTimer = 0f;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, swingAngle);

        while (swingTimer < swingTime)
        {
            // Rotate the mallet
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, (swingTimer / swingTime));
            swingTimer += Time.deltaTime;

            // Pause execution of the coroutine and yield control back to main thread
            yield return null;
        }

        isSwingComplete = true;
    }

    private void Update()
    {
        // Once swing is complete, wait, then destroy
        if (isSwingComplete)
        {
            waitTimer += Time.deltaTime;

            Debug.Log($"waitTimer: {waitTimer} / {timeToWait}");
            if (waitTimer >= timeToWait)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
