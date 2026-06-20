
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DificultySelecter : MonoBehaviour
{
    [SerializeField]
    private MainGame.Dificulty dificulty;

    [SerializeField]
    private Image image;

    public void SelectDificulty()
    {
        MainGame.currentDificulty = dificulty;
    }
}
