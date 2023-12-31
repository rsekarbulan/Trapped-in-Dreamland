using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    /*public Animator animator;*/

    public AudioSource theMusic;

    public bool startPlaying;

    public BeatController theBS;

    public static GameManager instance;

    public int currentScore = 0;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultScreen;
    public Text percentHitText, normalsText, goodText, perfectsText, missesText, rankText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();

                /*animator.SetInteger("state", 0);*/
            }
        }
        else
        {
            if(!theMusic.isPlaying && !resultScreen.activeInHierarchy) { 
                resultScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString(); 
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if(percentHit > 40) {
                    rankVal = "D";
                    if(percentHit > 55)
                    {
                        rankVal = "C";
                        if(percentHit > 70)
                        {
                            rankVal = "B";
                            if(percentHit > 85)
                            {
                                rankVal = "A";
                                if(percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();

            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

            multiText.text = "Multiplier: x" + currentMultiplier;

            currentScore += scorePerNote * currentMultiplier;
            scoreText.text = "Score: " + currentScore;
        }

   public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
        /*animator.SetInteger("state", 1);*/
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
        /*animator.SetInteger("state", 1);*/
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
        /*animator.SetInteger("state", 1);*/
    }
    public void NoteMissed()
    {
       Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;
        missedHits++;
    }
}