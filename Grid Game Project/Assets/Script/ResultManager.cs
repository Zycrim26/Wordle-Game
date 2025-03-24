using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI WordDisp;
    public TextMeshProUGUI StatDisp;
    public TextMeshProUGUI ScoreDisp;
    public TextMeshProUGUI CongratsDisp;

    private AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        WordDisp.text = "The Word Was: " + GameManager.currentWord;
        ScoreDisp.text = "Score: " + GameManager.score + " High Score: " + GameManager.highScore;
        if (GameManager.win)
        {
            StatDisp.text = "You Got It In:\n " + (6 - GameManager.row) + " - Attempts\n" + (GameManager.baseTime - GameManager.totalTimer) + " - Seconds";
            _audioSource.Play();
        }
        else
        {
            StatDisp.text = "";
            ScoreDisp.transform.localPosition = new Vector2(-100, 0);
            CongratsDisp.text = "Better Luck Next Time";
        }
    }
}
