using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BeeHive : MonoBehaviour , IInteractable
{
    [SerializeField] private TextMeshPro tmp;
    private int beeCount;

    public void Initialize(int count)
    {
        beeCount = count;
        tmp.text = $"+ {count}";
    }
    
    public void Interact()
    {
        GameManager.Instance.CollectBee(beeCount);
        Destroy(this.gameObject);
    }
}
