using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastClickHandler : MonoBehaviour
{    
    public string critterTag;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                    // get the game objects script
                    Critter critterScript = hits[0].collider.gameObject.GetComponent<Critter>();
                    if (critterScript != null)
                    {
                        // run critter hit method
                        critterScript.GotHit();
                    }
                }
            }
        }
    }
}
