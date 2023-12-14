using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    
    [SerializeField] private List<Sprite> soundSprites;
    [SerializeField] private Image soundButton;

    private void Start()
    {
        soundButton.sprite = soundSprites[Client.Instance.player.sound?1:0];
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeSound()
    {
        Client.Instance.player.ChangeSoundStatues();
        soundButton.sprite = soundSprites[Client.Instance.player.sound?1:0];
        GameManager.Instance.SoundChange();
    }

    public void Replay()
    {
        Client.Instance.StartLevel();
    }
    public void NextLevel()
    {
        Client.Instance.StartLevel(Client.Instance.currentLevel+1>10?10:Client.Instance.currentLevel+1);
    }
}
