using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Sprite _closeSprite;
    private SpriteRenderer _sr;

    [SerializeField] private DropItem _dropItem;
    private float _delay = 0.3f;
    private bool _isInteracting = false;
    private bool _isOpen = false;


    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isInteracting && !_isOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
                Open();
        }
    }

    private void Open()
    {
        _sr.sprite = _openSprite;
        _isOpen = true;
        StartCoroutine(DelaySpawnDropItem());
    }

    private IEnumerator DelaySpawnDropItem()
    {
        yield return new WaitForSeconds(_delay);
        DropItem dropItem = Instantiate(_dropItem, transform.position, Quaternion.identity);
        dropItem.SpawnItem(Vector2.up * 8);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && !_isOpen)
        {
            PopUpPanel popUp = UIManager.Instance.GetUI(UIType.PopUp) as PopUpPanel;
            popUp.SetText("상자 열기 [ F ]");
            popUp.Open();
            _isInteracting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && !_isOpen)
        {
            UIManager.Instance.Close(UIType.PopUp);
            _isInteracting = false;
        }
    }
}
