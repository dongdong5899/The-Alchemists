using UnityEngine;

public class PutOpen : MonoBehaviour
{
    [SerializeField] private GameObject _interact;
    [SerializeField] private float _radius = 2f;

    private Player _player;
    private PopUpPanel _popUpPanel;
    private bool _isPlayerInRange = false;

    private void Start()
    {
        _popUpPanel = UIManager.Instance.panelDictionary[UIType.PopUp] as PopUpPanel;
        _player = PlayerManager.Instance.Player;
    }

    #region
    private void Update()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if (distance <= _radius)
        {
            if (!_isPlayerInRange)
            {
                _isPlayerInRange = true;
                OnPlayerEnter();
            }
        }
        else
        {
            if (_isPlayerInRange)
            {
                _isPlayerInRange = false;
                OnPlayerExit();
            }
        }
    }

    private void OnPlayerEnter()
    {
        _popUpPanel.SetText("器记 力累 [ F ]");
        UIManager.Instance.Open(UIType.PopUp);
        _player.PlayerInput.InteractPress += OnInteract;
    }

    private void OnPlayerExit()
    {
        UIManager.Instance.Close(UIType.PopUp);
        _player.PlayerInput.InteractPress -= OnInteract;
    }
    #endregion

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out _player))
    //    {
    //        _popUpPanel.SetText("器记 力累 [ F ]");
    //        UIManager.Instance.Open(UIType.PopUp);
    //        _player.PlayerInput.InteractPress += OnInteract;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out _player))
    //    {
    //        UIManager.Instance.Close(UIType.PopUp);
    //        _player.PlayerInput.InteractPress -= OnInteract;
    //    }
    //}

    private void OnInteract()
    {
        UIManager.Instance.Open(UIType.PotionCraft);
        UIManager.Instance.Close(UIType.PopUp);
        _player.PlayerInput.InteractPress -= OnInteract;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
