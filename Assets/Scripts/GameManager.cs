using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameStart = false;
    public int takoyakiDone = 0;

    #region Singleton
    public static GameManager Instance;

    private GameManager()
    {
        if(Instance == null) { Instance = this; }
    }
    #endregion

    public void GameStart() { isGameStart = true; }

    public void Add() => takoyakiDone++;
}
