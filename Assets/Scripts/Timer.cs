using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float remainingTime = 60;
    [SerializeField] private float timePenalty = 5;
    private Text timer;

    private void Awake() { timer = GetComponent<Text>(); }

    public void MinusTime() => remainingTime -= timePenalty;

    private void CountDown() 
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime = 0;
            GameManager.Instance.isGameStart = false;
        }
    }

    private string ToText(float remainingTime)
    {
        int min = (int) remainingTime/60;
        int remainder = (int) remainingTime%60;
        string sec;
        if(remainder < 10) 
        { 
            sec = "0" + remainder.ToString(); 
        }
        else
        {
            sec = remainder.ToString();
        }
        string text = min.ToString() + ":" + sec;

        return text;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameStart) { CountDown(); }

        timer.text = ToText(remainingTime); ;
    }
}
