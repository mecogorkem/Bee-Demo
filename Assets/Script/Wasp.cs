using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    private void Start()
    {
        Client.Instance.player.skinChanged += SetSkin;
        SetSkin();
    }

    private void OnDestroy()
    {
        Client.Instance.player.skinChanged -= SetSkin;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            other.GetComponent<IInteractable>().Interact();
        }
    }

    private void SetSkin()
    {
        _skinnedMeshRenderer.material = Resources.Load<Material>($"Materials/{(int)Client.Instance.player.activeSkin}");
    }
    
    
    
}
