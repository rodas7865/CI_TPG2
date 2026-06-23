using UnityEngine;
using TMPro;
using UnityEngine.UI; // REQUIRED: For manipulating UI Images
using UnityEngine.SceneManagement; // REQUIRED: For changing scenes

public class ItemChecker : MonoBehaviour
{
    // Static instance allowing other scripts (like TimeDestroyer) to access this easily
    public static ItemChecker Instance;

    [Header("Score Settings")]
    public int score;
    public GameObject scoreTextObject;
    private TMP_Text tmpText;

    [Header("Lives & UI Settings")]
    public int lives = 3; // Player starts with 3 lives
    
    // Drag your 3 Heart Image components from the hierarchy into this array
    public Image[] heartImages; 
    
    // Drag your Red and Grey heart sprites here from your Assets folder
    public Sprite redHeart;
    public Sprite greyHeart;

    [Header("Audio Settings")]
    public AudioClip okSound;
    public AudioClip boomSound;
    private AudioSource audioSource;

    void Awake()
    {
        // Establish the Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        tmpText = scoreTextObject.GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        // Ensure the hearts visually match our starting lives
        UpdateHeartsUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If we caught an object tagged "Good"
        if (other.gameObject.CompareTag("Good"))
        {
            audioSource.PlayOneShot(okSound);
            score += 10;
            Destroy(other.gameObject);
        }
        // If we caught an object tagged "Bad"
        else if (other.gameObject.CompareTag("Bad"))
        {
            audioSource.PlayOneShot(boomSound);
            score -= 10;
            LoseLife(); // Penalty: Lose a life for catching a bad object
            Destroy(other.gameObject);
        }

        // Update the text on the Canvas to show the current score
        tmpText.text = score.ToString();
    }

    // Public method that can be called internally or by TimeDestroyer
    public void LoseLife()
    {
        lives--;
        UpdateHeartsUI();

        // Check if game over condition is met
        if (lives <= 0)
        {
            SceneManager.LoadSceneAsync("GameOver",LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Game");
        }
    }

    // Method to change heart colors based on remaining lives
    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < lives)
            {
                heartImages[i].sprite = redHeart; // Keep it red
            }
            else
            {
                heartImages[i].sprite = greyHeart; // Turn it grey
            }
        }
    }
}