using DG.Tweening;
using UnityEngine;

namespace StageSystem.Interact
{
public class OpenTheDoor : MonoBehaviour ,IInteractive
{
    [SerializeField] float openTime = 1f;
    [SerializeField] float angle = 90f;
   
    bool _isInteract = false;
    public void Interact()
    {
        if(_isInteract) return;
        transform.DORotate(new Vector3(0, angle, 0), openTime , RotateMode.LocalAxisAdd);
        _isInteract = true;
    }
    //俺は磯貝。みんな嫌いだぜ
}
}
