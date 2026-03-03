using DG.Tweening;
using UnityEngine;

namespace StageSystem.Interact
{
public class SwingingDoorController : MonoBehaviour, IInteractive
{
    [SerializeField]float openTime = 5f;
    [SerializeField]float angle = 90f;
    
    public void Interact()
    {
        transform.DORotate(new Vector3(0, angle, 0), openTime , RotateMode.LocalAxisAdd);
    }
}
}
