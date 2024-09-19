using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    public GameObject critterPrefab;
    public GameObject malletPrefab;

    public GameObject[] critterMovers;

    [SerializeField] TextMeshProUGUI textCrushes;
    [SerializeField] TextMeshProUGUI textMisses;
    [SerializeField] TextMeshProUGUI textEscapes;

    [SerializeField] private string critterTag;
    [SerializeField] private float malletXOffset;

    private int crittersSummoned = 0;

    private int crushCounter = 0;
    private int crushComboCounter = 0;
    private int missCounter = 0;
    private int missComboCounter = 0;
    private int escapeCounter = 0;
    private int escapeComboCounter = 0;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        // Update GUI
        textCrushes.text = $"Crushes: {crushCounter}\n" + $"Crush Combo: {crushComboCounter}";
        textMisses.text = $"Misses: {missCounter}\n" + $"Miss Combo: {missComboCounter}";
        textEscapes.text = $"Escapes: {escapeCounter}\n" + $"Escape Combo: {escapeComboCounter}";

        // New critter
        if(Input.GetKeyDown(KeyCode.N))
        {
            NewCritter();
        }

        // New critter for all
        if(Input.GetKeyDown(KeyCode.A))
        {
            NewCritterAll();
        }

        // Summon a mallet at the mouse position
        if(Input.GetMouseButtonDown(0))
        {
            // TODO: check if missed
            SummonMallet(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    /// <summary>
    /// Instantiates a mallet prefab at a specified world position, considering the topmost collider at that point.
    /// </summary>
    /// <param name="worldPoint">The world position where the mallet should be summoned.</param>
    private void SummonMallet(Vector2 worldPoint)
    {
        // Get the mouse position in world coordinates
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 summonPosition = new Vector2(worldPoint.x + malletXOffset, worldPoint.y);
        GameObject mallet = Instantiate(malletPrefab, summonPosition, Quaternion.identity);

        // Get all colliders at the mouse position
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos);

        // Find the collider with the highest sorting order
        Collider2D topmostCollider = null;
        int topmostSortingOrder = int.MinValue;
        foreach(Collider2D collider in colliders)
        {
            SpriteRenderer spriteRenderer = collider.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null && spriteRenderer.sortingOrder > topmostSortingOrder)
            {
                topmostCollider = collider;
                topmostSortingOrder = spriteRenderer.sortingOrder;
            }
        }

        // If a topmost collider was found and topmost collider is a critter
        if(topmostCollider != null && topmostCollider.gameObject.tag == critterTag)
        {
            CrushCritter(topmostCollider);
        }
    }

    /// <summary>
    /// Attempts to instantiate a new critter at a random vacant mover position.
    /// </summary>
    private void NewCritter()
    {

        int rand = Random.Range(0, critterMovers.Length);
        if(critterMovers[rand].GetComponent<CritterMover>().IsVacant)
        {
            if(critterMovers[rand].GetComponent<CritterMover>().NewCritter(critterPrefab, crittersSummoned.ToString()))
            {
                crittersSummoned++;
            }
        }
    }

    /// <summary>
    /// Attempts to instantiate a new critter at every vacant mover position.
    /// </summary>
    private void NewCritterAll()
    {
        foreach(GameObject cm in critterMovers)
        {
            if(cm.GetComponent<CritterMover>().NewCritter(critterPrefab, crittersSummoned.ToString()))
            {
                crittersSummoned++;
            }
        }
    }

    /// <summary>
    /// Handles a critter collision by logging information and calling the critter's Crush() method.
    /// </summary>
    /// <param name="collider">The Collider2D component representing the collided critter.</param>
    private void CrushCritter(Collider2D collider)
    {
        Debug.Log($"GameManager.CrushCritter(): {collider.transform.name}");
        
        Critter critter = collider.GetComponent<Critter>();
        
        crushCounter++;
        critter.Crush();
    }

    /// <summary>
    /// Called when a critter escapes. Called by the escaping critter.
    /// </summary>
    /// <param name="critter">The GameObject representing the escaped critter.</param>
    public void CritterEscaped()//string critterName)
    {
        //Debug.Log($"GM.CritterEscaped(): {critterName}");
                
        escapeCounter++;
    }

}
