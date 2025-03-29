using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public abstract string GetItemName(int level = 0);
    public abstract string GetItemDescription(int level = 0);
    public abstract int GetItemTypeNumber();

    [Space(20)]
    [Header("Sprite")]
    public Sprite image;
}