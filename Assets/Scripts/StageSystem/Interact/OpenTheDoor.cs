using System;
using UnityEngine;
using DG.Tweening;
using StageSystem.Interact;

public class OpenTheDoor : MonoBehaviour ,IInteractive
{
    [SerializeField] float openTime = 1f;
    [SerializeField] float angle = 90f;
   
    public void Interact()
    {
        transform.DORotate(new Vector3(0, angle, 0), openTime , RotateMode.LocalAxisAdd);
    }
}
