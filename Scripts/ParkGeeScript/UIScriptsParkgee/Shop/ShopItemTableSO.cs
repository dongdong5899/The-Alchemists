using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Shop/ShopItemTable")]
public class ShopItemTableSO : ScriptableObject
{
    public List<ShopItemSO> list = new List<ShopItemSO>();
}
