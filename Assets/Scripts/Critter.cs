using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Critter : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Crush()
    {
        // summon the poof animation
        Debug.Log($"Critter.Crush(): {this.name} has been Crushed!");
        // tell GM it was crushed?
        Destroy(this.gameObject);
    }

    public void Escape()
    {
        Debug.Log($"Critter.Escape(): {this.name} has Escaped!");

        gameManager.CritterEscaped();// this.name );
        Destroy(this.gameObject);
    }
}
