//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;
//using DG.Tweening;
//using System.Collections.Generic;
//using System.Linq;

//public class InventoryManager : Singleton<InventoryManager>
//{
//    public ItemSetSO ItemSet;
//    [SerializeField] private Inventory playerInventory;
//    private QuickSlotSet quickslot;
//    public Inventory PlayerInventory => playerInventory;
//    public QuickSlotSet QuickSlot => quickslot;
//    public Item curMovingItem { get; private set; }
//    public InventorySlot curCheckingSlot { get; private set; }

//    public RectTransform inventoryRect;

//    private bool isIngredientsInventoryActive = true;

//    [SerializeField] private Transform itemParent;
//    [SerializeField] private Transform explainParent;

//    [SerializeField] private InputReader inputReader;

//    public Transform ItemParent => itemParent;
//    public Transform ExplainParent => explainParent;

//    private TextMeshProUGUI explainName;
//    private TextMeshProUGUI explainTxt;
//    private Image explainImage;

//    private Tween tween;
//    private bool inventoryOpen = false;

//    private List<Inventory> inventories;

//    private void Awake()
//    {
//        inventories = FindObjectsByType<Inventory>(FindObjectsSortMode.None).ToList();

//        explainName = explainParent.Find("Name").GetComponent<TextMeshProUGUI>();
//        explainTxt = explainParent.Find("Explain").GetComponent<TextMeshProUGUI>();
//        explainImage = explainParent.Find("Frame/Image").GetComponent<Image>();
//        SetExplain(null);
//    }

//    private void OnEnable()
//    {
//        inputReader.PressTabEvent += OnPressTab;
//    }

//    private void OnDisable()
//    {
//        inputReader.PressTabEvent -= OnPressTab;
//    }

//    public void LoadAllInventory()
//    {
//        inventories.ForEach(inven => inven.Load());
//    }

//    public void MoveItem(Item item) => curMovingItem = item;
//    public void CheckSlot(InventorySlot slot) => curCheckingSlot = slot;

//    public void SetExplain(ItemSO itemSO)
//    {
//        if (itemSO == null)
//        {
//            explainName.SetText("");
//            explainTxt.SetText("");
//            explainImage.color = new Color(1, 1, 1, 0);
//            return;
//        }

//        explainImage.color = new Color(1, 1, 1, 1);
//        explainName.SetText(itemSO.itemName);
//        explainTxt.SetText(itemSO.itemExplain);
//        explainImage.sprite = itemSO.itemImage;
//    }

//    public void OnPressTab()
//    {
//        if (tween != null && tween.active)
//            tween.Kill();

//        if (inventoryOpen == false)
//            OpenInventory();
//        else
//            CloseInventory();
//    }

//    public void OpenInventory()
//    {
//        tween = inventoryRect.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);
//        inventoryOpen = true;
//    }

//    public void CloseInventory()
//    {
//        tween = inventoryRect.DOAnchorPosY(-1100, 0.3f).SetEase(Ease.Linear);
//        inventoryOpen = false;
//    }

//    public Item MakeItemInstanceByItemSO(ItemSO itemSO, int amount = 1)
//    {
//        if (itemSO == null) return null;
//        Item item = Instantiate(itemSO.prefab, itemParent).GetComponent<Item>();
//        item.SetItemAmount(amount);

//        return item;
//    }

//    public void EnbableIgredientsInventory(bool isEnable)
//    {

//    }
//}
