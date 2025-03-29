using UnityEngine;

public class DisableMap : MonoBehaviour
{
    //private readonly Vector3[] offset = new Vector3[8] = {};
    private Vector3[] offsets = new Vector3[9] { Vector3.zero, Vector3.up * 0.5f, Vector3.down * 0.5f, Vector3.left * 0.5f, Vector3.right * 0.5f, new Vector3(-0.25f, -0.25f, 0), new Vector3(0.25f, -0.25f, 0), new Vector3(-0.25f, 0.25f, 0), new Vector3(0.25f, 0.25f, 0) };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < 9; i++)
        {
            if (MapManager.Instance.DisableMap(collision.transform.position + offsets[i]))
                break;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (MapManager.Instance != null)
            MapManager.Instance.EnableMap();
    }
}
