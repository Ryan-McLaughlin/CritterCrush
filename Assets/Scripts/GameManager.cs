using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] critterPrefab;
    public GameObject malletPrefab;

    public GameObject[] critterMovers;

    public bool isPaused = false;

    [SerializeField] TextMeshProUGUI textCrushes;
    [SerializeField] TextMeshProUGUI textMisses;
    [SerializeField] TextMeshProUGUI textEscapes;

    [SerializeField] private string critterTag;
    [SerializeField] private float malletXOffset;

    [SerializeField] private float newCritterAlarm;
    [SerializeField] private float newCritterAlarmOffset;
    float newCritterTimer;

    private int crittersSummoned = 0;

    private int crushCounter = 0;
    private int crushComboCounter = 0;
    private int bestCrushCombo = 0;
    private int missCounter = 0;
    private int missComboCounter = 0;
    private int bestMissCombo = 0;
    private int escapeCounter = 0;
    private int escapeComboCounter = 0;
    private int bestEscapeCombo = 0;

    //Dropdown dropdownCritter;
    [SerializeField] bool debugging = true;

    void Awake()
    {
        //DebugPanel();

        if(Instance == null)
        {
            Instance = this;
        }
    }

    /*
    void DebugPanel()
    {
        List<string> names = new List<string>();
        //dropdownCritter = FindObjectOfType<Dropdown>();
        //dropdownCritter = FindObjectOfType<Dropdown>(name: "Dropdown Critter");
        dropdownCritter = GameObject.Find("Dropdown Critter").GetComponent<Dropdown>();

        for (int i = 0; i < critterPrefab.Length;)
        {
            names[i] = critterPrefab[i].name;            
        }
        
        dropdownCritter.ClearOptions();
        dropdownCritter.AddOptions(names);
    }
    */

    void Update()
    {
        // Pause
        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        // Check if paused
        if(isPaused) { Time.timeScale = 0f; return; }
        else { Time.timeScale = 1f; }

        // Update GUI
        textCrushes.text = $"Crushes: {crushCounter}\n"
                         + $"Crush Combo: {crushComboCounter}\n"
                         + $"Best Crush Combo: {bestCrushCombo}";

        textMisses.text = $"Misses: {missCounter}\n"
                        + $"Miss Combo: {missComboCounter}\n"
                        + $"Best Miss Combo: {bestMissCombo}";

        textEscapes.text = $"Escapes: {escapeCounter}\n"
                         + $"Escape Combo: {escapeComboCounter}\n"
                         + $"Best Escape Combo: {bestEscapeCombo}";

        // Summon a mallet at the mouse position
        if(Input.GetMouseButtonDown(0))
        {
            // TODO: check if missed
            SummonMallet(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        newCritterTimer += Time.deltaTime;
        // Time for new critter
        if(newCritterTimer >= newCritterAlarm)
        {
            int rand = Random.Range(0, critterPrefab.Length);
            NewCritterAtRandomCM(critterPrefab[rand]);
            newCritterTimer = 0;
        }

        #region Debugging Region
        /* *********************************************** //
        // **                 DEBUGGING                 ** //
        // *********************************************** */
        if(debugging)
        {
            // Get a random critter prefab choice
            int rand = Random.Range(0, critterPrefab.Length);

            // New critter at cm 0-9
            for(int i = 0; i <= 9; i++)
            {
                KeyCode key = (KeyCode)(KeyCode.Alpha0 + i);
                if(Input.GetKeyDown(key))
                {
                    NewCritterAtCM(critterMovers[i], critterPrefab[rand]);
                }
            }

            // New critter at random critter mover
            if(Input.GetKeyDown(KeyCode.N))
            {
                NewCritterAtRandomCM(critterPrefab[rand]);
            }

            // New critter for all critter movers
            if(Input.GetKeyDown(KeyCode.A))
            {
                foreach(GameObject cm in critterMovers)
                {
                    rand = Random.Range(0, critterPrefab.Length);
                    NewCritterAtCM(cm, critterPrefab[rand]);
                }
                //NewCritterAllCMs(critterPrefab[rand]);
            }

            // Reset counters
            if(Input.GetKeyDown(KeyCode.R))
            {
                crushCounter = 0;
                crushComboCounter = 0;
                missCounter = 0;
                missComboCounter = 0;
                escapeCounter = 0;
                escapeComboCounter = 0;
            }
        }
        #endregion Debugging
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
        else { MalletMissed(); }
    }

    /// <summary>
    /// Attempts to instantiate a new critter at a random vacant mover position.
    /// </summary>
    public void NewCritterAtRandomCM(GameObject critter)
    {

        int rand = Random.Range(0, critterMovers.Length);
        if(critterMovers[rand].GetComponent<CritterMover>().IsVacant)
        {
            // Send critter mover critter prefab, and its summoned number (id)
            if(critterMovers[rand].GetComponent<CritterMover>().NewCritter(critter, crittersSummoned.ToString()))
            {
                crittersSummoned++;
            }
        }
    }

    /// <summary>
    /// Attempts to summon a new critter at the specified CritterMover component.
    /// </summary>
    /// <param name="cm">The CritterMover component representing the location to summon the critter.</param>
    /// <param name="critter">The GameObject prefab of the critter to be summoned.</param>
    /// <returns>True if the critter was successfully summoned, false otherwise.</returns>
    /// <remarks>
    /// This method calls the `NewCritter` function on the provided CritterMover component, passing the critter prefab and a string representation of the current number of summoned critters.
    /// If the NewCritter function returns true, the number of summoned critters is incremented. Otherwise, a debug message is logged indicating the failure.
    /// </remarks>
    public void NewCritterAtCM(GameObject cm, GameObject critter)
    {
        // Send critter mover critter prefab, and its summoned number (id)
        if(cm.GetComponent<CritterMover>().NewCritter(critter, crittersSummoned.ToString()))
        {
            crittersSummoned++;
        }
        else { Debug.Log($"Could not summon {critter.name} {crittersSummoned} at {cm.name}"); }
    }

    /*
    /// <summary>
    /// Attempts to instantiate a new critter at every vacant mover position.
    /// </summary>
    public void NewCritterAllCMs(GameObject critter)
    {
        foreach(GameObject cm in critterMovers)
        {
            // Send critter mover critter prefab, and its summoned number (id)
            if(cm.GetComponent<CritterMover>().NewCritter(critter, crittersSummoned.ToString()))
            {
                crittersSummoned++;
            }
        }
    }
    */
    private void MalletMissed()
    {
        missCounter++;
        missComboCounter++;

        // Reset the crush combo counter
        crushComboCounter = 0;

        // Update best combo
        if(missComboCounter > bestMissCombo)
        {
            bestMissCombo = missComboCounter;
        }
    }

    /// <summary>
    /// Handles a critter collision by logging information and calling the critter's Crush() method.
    /// </summary>
    /// <param name="collider">The Collider2D component representing the collided critter.</param>
    /// <remarks>
    /// Increments the crush counter and crush combo counter. Resets the escape combo counter.
    /// Updates the highest crush counter if necessary. Calls the `Crush` method on the critter to initiate the crushing animation.
    /// </remarks>
    private void CrushCritter(Collider2D collider)
    {
        //Debug.Log($"GameManager.CrushCritter(): {collider.transform.name}");

        Critter critter = collider.GetComponent<Critter>();

        // Count the crush
        crushCounter++;
        crushComboCounter++;

        // Reset the escape combo
        escapeComboCounter = 0;

        // Reset the miss combo counter
        missComboCounter = 0;

        // Update best combo
        if(crushComboCounter > bestCrushCombo)
        {
            bestCrushCombo = crushComboCounter;
        }

        critter.Crush();
    }

    /// <summary>
    /// Called when a critter escapes. Called by the escaping critter.
    /// </summary>
    /// <param name="critter">The GameObject representing the escaped critter.</param>
    public void CritterEscaped()//string critterName)
    {
        // Count the escape
        escapeCounter++;
        escapeComboCounter++;

        // Reset the crush combo
        crushComboCounter = 0;

        // Update best combo
        if(escapeComboCounter > bestEscapeCombo)
        {
            bestEscapeCombo = escapeComboCounter;
        }
    }

}
