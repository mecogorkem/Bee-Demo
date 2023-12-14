using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private List<string> waspName;
    private int currentIndex;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private GameObject button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI flowerText;
    [SerializeField] private GameObject mainWasp;

    private void Start()
    {
        currentIndex = 0;
        _skinnedMeshRenderer.material = _materials[currentIndex];
        UpdateButtonStatues();
    }

    public void Left()
    {
        currentIndex -= 1;
        _skinnedMeshRenderer.material = _materials[currentIndex];
        UpdateButtonStatues();
    }

    public void Right()
    {
        currentIndex += 1;
        _skinnedMeshRenderer.material = _materials[currentIndex];
        UpdateButtonStatues();
    }

    private void UpdateButtonStatues()
    {
        leftButton.interactable = currentIndex > 0;
        rightButton.interactable = currentIndex < _materials.Count-1;
        SkinType selectedSkin = (SkinType)currentIndex;
        button.gameObject.SetActive(true);
        flowerText.text = Client.Instance.player.flower.ToString();

        if (Client.Instance.player.availableSet.Any(e => e == selectedSkin))
        {
            if (selectedSkin == Client.Instance.player.activeSkin)
            {
                button.gameObject.SetActive(false);

            }
            else
            {
                buttonText.text = "Use";
            }
            priceText.text = "";

        }
        else
        {
            priceText.text = "100";
            buttonText.text = "Buy";
        }
    }

    public void Interact()
    {
        
        SkinType selectedSkin = (SkinType)currentIndex;

        if (Client.Instance.player.availableSet.Any(e => e == selectedSkin))
        {
            Client.Instance.player.activeSkin = selectedSkin;
        }
        else
        {
            if (Client.Instance.player.flower>=100)
            {
                Client.Instance.player.availableSet.Add(selectedSkin);
                Client.Instance.player.UseFlower(100);
            }
        }
        UpdateButtonStatues();

    }

    public void CloseShop()
    {
        mainWasp.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
