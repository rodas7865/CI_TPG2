using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public void SetDifficultyAndPlay(int level)
    {
        PlayerPrefs.SetInt("GameDifficulty", level);
        PlayerPrefs.Save();
    }
}