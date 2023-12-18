using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isTrigger;

    protected void Initialize()
    {
        isTrigger = false;
    }

    public bool CheckTrigger()
    {
        if (isTrigger)
        {
            return true;
        }
        isTrigger = true;
        return false;
    }
}
