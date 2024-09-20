using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Critter : MonoBehaviour
{
    GameManager gameManager;

    CircleCollider2D circleCollider;
    SpriteRenderer spriteRenderer;
    float spriteRadius;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRadius = Mathf.Max(spriteRenderer.sprite.bounds.extents.x, spriteRenderer.sprite.bounds.extents.y);
        
        circleCollider.radius = spriteRadius;
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
