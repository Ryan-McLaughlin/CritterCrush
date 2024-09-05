using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Critter : MonoBehaviour
{
    public void GotHit()
    {
        Debug.Log($"{this.name} has been hit!");
    }
}
