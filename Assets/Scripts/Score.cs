using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int scoreModifier = 10;
    private Text score;
    private int totalScore;

    private void Awake()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        totalScore = GameManager.Instance.takoyakiDone * scoreModifier;
        score.text = totalScore.ToString();
    }
}