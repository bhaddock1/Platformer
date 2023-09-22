using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**************************************
 * attached to player: increases point count on player collision with each platform for the first time.
 * 
 * Bryce Haddock 1.0 September 22, 2023
 * ************************************/
public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreboardText;
    private float score;

    public static ScoreKeeper instance;

    private void Awake()
    {
        
    }

    private void AddToScore(Collision collision)
    {

    }
}

    