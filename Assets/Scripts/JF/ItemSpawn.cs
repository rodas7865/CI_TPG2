using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    // Right boundary of the spawn area
    public GameObject RightSide;

    // List of prefabs to spawn (e.g. fish, bombs, etc.)
    public GameObject[] items;

    // The Canvas (or a panel inside it) that will hold the spawned items
    public Transform canvasParent;

    // Delay before the first spawn and interval between the rest
    public float startDelay, repeatRate;

    // --- Difficulty & Gravity Settings ---
    [Header("Difficulty Settings")]
    private int currentDifficulty; // Made private because we read it from PlayerPrefs now
    
    public float easyGravity = 10f;
    public float mediumGravity = 15f;
    public float hardGravity = 22f;

    void Start()
    {
        // --- NEW: Read the difficulty chosen in the Menu Scene ---
        // If it can't find a saved difficulty, it defaults to 1 (Easy)
        currentDifficulty = PlayerPrefs.GetInt("GameDifficulty", 1);

        // Periodically call Spawn() at the specified interval
        InvokeRepeating("Spawn", startDelay, repeatRate);
    }

    void Spawn()
    {
        // Random position on X between the spawner and RightSide
        Vector3 pos = new Vector3(
            Random.Range(transform.position.x, RightSide.transform.position.x),
            transform.position.y,
            0
        );

        // 1. Instantiate the item and save it to a variable
        GameObject spawnedItem = Instantiate(items[Random.Range(0, items.Length)], pos, transform.rotation, canvasParent);

        // 2. Get the Rigidbody2D component to change its gravity
        Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

        // 3. Apply the correct gravity scale based on the saved difficulty
        if (rb != null)
        {
            if (currentDifficulty == 1)
            {
                rb.gravityScale = easyGravity;
            }
            else if (currentDifficulty == 2)
            {
                rb.gravityScale = mediumGravity;
            }
            else if (currentDifficulty == 3)
            {
                rb.gravityScale = hardGravity;
            }
        }
    }
}