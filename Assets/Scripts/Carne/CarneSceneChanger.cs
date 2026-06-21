using UnityEngine;
using UnityEngine.SceneManagement;

public class CarneSceneChanger : MonoBehaviour
{
      [Header("Scene Settings")]
      [Tooltip("Type the exact name of the scene you want to load here.")]
      [SerializeField] private string defaultTargetScene;

      public void LoadDefaultScene()
      {
            if (!string.IsNullOrEmpty(defaultTargetScene))
            {
                  SceneManager.LoadScene(defaultTargetScene);
            }
            else
            {
                  Debug.LogWarning("CarneSceneChanger: Default Target Scene is empty!");
            }
      }

      public void LoadGameLevelByIndex(int levelIndex)
      {
            if (CarneScoreManager.Instance != null)
            {
                  CarneScoreManager.Instance.LoadLevelFromMenu(levelIndex);
            }
            else
            {
                  Debug.LogError("ScoreManager not found! Make sure it exists in your scene.");
            }
      }
}
