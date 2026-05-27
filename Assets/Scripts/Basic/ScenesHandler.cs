using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    public static void ChangeScene(string sceneName)//background handle missing TODO
    {
        SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
    }

    public static void PreviousScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.sceneCount);
    }

}
