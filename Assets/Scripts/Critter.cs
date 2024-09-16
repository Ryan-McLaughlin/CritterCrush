using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Critter : MonoBehaviour
{
    // maybe rename to DeadFishCritter, the critter mover handles movement
    // have ActiveCritter move itself

    // public GameObject gameObject;
    public int health;

    public void GotHit()
    {
        Debug.Log($"{this.name} has been hit!");
    }

    public void Crush()
    {
        // summon the poof animation
        // delete this
        Debug.Log($"Critter.Crush(): {this.name} has been Crushed!");
        Destroy(this.gameObject);
    }
}
