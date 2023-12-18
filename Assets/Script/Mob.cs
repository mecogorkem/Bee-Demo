using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Mob : Interactable,IInteractable
{
    [SerializeField] private TextMeshPro tmp;
    [SerializeField] private int damage;
    private bool isTrigger;
    
    public void Initialize()
    {
        base.Initialize();
        tmp.text = $"- {damage}";
        isTrigger = false;
    }
    
    public void Interact()
    {
        if (CheckTrigger())
        {
            return;
        }
        GameManager.Instance.LoseBee(damage);
        Destroy(this.gameObject);
    }
}
