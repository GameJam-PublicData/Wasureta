using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
using InputSystemActions;
using StageSystem.Item;
using TMPro;
using UnityEngine.InputSystem;
using VContainer;

namespace StageSystem.Player
{
public class ItemSelector : MonoBehaviour
{
    GameObject _selectItem;
    
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
        _inputActions.Player.Interact.started += SelectItem;
    }

    void OnDisable()
    {
        _inputActions.Player.Interact.started -= SelectItem;
        _inputActions.Player.Disable();
    }

    void Update()
    {
        //目の前ずっと確認する
        bool hitDetected = Physics.Raycast(
            transform.position, 
            transform.forward, 
            out var hitInfo, 
            5f, 
            LayerMask.GetMask("Item")
        );

        //選択されているか
        if (hitDetected && hitInfo.collider != null)
        {
            if (_selectItem == null || _selectItem != hitInfo.collider.gameObject)
            {
                //選択された時
                Debug.Log($"ItemSelector: {hitInfo.collider.name}");
                _selectItem = hitInfo.collider.gameObject;
                ItemSelectHoverStart(hitInfo.collider.gameObject);
            }
        }
        else if (_selectItem != null)
        {
            //離された時
            Debug.Log("ItemSelector: アイテムから離れました");
            _selectItem = null;
            ItemSelectHoverEnd();
        }
        
        //デバックレイ
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
    }

    void ItemSelectHoverStart(GameObject selectObject)
    {
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
        Debug.Log("ItemSelector: アイテムを選択しようとしました");
        IItem item = _selectItem.GetComponent<IItem>();
        if (item != null)
        { 
            _itemManager.AddItem(item); 
            Debug.Log($"ItemSelector: アイテムを選択しました: {item}");
        }
    }
}
}