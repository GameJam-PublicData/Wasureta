using System;
using StageSystem.Interact;
using UnityEngine;
using DG.Tweening;

public class OpenDrawerAnimation : MonoBehaviour, IInteractive
{
    [SerializeField] float openTime = 5f;
    [SerializeField] float xDistance = 10f;
    [SerializeField] float zDistance = 10f;
    
    public void Interact()
    {
        transform.DOMove(new Vector3(xDistance, 0 ,zDistance ), openTime);
    }
}