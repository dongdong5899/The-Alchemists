using DG.Tweening;
using TMPro;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private TextMeshPro txt;
    private Tween tween;

    private void Awake()
    {
        txt = GetComponent<TextMeshPro>();
    }

    public void DoEffect()
    {
        if (tween != null && tween.active)
            tween.Kill();

        Vector3 endPos = transform.position + new Vector3(0, Random.Range(0.5f, 1.5f));
        float jumpPower = Random.Range(1f,3f);
        float duration = 0.4f;
        Color endColor = new Color(txt.color.r, txt.color.g, txt.color.b, 0);

        //여기 OnComplete에서 풀링 하면 바꿔주기로
        tween = transform.DOJump(endPos, jumpPower, 1,duration)
                        .SetEase(Ease.Linear)
                        .SetDelay(0.05f)
                        .OnComplete(() => Destroy(gameObject));
    }

    public void Init(float damage, float fontSize,Color color, Vector2 position)
    {
        transform.position = position;
        txt.SetText(damage.ToString());
        txt.fontSize = fontSize;
        txt.color = color;
    }
}
