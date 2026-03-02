using InputSystemActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StageSystem.Player
{
public class PlayerMove : MonoBehaviour
{
    InputActions _inputActions;
    InputAction _moveValue;
    [SerializeField] float speed = 10f;

    void OnEnable()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();
        _moveValue = _inputActions.Player.Move;
        Debug.Log("完了");
    }

    void FixedUpdate()
    {
        //値を受け取る
        Vector3 moveValue = _moveValue.ReadValue<Vector2>();
        moveValue = new Vector3(moveValue.x, 0, moveValue.y);
        
        //動く
        transform.Translate(moveValue * (speed * Time.fixedDeltaTime));
    }
}
}
