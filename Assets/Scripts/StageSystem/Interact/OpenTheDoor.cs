using DG.Tweening;
using UnityEngine;

namespace StageSystem.Interact
{
public class OpenTheDoor : MonoBehaviour ,IInteractive
{
    [SerializeField] float openTime = 1f;
    [SerializeField] float angle = 90f;
   
    bool isInteract = false;
    public void Interact()
    {
        if(isInteract) return;
        transform.DORotate(new Vector3(0, angle, 0), openTime , RotateMode.LocalAxisAdd);
        isInteract = true;
    }
}
}
