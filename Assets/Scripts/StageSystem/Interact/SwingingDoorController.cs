using DG.Tweening;
using UnityEngine;

namespace StageSystem.Interact
{
public class SwingingDoorController : MonoBehaviour, IInteractive
{
    [SerializeField] float openTime = 5f;
    [SerializeField] float angle = 90f;

    bool _isOpen = false;
    bool _isAnimating = false;

    public void Interact()
    {
        // アニメーション中は無視
        if (_isAnimating) return;

        _isAnimating = true;
        float targetAngle = _isOpen ? -angle : angle;

        transform.DORotate(new Vector3(0, targetAngle, 0), openTime, RotateMode.LocalAxisAdd)
            .OnComplete(() =>
            {
                _isOpen = !_isOpen;
                _isAnimating = false;
            });
    }
}
}
