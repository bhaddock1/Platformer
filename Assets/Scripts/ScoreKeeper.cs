using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**************************************
 * attached to player: increases point count on player collision with each platform for the first time.
 * 
 * Bryce Haddock 1.0 September 22, 2023
 * ************************************/
public class Scorekeeper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreboardText;
    private float score;
    private HashSet<Collider> collidedPlatforms = new HashSet<Collider>(); // To track collided platforms

    public static Scorekeeper Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    // Adds to score on initial player collision trigger with a platform 
    public void AddToScore(float Collision)
    {

    }
       
         
    
    //Displays score to UI rounded to nearest integer
    public void DisplayScore()
    {
        int roundedScore = Mathf.RoundToInt(score);
        scoreboardText.text = "Score: " + roundedScore.ToString();                                 
    }
}

    