using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StatusBuffEffectSprite
{
    public Sprite sprite;
    public StatusBuffEffectEnum statusEnum;
}

[Serializable]
public struct StatusDebuffEffectSprite
{
    public Sprite sprite;
    public StatusDebuffEffectEnum statusEnum;
}

[CreateAssetMenu(menuName = "SO/StatusIconSprite")]
public class StatusIconSpriteSO : ScriptableObject
{
    [Header("StatusIcon")]
    [SerializeField] private StatusBuffEffectSprite[] _buffStatusIconSprite;
    [SerializeField] private StatusDebuffEffectSprite[] _debuffStatusIconSprite;

    [Header("LevelOutlineIcon")]
    [SerializeField] private Sprite[] _levelOutlineIconSprite;

    private Dictionary<StatusBuffEffectEnum, Sprite> _statusBuffEffectSprite = new Dictionary<StatusBuffEffectEnum, Sprite>();
    private Dictionary<StatusDebuffEffectEnum, Sprite> _statusDebuffEffectSprite = new Dictionary<StatusDebuffEffectEnum, Sprite>();

    private void OnEnable()
    {
        foreach (StatusBuffEffectSprite enumSprite in _buffStatusIconSprite)
        {
            _statusBuffEffectSprite.Add(enumSprite.statusEnum, enumSprite.sprite);
        }
        foreach (StatusDebuffEffectSprite enumSprite in _debuffStatusIconSprite)
        {
            _statusDebuffEffectSprite.Add(enumSprite.statusEnum, enumSprite.sprite);
        }
    }

    public Sprite GetStatusIconSprite(StatusBuffEffectEnum statusEnum)
    {
        return _statusBuffEffectSprite[statusEnum];
    }
    public Sprite GetStatusIconSprite(StatusDebuffEffectEnum statusEnum)
    {
        return _statusDebuffEffectSprite[statusEnum];
    }
    public Sprite GetLevelOutlineIconSprite(int level)
    {
        return _levelOutlineIconSprite[level];
    }
}
