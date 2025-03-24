
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public string[] wordList;
    public static string currentWord;

    private static int _streak = 0;
    public static int row;
    public int basePointGained;

    private float _timer;
    public float maxTime;
    public float timePointMulti;
    public float rowPointMulti;
    public static float score = 0;
    public static float highScore = 0;

    public Color redCol;
    public Color yellowCol;
    public Color greenCol;

    public AudioClip winSound;
    public AudioClip loseSound;

    private AudioSource _audio;

    private bool _gameOff;
    public static bool win;

    public static float baseTime;
    public static float totalTimer;

    private GameObject[] _lettersInRow;
    public GameObject TXTInput;
    public GameObject WordManager;

    public Canvas GameUICanvas;
    public Canvas InstructUICanvas;

    private TMP_InputField guessInput;

    public TextMeshProUGUI timerDisp;
    public TextMeshProUGUI noticeDisplay;

    private TextMeshPro[] _txtInRow;
    // Start is called before the first frame update
    void Start()
    {
        _gameOff = true;
        score = 0;
        _audio = GetComponent<AudioSource>();
        GameUICanvas = GameUICanvas.GetComponent<Canvas>();
        InstructUICanvas = InstructUICanvas.GetComponent<Canvas>();
        guessInput = TXTInput.GetComponentInChildren<TMP_InputField>();
        GameUICanvas.enabled = false;
        if (_streak != 0)
        {
            StartGame();
        }
        //lettersInRow[0].text = "I";
    }

    void Update()
    {
        EventSystem.current.SetSelectedGameObject(TXTInput.gameObject);
        if (!_gameOff)
        {

            for (int i = 0; i < 5; i++)
            {
                if (i < guessInput.text.Length)
                {
                    _txtInRow[i].text = guessInput.text.Substring(i, 1).ToUpper();
                }
                else
                {
                    _txtInRow[i].text = "";
                }
            }
            _timer += Time.deltaTime;
            if (_timer > 1)
            {
                totalTimer--;
                _timer = 0;
            }
            timerDisp.text = "Time Left: " + totalTimer + "s";
            if (totalTimer <= 0)
            {
                _gameOff = true;
                StartCoroutine(Notice(0));
            }
            else if(totalTimer == 30)
            {
                timerDisp.color = Color.red;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log(guessInput.text.Length);
                //Debug.Log(inputTxt.text);
                if (guessInput.text.Length == 5)
                {
                    CheckGuess();

                }
                else
                {
                    //Debug.Log("Didn't");
                    //Add an insuficient word length notification
                }
            }
        }
    }
    // Update is called once per frame

    IEnumerator Notice(int greens)
    {
        if (greens == 5){
            noticeDisplay.text = "Good Job!";
            win = true;
            score += basePointGained * (totalTimer * timePointMulti) * (row  * rowPointMulti);
            if (score > highScore)
            {
                highScore = score;
            }
            _audio.clip = winSound;
        }
        else if (totalTimer <= 0)
        {
            noticeDisplay.text = "Times Up!";
            _streak = 0;
            _audio.clip = loseSound;
        }
        else if(row == 0)
        {
            noticeDisplay.text = "Out Of Tries!";
            _streak = 0;
            _audio.clip = loseSound;
        }
        _audio.Play();
        yield return new WaitForSeconds(2);
            SceneManager.LoadScene("ResultsScreen");
    }

    string GenWord()
    {
        return wordList[Random.Range(0, wordList.Length)];
    }

    GameObject[] FindLetters()
    {
        GameObject[] output = {null, null, null, null, null};
        for (int i = 4; i >= 0; i--)
        {
            //txtInput.text;
            //lettersInRow[i] = GameObject.Find("Letter_" + i + "_" + row);
            output[i] = GameObject.Find("Letter_" + i + "_" + row);
        }
        return output;
    }

    TextMeshPro[] FindTxt()
    {
        TextMeshPro[] output = { null, null, null, null, null };
        for (int i = 4; i >= 0; i--)
        {
            //txtInput.text;
            //lettersInRow[i] = GameObject.Find("Letter_" + i + "_" + row);
           output[i] = _lettersInRow[i].GetComponentInChildren<TextMeshPro>();
        }
        return output;
    }

    void CheckGuess()
    {
        int greens = 0;
        string foundLetters = "";
        /*
         * 1st. Check if the letters in both the currentWord and the guess maatch at the same positon 
         * 2nd. Check to see if the letters in the guess that didn't math directly are included in the currentWord
         * 3rd. Mark every other letter as wrong
         */

        for (int i = 0; i < 5; i++)
        {
            if (guessInput.text.ToLower()[i] == currentWord.ToLower()[i])
            {
                _lettersInRow[i].GetComponent<SpriteRenderer>().color = greenCol;
                greens++;
                if(foundLetters.IndexOf(guessInput.text.ToLower()[i]) >= 0)
                {
                    _lettersInRow[foundLetters.IndexOf(guessInput.text.ToLower()[i])].GetComponent<SpriteRenderer>().color = redCol;
                }
                foundLetters += guessInput.text.ToLower()[i];
            }
            else if (currentWord.ToLower().IndexOf(guessInput.text.ToLower()[i]) > -1 && foundLetters.IndexOf(guessInput.text.ToLower()[i]) < 0)
            {
                    _lettersInRow[i].GetComponent<SpriteRenderer>().color = yellowCol;
                    foundLetters += guessInput.text.ToLower()[i];
            }
            else
            {
                    _lettersInRow[i].GetComponent<SpriteRenderer>().color = redCol;
            } 
            
        }

        if (greens == 5)
        {
            _streak++;
            _gameOff = true;
            StartCoroutine(Notice(greens));
        }
        else if(row > 0)
        {
            row--;
            _lettersInRow = FindLetters();
            _txtInRow = FindTxt();
            guessInput.text = "";
        }
        else
        {
            _gameOff = true;
            StartCoroutine(Notice(greens));
        }
    }

    public void StartGame()
    {
        WordManager.SetActive(true);
        WordManager.GetComponent<WordManager>().InitGrid();
        win = false;
        baseTime = maxTime;
        currentWord = GenWord();
        row = 5;
        _lettersInRow = FindLetters();
        _txtInRow = FindTxt();
        _timer = 0;
        totalTimer = maxTime;
        InstructUICanvas.enabled = false;
        GameUICanvas.enabled = true;
        _gameOff = false;
    }
    
}
