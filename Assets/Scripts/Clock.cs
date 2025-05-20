using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Clock : MonoBehaviour
{

    private TextMeshProUGUI timeText;
    public bool isClockRunning = true;
    public int roundedTime;
    [Header("Clock Settings")]
    public float maxTime;
    public float currentTime;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
        timeText = GetComponent<TextMeshProUGUI>();
        timeText.text = currentTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        roundedTime = Mathf.RoundToInt(currentTime);
        timeText.text = roundedTime.ToString();
        if (currentTime <= 0)
        {
            isClockRunning = false;

        }
    }
}
