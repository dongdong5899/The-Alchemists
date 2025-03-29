using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPotion : Potion
{
    public override void Init(QuickSlot slot, Entity owner, Vector2 movement = default, float rotatPow = 0)
    {
        //DrinkPotionItemSO drinkPotionItemSO = slot.assignedItem.itemSO as DrinkPotionItemSO;
        base.Init(slot, owner);
    }
    public override void UsePotion()
    {
        List<IAffectable> list = new List<IAffectable>() { PlayerManager.Instance.Player };

        foreach (Effect effect in effects)
        {
            effect.SetAffectedTargets(list);
            effect.ApplyEffect();
        }
    }
}
