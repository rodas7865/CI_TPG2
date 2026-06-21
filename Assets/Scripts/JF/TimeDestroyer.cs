using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{
    // How long the object stays in the scene
    public float aliveTimer = 5f;
    private float timer;

    void Start()
    {
        timer = aliveTimer;
    }

    void Update()
    {
        // Countdown every frame
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // If a "Good" item hits the floor/times out, the player missed it
            if (gameObject.CompareTag("Good") && ItemChecker.Instance != null)
            {
                ItemChecker.Instance.LoseLife();
            }

            // Remove the object from the scene
            Destroy(gameObject);
        }
    }
}