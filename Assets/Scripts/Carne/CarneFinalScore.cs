using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarneFinalScore : MonoBehaviour
{
      [Header("UI Text References")]
      [SerializeField] private TMP_Text titleText;
      [SerializeField] private TMP_Text finalScoreText;

      private void Start()
      {
            if (CarneScoreManager.Instance != null)
            {
                  if (titleText != null)
                  {
                        titleText.text = CarneScoreManager.Instance.IsWin ? "Parabéns!" : "Jogo terminou!";
                  }

                  if (finalScoreText != null)
                  {
                        finalScoreText.text = $"Pontuação: {CarneScoreManager.Instance.Score}";
                  }
            }
            else
            {
                  if (finalScoreText != null)
                  {
                        finalScoreText.text = "Pontuação: 0";
                  }
            }
      }

      public void RestartGame()
      {
            if (CarneScoreManager.Instance != null)
            {
                  CarneScoreManager.Instance.ResetLevelStats();
            }

            SceneManager.LoadScene("CarneLevel1");
      }

}
