using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnLockInventory : MonoBehaviour
{
    private Image slot;
    private GameObject lockSlot;

    private void Awake()
    {
        slot = GetComponentInChildren<Image>();//��ũ��Ʈ ���� ����
    }
}
