using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour
{
    public static Client Instance;
    public Player player;
    private int _currentLevel;

    public int currentLevel => _currentLevel;



    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (player == null)
        {
            player = new Player();
        }

        Instance = this;
        DontDestroyOnLoad(this);
        _currentLevel = player.reachedLevel;
    }

    public void StartLevel()
    {
        Debug.Log("LevelStart = " + currentLevel.ToString());
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void StartLevel(int level)
    {
        _currentLevel = level;
        StartLevel();
    }
    
    
    
}
