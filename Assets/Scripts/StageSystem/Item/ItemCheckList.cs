using InputSystemActions;
using MainSystem.CoreFlow;
using MainSystem.StageData;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using VContainer;

public class ItemCheckList : MonoBehaviour
{
    ItemSO _itemSO;
    
    [SerializeField] GameObject itemLootPrefab;
    int _totalItemAmount;
    int _currentItemAmount;
    
    [SerializeField] TextMeshProUGUI itemAmountText;
    
    [Inject]
    public void Construct(IStageSOProvider stageSOProvider)
    {
        //アイテムの総数を取得
        _totalItemAmount = itemLootPrefab.transform.childCount;
        
        _currentItemAmount = 0;
            
        //itemSOを取得
        InitItemSO(stageSOProvider);
        
        //テキスト初期化(0/3的な感じ)
        itemAmountText.text = $"{_currentItemAmount}/{_totalItemAmount}";
    }
    
    private void InitItemSO (IStageSOProvider stageSOProvider)
    {
        _itemSO = stageSOProvider.Get.ItemSO;
    }

    //取得したアイテムの数を増やす処理
    //アイテムを取得したときに呼び出すようにする
    public void AddGetItemCount()
    {
        _currentItemAmount++;

        //テキストに表示(1/3的な感じ)
        itemAmountText.text = $"{_currentItemAmount}/{_totalItemAmount}";
    }
    
    /*
    InputActions _inputActions;
    void Start()
    {
        _inputActions ??= new InputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Next.performed += TestNext;
    }

    void OnDisable()
    {
        _inputActions.Player.Next.performed -= TestNext;
        _inputActions.Player.Disable();
    }

    void TestNext(InputAction.CallbackContext context)
    {
        Debug.Log("TestNext");
        AddGetItemCount();
    }
    */
}
