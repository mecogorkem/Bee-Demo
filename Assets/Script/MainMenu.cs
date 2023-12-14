using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<Sprite> soundSprites;
    [SerializeField] private Image soundButton;
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI flowerCount;
    [SerializeField] private GameObject levelSelectPage;
    [SerializeField] private AudioSource ambiance;
    [SerializeField] private GameObject skinStore;
    [SerializeField] private GameObject wasp;
    private void Start()
    {
        flowerCount.text = Client.Instance.player.flower.ToString();
        currentLevel.text = $"Level {Client.Instance.currentLevel.ToString()}";
        soundButton.sprite = soundSprites[Client.Instance.player.sound?1:0];
        ambiance.mute = !Client.Instance.player.sound;
    }

    public void ChangeSound()
    {
        Client.Instance.player.ChangeSoundStatues();
        soundButton.sprite = soundSprites[Client.Instance.player.sound?1:0];
        ambiance.mute = !Client.Instance.player.sound;

    }

    public void OpenSkinStore()
    {
        skinStore.SetActive(true);
        wasp.SetActive(false);
    }

    public void ChangeLevelPageStatues()
    {
        levelSelectPage.SetActive(!levelSelectPage.activeSelf);
    }

    public void StartLevel()
    {
        Client.Instance.StartLevel();
    }
}
