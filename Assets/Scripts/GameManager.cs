using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// creates critters
// sends critters to critter movers
// tracks active critters / critter movers

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CritterMover critterMover;

    public GameObject critterPrefab;
    public GameObject malletPrefab;

    public GameObject[] critterMovers;

    public TextMeshProUGUI tmpCrushed;

    [SerializeField] private string critterTag;
    [SerializeField] private float malletXOffset;

    private int critterHits = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // wherever the players tap lands hammer should hit, so instantiate hammer at tap with offset for swing
    }

    void Update()
    {
        Debug.Log($"{this.name}.Update()");
        tmpCrushed.text = $"CRUSHED: {critterHits}";

        // Get mouse down        
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Perform a 2D raycast from the mouse position towards zero (no direction), store first hit
            RaycastHit2D raycastHit = Physics2D.Raycast(touchPosition, Vector2.zero);

            // Summon a mallet
            SummonMallet(touchPosition);

            // Check if anything was hit, if so, was it a critter
            if (raycastHit.collider != null && raycastHit.collider.gameObject.tag == critterTag)
            {
                CrushCritter(raycastHit);
                critterHits++;                
                //Debug.Log($"Critter hits: {critterHits}");
            }
        }

        // New critter
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (critterMovers[0].GetComponent<CritterMover>().IsVacant)
            {
                Debug.Log(critterMovers[0].GetComponent<CritterMover>().IsVacant);
                critterMovers[0].GetComponent<CritterMover>().NewCritter(critterPrefab);
            }

            //critterMover.NewCritter(critterPrefab);
            //if (isVacant)
            //{
            //    NewCritter(critterPrefab);
            //}
            //else
            //{
            //    Debug.Log("Not Vacanct");
            //}
        }
    }

    private void CrushCritter(RaycastHit2D critterRaycast)
    {
        Debug.Log($"GameManager.CheckCritterHit() hit.name, tag: {critterRaycast.transform.name}, {critterRaycast.collider.gameObject.tag}");
        Critter critter = critterRaycast.transform.GetComponent<Critter>();

        critter.Crush();
    }

    private void SummonMallet(Vector2 worldPoint)
    {   
        Vector2 summonPosition = new Vector2(worldPoint.x + malletXOffset, worldPoint.y);
        GameObject mallet = Instantiate(malletPrefab, summonPosition, Quaternion.identity);
    }

    /*
    private bool IsCritterHit()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

        if (hits.Length > 0)
        {
            // sort game object hits by sorting order, highest (largest) order in layer (sprite renderer) first
            System.Array.Sort(hits, (a, b) =>
            {
                Renderer rendererA = a.collider.gameObject.GetComponent<Renderer>();
                Renderer rendererB = b.collider.gameObject.GetComponent<Renderer>();

                return rendererB.sortingOrder.CompareTo(rendererA.sortingOrder);
            });

            Debug.Log($"GameManager.IsCritterHit() hits[0].name, tag: {hits[0].transform.name}, {hits[0].collider.gameObject.tag}");

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
    */
}
