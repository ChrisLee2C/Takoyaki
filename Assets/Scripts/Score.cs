using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int scoreModifier = 10;
    private int totalScore = 0;
    private Text score;

    private void Awake() => score = GetComponent<Text>();

    public void SetScore() => GameManager.Instance.takoyakiDone = 0;

    // Update is called once per frame
    void Update()
    {
        totalScore = GameManager.Instance.takoyakiDone * scoreModifier;
        score.text = totalScore.ToString();
    }
}
