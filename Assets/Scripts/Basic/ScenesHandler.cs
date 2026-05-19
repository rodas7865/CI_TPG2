using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    public static int currentSceneDepth { private set; get; } = 1;
    //private static string activeScene = "";

    public static void ChangeScene(string sceneName)//background handle missing TODO
    {/*

        if (sceneDepth > 3 || sceneDepth < 1)
        {
            Debug.LogError("Scene depth must be between 1 and 3"); return;
        }

        currentSceneDepth = sceneDepth;

        //if(activeScene!=string.Empty)
        //    SceneManager.UnloadSceneAsync(activeScene);
        */

        SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

        //activeScene = sceneName;
    }



}
