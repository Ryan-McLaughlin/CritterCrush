using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string critterTag;

    private int critterHits = 0;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        // get mouse down        
        if (Input.GetMouseButtonDown(0))
        {
            // do mallet event

            // check if critter is hit
            if (IsCritterHit())
            {
                critterHits++;
                Debug.Log($"Critter hits: {critterHits}");
            }

        }
    }

    private bool IsCritterHit()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

        if (hits.Length > 0)
        {
            // sort hits by sorting order, highest first
            System.Array.Sort(hits, (x, y) =>
            {
                Renderer rendererX = x.collider.gameObject.GetComponent<Renderer>();
                Renderer rendererY = y.collider.gameObject.GetComponent<Renderer>();

                return rendererY.sortingOrder.CompareTo(rendererX.sortingOrder);
            });

            // check if critter using tag
            if (hits[0].collider.gameObject.tag == critterTag)
            {
                // get the critter script
                Critter critterScript = hits[0].collider.gameObject.GetComponent<Critter>();
                if (critterScript != null)
                {
                    // run critter hit method
                    critterScript.GotHit();
                }

                // critter was hit
                return true;
            }
        }

        // critter was not hit
        return false;
    }
}
