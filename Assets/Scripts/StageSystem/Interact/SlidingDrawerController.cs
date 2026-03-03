using DG.Tweening;
using UnityEngine;

namespace StageSystem.Interact
{
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
}