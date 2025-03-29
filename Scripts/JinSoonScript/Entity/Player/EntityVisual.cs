using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Material _entityMat;
    [SerializeField] private float _whiteTime = 0.15f;
    [SerializeField] private GameObject _skillEffect;

    private readonly int BlinkShaderHash = Shader.PropertyToID("_IsWhite");
    private readonly int StoneShaderHash = Shader.PropertyToID("_IsStone");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _entityMat = _spriteRenderer.material;
    }

    public void SetAlpha(float alpha)
    {
        Color color = _spriteRenderer.color;
        color.a = alpha;
        _spriteRenderer.color = color;
    }

    public void SkillEffect()
    {
        _skillEffect.SetActive(true);
    }

    public void EndSkillEffect()
    {
        _skillEffect.SetActive(false);
    }

    public void Hit()
    {
        StartCoroutine(HitRoutine());
    }
    public void OnStone(bool isOn)
    {
        _entityMat.SetFloat(StoneShaderHash, isOn ? 1 : 0);
    }

    private IEnumerator HitRoutine()
    {
        _entityMat.SetFloat(BlinkShaderHash, 1);
        yield return new WaitForSeconds(_whiteTime);
        _entityMat.SetFloat(BlinkShaderHash, 0);
    }
}
