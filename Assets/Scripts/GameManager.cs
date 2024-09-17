using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    //public CritterMover critterMover;

    public GameObject critterPrefab;
    public GameObject malletPrefab;

    public GameObject[] critterMovers;

    public TextMeshProUGUI tmpCrushed;

    [SerializeField] private string critterTag;
    [SerializeField] private float malletXOffset;

    private int critterCrushed = 0;
    private int crittersSummoned = 0;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        tmpCrushed.text = $"CRUSHED: {critterCrushed}";

        // New critter
        if(Input.GetKeyDown(KeyCode.N))
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

        // Get mouse down        
        if(Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Summon a mallet
            SummonMallet(mousePos);

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
                critterCrushed++;
            }
        }
    }

    private void CrushCritter(Collider2D collider)
    {
        Debug.Log($"GameManager.CheckCritterHit() hit.name, tag: {collider.transform.name}, {collider.gameObject.tag}");
        Critter critter = collider.GetComponent<Critter>();
        critter.Crush();
    }

    private void SummonMallet(Vector2 worldPoint)
    {
        Vector2 summonPosition = new Vector2(worldPoint.x + malletXOffset, worldPoint.y);
        GameObject mallet = Instantiate(malletPrefab, summonPosition, Quaternion.identity);
    }
}
