using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Runtime : MonoBehaviour
{
    [SerializeField]
    private GameObject gameScreen;

    [SerializeField]
    private GameObject resultsScreen;

    [SerializeField]
    private GameObject letters;

    [SerializeField]
    private GameObject letterPrefab;

    [SerializeField]
    private Image blender;

    [SerializeField]
    private Sprite[] blenderSprites;

    [SerializeField]
    private TextMeshProUGUI wordsText;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI resultWordsText;

    private List<string> words = new();
    private string currentWord;
    private int maxWords;

    private int mistakes = 0;

    private Dictionary<GameObject, string> bingo = new();
    private List<GameObject> toClear = new();

    private bool awaitingKey = false;

    private float time;
    private bool countingTime;

    private float timer;
    private bool countingTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (awaitingKey)
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
                return;

            List<GameObject> temp = new List<GameObject>();

            if (keyboard.anyKey.wasReleasedThisFrame)
            {
                bool isMistake = true;

                foreach (KeyValuePair<GameObject, string> pair in bingo)
                {
                    string letter = pair.Value.ToLower();
                    KeyControl key = keyboard.FindKeyOnCurrentKeyboardLayout(pair.Value);

                    if (key == null)
                        return;

                    if (key.wasReleasedThisFrame)
                    {
                        TextMeshProUGUI text = pair.Key.GetComponentInChildren<TextMeshProUGUI>();
                        text.enabled = true;

                        temp.Add(pair.Key);

                        isMistake = false;
                    }
                }

                if (isMistake)
                    MakeMistake();
            }

            foreach (GameObject item in temp)
            {
                bingo.Remove(item);
                toClear.Add(item);
            }

            if (bingo.Count <= 0)
            {
                words.Remove(currentWord);

                UpdateCounter();
                ClearLetters();
                ClearBingo();

                awaitingKey = false;
                SpawnWord();
            }
        }

        if (countingTime)
        {
            UpdateTimeCounter();
        }

        if (countingTimer)
        {
            UpdateTimerCounter();
        }
    }

    public void Setup()
    {
        countingTime = true;
        
        resultsScreen.SetActive(false);
        gameScreen.SetActive(true);

        ClearLetters();
        ClearBingo();

        words = new List<string>(MainGame.dificultyWords[MainGame.currentDificulty]);
        maxWords = words.Count;

        mistakes = -1;
        time = 0;

        MakeMistake() ;
        UpdateCounter();
        SpawnWord();
    }

    private void UpdateCounter()
    {
        wordsText.text = maxWords - words.Count + "/" + maxWords.ToString();
        resultWordsText.text = "Palavras: " + (maxWords - words.Count) + "/" + maxWords.ToString();
    }

    private void SpawnWord()
    {
        switch (MainGame.currentDificulty)
        {
            case MainGame.Dificulty.Easy:
                countingTimer = false;
                break;
            case MainGame.Dificulty.Moderate:
                countingTimer = true;
                timer = 30f;
                break;
            case MainGame.Dificulty.Hard:
                countingTimer = true;
                timer = 10f;
                break;
        }


        if (words.Count == 0)
        {
            ShowResults();
            return;
        }

        int rand = Random.Range(0,words.Count-1);

        currentWord = words[rand];

        Debug.Log(currentWord);

        foreach (char c in currentWord)
        {
            Debug.Log(c);
            Debug.Log(c.ToString());
            bingo.Add(SpawnLetter(c.ToString()), c.ToString());
        }

        awaitingKey = true;
    }

    private GameObject SpawnLetter(string letter)
    {
        GameObject letterInstance = Instantiate(letterPrefab);
        TextMeshProUGUI text = letterInstance.GetComponentInChildren<TextMeshProUGUI>();
        text.text = letter;
        text.enabled = false;

        letterInstance.transform.SetParent(letters.transform, false);
        return letterInstance;
    }

    private void ClearLetters()
    {
        foreach (var item in toClear)
        {
            Destroy(item);
        }
    }

    private void ClearBingo()
    {
        foreach (KeyValuePair<GameObject,string> item in bingo)
        {
            Destroy(item.Key);
        }

        bingo.Clear();
    }

    private void MakeMistake()
    {
        mistakes++;

        Debug.Log(mistakes);

        if (mistakes >= 6)
        {
            ShowResults();
            return;
        }

        blender.sprite = blenderSprites[mistakes];

        
    }

    private void ShowResults()
    {
        gameScreen.SetActive(false);
        resultsScreen.SetActive(true);

        UpdateTimeCounter();
        UpdateCounter();


        countingTime = false;
    }

    private void UpdateTimeCounter()
    {
        if (!countingTime)
            return;

        time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = "Tempo: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void UpdateTimerCounter()
    {
        if (!countingTimer)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            countingTimer = false;

            ShowResults();
            return;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
