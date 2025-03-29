using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionallyDisappearingBlock : MonoBehaviour
{
    public Entity entity;

    [SerializeField]
    private PotionItemSO _gimmickPotionSO;

    private void Update()
    {
        if(entity.IsDead)
        {
            InventoryManager.Instance.AddGimmickPotion(_gimmickPotionSO, true);
            Destroy(gameObject);
        }
    }
}
