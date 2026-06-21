using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    // This method will be linked to your Easy, Medium, and Hard buttons
    public void SetDifficultyAndPlay(int level)
    {
        // 1. Save the chosen difficulty to Unity's memory
        PlayerPrefs.SetInt("GameDifficulty", level);
        PlayerPrefs.Save(); // Ensure it saves immediately

        SceneManager.LoadScene("Game"); 
    }
}