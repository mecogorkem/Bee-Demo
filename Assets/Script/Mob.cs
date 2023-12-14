using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Mob : MonoBehaviour,IInteractable
{
    [SerializeField] private TextMeshPro tmp;
    [SerializeField] private int damage;

    
    public void Initialize()
    {
        tmp.text = $"- {damage}";
    }
    
    public void Interact()
    {
        GameManager.Instance.LoseBee(damage);
        Destroy(this.gameObject);
    }
}
