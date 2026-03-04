using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
using InputSystemActions;
using StageSystem.Interact;
using StageSystem.Item;
using TMPro;
using UnityEngine.InputSystem;
using VContainer;

namespace StageSystem.Player
{
public class ItemSelector : MonoBehaviour
{
    GameObject _selectObj;
    
    [SerializeField] GameObject hoverObject; 
    
    [SerializeField] RectTransform itemNameUI;
    [SerializeField] TextMeshProUGUI itemNameText;
    Tween _itemNameUITween;

    InputActions _inputActions;

    IItemManager _itemManager;
    
    [Inject]
    public void Construct(IItemManager itemManager)
    {
        _itemManager = itemManager;
    }
    
    void OnEnable()
    {
        _inputActions ??= new InputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Attack.started += SelectItem;
        _inputActions.Player.Interact.started += Interact;
        _mask = LayerMask.GetMask("Item", "Interactive");
    }

    void OnDisable()
    {
        _inputActions.Player.Attack.started -= SelectItem;
        _inputActions.Player.Interact.started -= Interact;
        _inputActions.Player.Disable();
    }

    LayerMask _mask;
    void Update()
    {
        //目の前ずっと確認する
        bool hitDetected = Physics.Raycast(
            transform.position, 
            transform.forward, 
            out var hitInfo, 
            5f, 
            _mask
        );

        //選択されているか
        if (hitDetected && hitInfo.collider != null)
        {
            if (_selectObj == null || _selectObj != hitInfo.collider.gameObject)
            {
                //選択された時
                Debug.Log($"ItemSelector: {hitInfo.collider.name}");
                _selectObj = hitInfo.collider.gameObject;
                ItemSelectHoverStart(hitInfo.collider.gameObject);
            }
        }
        else if (_selectObj != null)
        {
            //離された時
            Debug.Log("ItemSelector: アイテムから離れました");
            _selectObj = null;
            ItemSelectHoverEnd();
        }
        
        //デバックレイ
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
    }

    void ItemSelectHoverStart(GameObject selectObject)
    {
        Debug.Log("アニメーションスタート");
        
        if (hoverObject != null)
        {
            hoverObject.SetActive(true);
        }
        
        //itemNameUIを上からアニメーションだす
        if (_itemNameUITween != null)
        {
            _itemNameUITween.Kill();
        }
        
        itemNameText.text = selectObject.name;
        
        itemNameUI.anchoredPosition = new Vector2(0f, 100f);
        _itemNameUITween = itemNameUI.DOAnchorPos(new Vector2(0f,-50f), 0.5f).SetEase(Ease.OutBack);
    }

    void ItemSelectHoverEnd()
    {
        Debug.Log("アニメーションエンド");
        
        if (hoverObject != null)
        {
            hoverObject.SetActive(false);
        }
        
        //itemNameUIを上に戻す
        if (_itemNameUITween != null)
        {
            _itemNameUITween.Kill();
        }
        
        itemNameUI.anchoredPosition = new Vector2(0f, -50f);
        _itemNameUITween = itemNameUI.DOAnchorPos(new Vector2(0f,100f), 0.5f).SetEase(Ease.OutBack);
    }

    void SelectItem(InputAction.CallbackContext context)
    {
        if(_selectObj == null)
        {
            Debug.Log("ItemSelector: アイテムを選択しようとしましたが、選択されているアイテムがありません");
            return;
        }
        
        if(_selectObj.TryGetComponent(out IItem item) ==false)
        {
            Debug.Log("ItemSelector: アイテムを選択しようとしましたが、プレイヤー自身がアイテムでした");
            return;
        }
        if (item == null) return;
        _itemManager.AddItem(item); 
        Debug.Log($"ItemSelector: アイテムを選択しました: {item}");
    }

    void Interact(InputAction.CallbackContext ctx)
    {
        if(_selectObj == null)
        {
            Debug.Log("ItemSelector: インタラクトしようとしましたが、選択されているオブジェクトがありません");
            return;
        }
        if (_selectObj.TryGetComponent(out IInteractive interactive) ==false)
        {
            Debug.Log("ItemSelector: インタラクトしようとしましたが、プレイヤー自身がインタラクトでした");
            return;
        }
        if (interactive == null) return;
        interactive.Interact();
        Debug.Log($"ItemSelector: アイテムとインタラクトしました: {interactive}");
    }
}
}