using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Player player;
    private Transform playerTrm;

    public float playerMaxX;

    private void Update()
    {
        playerMaxX = Mathf.Max(playerTrm.position.x, playerMaxX);
    }

    public Player Player 
    {
        get
        {
            if(player == null)
                player = FindObjectOfType<Player>();

            if (player == null)
                Debug.LogError($"Player is not exist in this scene but still trying to excess it");

            return player;
        } 
        private set
        {
            player = value;
        }
    }
    public Transform PlayerTrm
    {
        get
        {
            if (playerTrm == null)
                playerTrm = Player.transform;

            return playerTrm;
        }
        private set
        {
            playerTrm = value;
        }
    }

    public void DisablePlayerMovementInput()
    {
        player.PlayerInput.Controlls.Player.Disable();
    }

    public void EnablePlayerMovementInput()
    {
        player.PlayerInput.Controlls.Player.Enable();
    }

    public void EnablePlayerInventoryInput()
    {
        player.PlayerInput.Controlls.UI.Enable();
    }

    public void DisablePlayerInventoryInput()
    {
        player.PlayerInput.Controlls.UI.Disable();
    }
}
