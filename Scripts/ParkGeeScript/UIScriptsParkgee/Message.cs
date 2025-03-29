using System.Collections;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float waitSecond = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            MessageOn();
        }
    }

    public void MessageOn()
    {
        if (text.text == null)
        {
            text.text = message;
        }
        else
            StartCoroutine(IETextDelay());
    }

    private IEnumerator IETextDelay()
    {
        yield return new WaitForSeconds(waitSecond);
        text.text = null;
    }
}
