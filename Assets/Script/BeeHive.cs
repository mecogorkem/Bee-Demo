using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BeeHive : Interactable , IInteractable
{
    [SerializeField] private TextMeshPro tmp;
    private int beeCount;

    public void Initialize(int count)
    {
        base.Initialize();
        beeCount = count;
        tmp.text = $"+ {count}";
        
        
    }
    
    public void Interact()
    {
        if (CheckTrigger())
        {
            return;
        }
        GameManager.Instance.CollectBee(beeCount);
        Destroy(this.gameObject);
        return;
    }
}
