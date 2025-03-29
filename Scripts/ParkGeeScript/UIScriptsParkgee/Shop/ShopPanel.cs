using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class ShopPanel : MonoBehaviour, IManageableUI
{
    [SerializeField] private float _onYPos;
    [SerializeField] private float _offYPos;
    [SerializeField] private RectTransform _rectTrm;

    [SerializeField] private Transform _shopItemParent;
    [SerializeField] private ShopItemTableSO _shopItemTables;

    private ShopItemUI[] _shopItem;
    private bool _isPlay;
    private bool _isShuple = false;

    public void Open()
    {
        //if (_isPlay) return;
        ////Time.timeScale = 0;
        //SettingRandomItem();
        //_isPlay = true;

        //_rectTrm.DOAnchorPosY(_onYPos, 0.3f)
        //    .SetEase(Ease.InOutFlash)
        //    .SetUpdate(true)
        //    .OnComplete(() => _isPlay = false);

        gameObject.SetActive(true);
        PlayerManager.Instance.DisablePlayerMovementInput();
        SettingRandomItem();
    }
    
    private void SettingRandomItem()
    {
        if (_isShuple)
            return;

        ShopItemSO[] soArr = _shopItemTables.list
                           .Where(x => x.CheckSelect()).ToArray();

        if (soArr.Length < 6)
        {
            //Ȥ�ó� ��û�� �̽������� ��ǻ� �ʿ���� �ڵ�
            Debug.LogError("Error! : Must hav 6 item at least");
            return;
        }

        //ShopItemTableSO �ȿ� �ִ� SO���� �ߺ��� ���� �ʰ� �������� ������ _shopItem.Length���� ���÷� �̴´�.
        _shopItem = _shopItemParent.GetComponentsInChildren<ShopItemUI>();
        for (int i = 0; i < _shopItem.Length; i++)
        {
            int index = Random.Range(0, soArr.Length - i);

            _shopItem[i].SetItemData(soArr[index]);
            soArr[index] = soArr[soArr.Length - 1 - i];
        }

        _isShuple = true;
    }

    public void Close()
    {
        //if (_isPlay) return;
        ////Time.timeScale = 1;
        //_isPlay = true;

        //_rectTrm.DOAnchorPosY(_offYPos, 0.3f)
        //    .SetEase(Ease.OutBack)
        //    .SetUpdate(true)
        //    .OnComplete(() => _isPlay = false);
                    
        gameObject.SetActive(false);
        PlayerManager.Instance.EnablePlayerMovementInput();
    }

    public void Init()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
}
