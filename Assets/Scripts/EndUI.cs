using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    [SerializeField] private int scoreModifier = 10;
    public Text finalScore;
    private int totalScore;

    // Start is called before the first frame update
    void Start() { finalScore = GetComponent<Text>(); }

    // Update is called once per frame
    void Update() 
    {
        totalScore = GameManager.Instance.takoyakiDone * scoreModifier;
        finalScore.text = "最終スコア： " + totalScore.ToString(); 
    }
}
