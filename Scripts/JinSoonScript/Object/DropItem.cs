using TMPro;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public ItemSO itemSO;
    private Rigidbody2D _rigid;
    private PopUpPanel _popUp;
    private bool interacting;

    public bool interactEnable = true;

    private Collider2D[] _colliders = new Collider2D[1];
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private LayerMask _whatIsPlayer;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _popUp = UIManager.Instance.panelDictionary[UIType.PopUp] as PopUpPanel;
    }

    private void Update()
    {
        if (interactEnable == false) return;

        int count = Physics2D.OverlapCircleNonAlloc
            ((Vector2)transform.position + _offset, 
            _radius, _colliders, _whatIsPlayer);

        if (count != 0 && interacting == false)
        {
            _popUp.SetText("아이템 줍기 [F]");
            UIManager.Instance.Open(UIType.PopUp);
            interacting = true;
        }
        else if (count == 0 && interacting)
        {
            UIManager.Instance.Close(UIType.PopUp);
            interacting = false;
        }

        if (interacting && Input.GetKeyDown(KeyCode.F))
        {
            if (InventoryManager.Instance.TryAddItem(itemSO, openPannel: true))
            {
                DropItemManager.Instance.IndicateItemPanel(itemSO);
                UIManager.Instance.Close(UIType.PopUp);
                Destroy(gameObject);
            }
        }
    }

    public void SpawnItem(Vector2 dir)
    {
        _rigid.AddForce(dir, ForceMode2D.Impulse);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + _offset, _radius);
    }
#endif
}
