using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flower : Interactable, IInteractable
{
    [SerializeField] private TextMeshPro tmp;
    private int flowerCount;

    public void Initialize(int count)
    {
        base.Initialize();
        flowerCount = count;
        tmp.text = count.ToString();
    }
    
    public void Interact()
    {
        if (CheckTrigger())
        {
            return;
        }
        GameManager.Instance.CollectFlower(flowerCount);
        Destroy(this.gameObject);
    }
}
