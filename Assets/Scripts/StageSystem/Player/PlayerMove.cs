using System;
using InputSystemActions;
using MainSystem.CoreFlow;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StageSystem.Player
{
public class PlayerMove : MonoBehaviour
{
    InputActions _inputActions;
    
    InputAction _moveAction;
    InputAction _lookAction;
    GameObject _playerCamera;
    
    [SerializeField] float speed = 10f;
    [SerializeField] float lookSpeed = 2f;
    
    [Header("カメラの角度の上限と下限")]
    [SerializeField] float upperLimit = -30f;
    [SerializeField] float lowerLimit = 60f;

    void OnEnable()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();
        _moveAction = _inputActions.Player.Move;
        _lookAction = _inputActions.Player.Look;
        Debug.Log("完了");
    }

    void Awake()
    {
        //子オブジェクトを取得
        _playerCamera = transform.GetChild(0).gameObject;
        
        //マウス関連
        Cursor.lockState = CursorLockMode.Locked; //マウスカーソルを中央に固定して非表示
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None; //マウスカーソルのロックを解除して表示
    }

    void FixedUpdate()
    {
        if(StageFlow.IsGameEnd) return;//ゲームが一時停止している場合は処理を行わない
        //移動
        //値を受け取る
        Vector3 moveValue = _moveAction.ReadValue<Vector2>();
        moveValue = new Vector3(moveValue.x, 0, moveValue.y);
        
        //動く
        transform.Translate(moveValue * (speed * Time.fixedDeltaTime));
    }

    
    void Update()
    {
        if(StageFlow.IsGameEnd) return;//ゲームが一時停止している場合は処理を行わない
        // 視点移動（Delta値を使用）
        Vector2 mouseDelta = _lookAction.ReadValue<Vector2>();
    
        //縦回転
        _playerCamera.transform.Rotate(-mouseDelta.y * lookSpeed, 0, 0, Space.Self);
        //角度を-180~180の間に矯正
        float rotationX = _playerCamera.transform.localEulerAngles.x;
        if (rotationX > 180f)
            rotationX -= 360f;
        //上限値と下限値を設定
        rotationX = Mathf.Clamp(rotationX, upperLimit, lowerLimit);
        _playerCamera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);

        //横回転
        transform.Rotate(0, mouseDelta.x * lookSpeed, 0, Space.World);
        
    }

    void OnDisable()
    {
        _inputActions.Player.Disable();
    }
}
}
