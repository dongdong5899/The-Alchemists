//using DG.Tweening;
//using UnityEngine;
//using UnityEngine.UI;
//using Doryu.Inventory;

///// <summary>
///// ��� 3���� ���°� �ִٰ� �����ϸ� ��
///// 
///// Ȱ��ȭ ���ִ� ����, ��Ȱ��ȭ ����, �Ҵ�� �ִ� ������ ������ �ִ� ����
///// ������ ���´� �� ��ũ��Ʈ�� ������ �ȵ����� ���� �����ϱ� ���϶�� �̷��� ǥ���Ѱ���
///// 
///// �̸� QuickSlotSet 3���� �����صдٰ� �ϸ�
///// 
///// 1��°�� Ȱ��ȭ ���·� ����� �غ� �Ǿ��ְ�
///// 2��°�� ��Ȱ��ȭ ���·� Ȱ��ȭ ������ �������� �پ��� ��� �Ѿ��
///// 3��°�� � �������� �Ҵ�Ǿ� �ֳ��� ������ �ִٰ� 1���� �پ��� 2���� Ȱ��ȭ���°� �ǰ� 
///// 3���� ��Ȱ��ȭ ���°� �Ǽ� �ΰ��ӿ��� ������Ʈ�� ��������� ��
///// </summary>
//public class QuickSlotSet : MonoBehaviour
//{
//    [SerializeField] private AnimationCurve disableAnimCurve;
//    [SerializeField] private RectTransform quickSlotSetRect;

//    public int slotNum = 0;

//    private readonly QuickSlotVisual[] slots = new QuickSlotVisual[5];
//    private QuickSlotSetsParent quickSlotSetsParent;

//    private Image quickSlotSetImage;
//    private Player player;

//    private int selectedSlot = -1;

//    private Sequence seq;

//    private void Awake()
//    {
//        Transform slotParent = transform.Find("Slots");
//        for (int i = 0; i < 5; i++)
//            slots[i] = slotParent.GetChild(i).GetComponent<QuickSlotVisual>();

//        quickSlotSetsParent = transform.parent.GetComponent<QuickSlotSetsParent>();
//        quickSlotSetImage = GetComponent<Image>();

//        player = PlayerManager.Instance.Player;
//    }

//    private void OnDisable()
//    {
//        UnSetQuickSlotInput();
//    }

//    public void SetItem(ItemSO item, int index)
//    {
//        slots[index].SetItem(item);
//    }

//    public void RemoveItem(int idx)
//    {
//        slots[idx].DeleteItem();
//    }

//    private void SelectOneSlot(int num)
//    {
//        if (selectedSlot == num)
//            num = -1;

//        selectedSlot = num;

//        for (int i = 0; i < 5; i++)
//        {
//            if (i == num)
//                slots[i].EnableSlot();
//            else
//                slots[i].DisableSlot();
//        }

//        if (num > 0 && slots[num].AssignedPortion != null)
//        {
//            player.throwingPortionSelected = (true);
//        }
//        else
//        {
//            player.throwingPortionSelected = (false);
//        }
//    }

//    private void UseQuickSlot()
//    {
//        if (selectedSlot == -1) return;

//        if (slots[selectedSlot].AssignedPortion != null)
//        {
//            slots[selectedSlot].UseItem();
//            QuickSlotManager.Instance.RemoveItem(0, selectedSlot, true);
//        }
//        else
//            return;

//        bool isSlotEmpty = true;

//        for (int i = 0; i < 5; i++)
//        {
//            if (slots[i].AssignedPortion != null)
//            {
//                isSlotEmpty = false;
//                break;
//            }
//        }

//        if (isSlotEmpty)
//        {
//            QuickSlotManager.Instance.MoveToNextQuickSlot();
//        }
//    }

//    private void UnSelectAllSlot()
//    {
//        selectedSlot = -1;

//        for (int i = 0; i < 5; i++)
//        {
//            slots[i].DisableSlot();
//        }
//    }

//    #region QuickSlotAnimation

//    public void ChangeQuickSlotSet(QuickSlotItems items)
//    {
//        UnSetQuickSlotInput();

//        if (seq != null && seq.active)
//            seq.Kill();

//        slotNum = -1;
//        seq = DOTween.Sequence();

//        float tweeningTime = Random.Range(0.50f, 0.70f);
//        Graphic[] graphics = quickSlotSetRect.GetComponentsInChildren<Graphic>();

//        seq.Append(quickSlotSetRect.DOAnchorPosY(-150f, tweeningTime).SetEase(disableAnimCurve))
//            .Join(quickSlotSetRect.DOAnchorPosX(Random.Range(-10f, 10f), tweeningTime))
//            .AppendCallback(() =>
//            {
//                for (int i = 0; i < slots.Length; i++)
//                {
//                    slots[i].DisableSlot();
//                    if (items.items[i] != null)
//                        slots[i].SetItem(items.items[i]);
//                }

//                foreach (Graphic g in graphics)
//                {
//                    if (g != null)
//                        g.color = new Color(1, 1, 1, 0);
//                }

//                quickSlotSetRect.anchoredPosition = new Vector2(-200f, 0f);
//            })
//            .AppendInterval(0.2f)
//            .AppendCallback(() =>
//            {
//                for (int i = 0; i < slots.Length; i++)
//                {
//                    if (items.items[i] != null)
//                        slots[i].SetItem(items.items[i]);
//                }

//                foreach (Graphic g in graphics)
//                {
//                    if (g != null)
//                        seq.Join(g.DOFade(1f, 0.2f).SetEase(Ease.InSine));
//                }
//            })
//            .Append(quickSlotSetRect.DOAnchorPosX(-75f, 0.25f).SetEase(Ease.InSine))
//            .OnComplete(SetQuickSlotInput);
//    }

//    #endregion

//    #region QuickSlotInput

//    private void SetQuickSlotInput()
//    {
//        player.PlayerInput.SelectQuickSlot += SelectOneSlot;
//        player.PlayerInput.OnUseQuickSlot += UseQuickSlot;
//        player.PlayerInput.SelectMysteryPortion += UnSelectAllSlot;
//    }
//    private void UnSetQuickSlotInput()
//    {
//        player.PlayerInput.SelectQuickSlot -= SelectOneSlot;
//        player.PlayerInput.OnUseQuickSlot -= UseQuickSlot;
//        player.PlayerInput.SelectMysteryPortion -= UnSelectAllSlot;
//    }

//    #endregion

//    public void Init(QuickSlotItems items)
//    {
//        SetQuickSlotInput();

//        slotNum = 0;

//        for (int i = 0; i < 5; i++)
//            slots[i].SetItem(items.items[i]);

//        selectedSlot = -1;
//    }
//}