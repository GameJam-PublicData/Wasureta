using System;
using UnityEngine;

namespace StageSystem.Item
{
public interface IItem
{
    void GotItem();
}
public class Item : MonoBehaviour,IItem
{
    void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Item");
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Item");
        }
    }

    public void GotItem()
    {
        Destroy(gameObject);
    }
}
}

  
