using UnityEngine.SceneManagement;
public static class GameManager
{
 private static int _rightAnswers = 0;
 private static int _wrongAnswers = 0;
 public static void IncrementRightAnswer()
 {
 _rightAnswers++;
 if (_rightAnswers == 9)
 {
 SceneManager.LoadSceneAsync("cenaFinal",LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("cenaJogo 1");
            SceneManager.UnloadSceneAsync("cenaJogo 2");
            SceneManager.UnloadSceneAsync("cenaJogo 3");
        }
 } 
 public static void IncrementWrongAnswer()
 {
 _wrongAnswers++;
 }
 public static int GetRightAnswer()
 {
 return _rightAnswers;
 }
 public static int GetWrongAnswer()
 {
 return _wrongAnswers;
 }
 public static void Reset()
 {
 _rightAnswers = 0;
 _wrongAnswers = 0;
 }
} 