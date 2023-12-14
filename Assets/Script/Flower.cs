using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flower : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshPro tmp;
    private int flowerCount;

    public void Initialize(int count)
    {
        flowerCount = count;
        tmp.text = count.ToString();
    }
    
    public void Interact()
    {
        GameManager.Instance.CollectFlower(flowerCount);
        Destroy(this.gameObject);
    }
}
