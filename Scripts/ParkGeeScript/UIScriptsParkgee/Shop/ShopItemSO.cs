using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Shop/Item")]
public class ShopItemSO : ScriptableObject
{
    public Sprite itemImg;
    public string itemName;
    [TextArea] public string itemMenual;
    public string itemPrice;

    public bool CheckSelect()
    {
        return true;
    }
}
