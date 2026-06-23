using UnityEngine;

public class PuzzleSettings : MonoBehaviour
{
    public int piecesToWin;

    void Start()
    {
        GameManager.Reset();
        GameManager.piecesToWin = piecesToWin;
    }
}