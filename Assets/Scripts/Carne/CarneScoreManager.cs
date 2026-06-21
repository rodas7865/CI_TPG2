using UnityEngine;

public class CarneScoreManager : MonoBehaviour
{
      public static CarneScoreManager Instance { get; private set; }

      private int score;
      private float levelTime;
      private int currentLevelIndex;

      public int Score => score;
      public float LevelTime => levelTime;
      public int CurrentLevelIndex => currentLevelIndex;

      public bool IsWin { get; private set; }

      public void SetWinState(bool won)
      {
            IsWin = won;
      }

      private void Awake()
      {
            if (Instance == null)
            {
                  Instance = this;
                  DontDestroyOnLoad(gameObject);
                  ResetGame();
            }
            else
            {
                  Destroy(gameObject);
            }
      }

      public void ResetGame()
      {
            score = 0;
            currentLevelIndex = 0;
            ResetLevelStats();
      }

      public void ResetLevelStats()
      {
            levelTime = 0f;
      }

      private void Update()
      {
            levelTime += Time.deltaTime;
      }

      public void SetScore(int amount)
      {
            score = amount;
      }

      public void SetLevelIndex(int index)
      {
            currentLevelIndex = index;
      }

      public void LoadLevelFromMenu(int levelIndex)
      {
            currentLevelIndex = levelIndex;
            ResetLevelStats();
            UnityEngine.SceneManagement.SceneManager.LoadScene("CarneLevel1");
      }
}
