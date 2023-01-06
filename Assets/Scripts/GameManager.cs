using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStart = false;
    public int takoyakiDone = 0; 

    #region Singleton
    public static GameManager Instance;

    private GameManager()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public void GameStart() { isGameStart = true; }

    public void Add()
    {
        takoyakiDone++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
