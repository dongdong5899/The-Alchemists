using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIconController : MonoBehaviour
{
    [SerializeField] private StatusIconSpriteSO _statusIconSpriteSO;
    [SerializeField] private StatusIcon _statusIconPrfab;

    private Dictionary<StatusBuffEffectEnum, StatusIcon> _buffStatusIcon = new Dictionary<StatusBuffEffectEnum, StatusIcon>();
    private Dictionary<StatusDebuffEffectEnum, StatusIcon> _debuffStatusIcon = new Dictionary<StatusDebuffEffectEnum, StatusIcon>();

    private void Awake()
    {
        PlayerManager.Instance.Player.OnStatusChanged += HandleStatusChangedEvent;
    }

    private void HandleStatusChangedEvent(int enumNum, int level, bool isBuff, bool isApply)
    {
        if (isApply)
        {
            StatusIcon statusIcon = Instantiate(_statusIconPrfab, transform);
            if (isBuff)
            {
                StatusBuffEffectEnum statusEnum = (StatusBuffEffectEnum)enumNum;
                statusIcon.Init(_statusIconSpriteSO.GetStatusIconSprite(statusEnum), _statusIconSpriteSO.GetLevelOutlineIconSprite(level));
                if (_buffStatusIcon.ContainsKey(statusEnum))
                    _buffStatusIcon[statusEnum] = statusIcon;
                else
                    _buffStatusIcon.Add(statusEnum, statusIcon);
            }
            else
            {
                StatusDebuffEffectEnum statusEnum = (StatusDebuffEffectEnum)enumNum;
                statusIcon.Init(_statusIconSpriteSO.GetStatusIconSprite(statusEnum), _statusIconSpriteSO.GetLevelOutlineIconSprite(level));
                if (_debuffStatusIcon.ContainsKey(statusEnum))
                    _debuffStatusIcon[statusEnum] = statusIcon;
                else
                    _debuffStatusIcon.Add(statusEnum, statusIcon);
            }
        }
        else
        {
            if (isBuff && _buffStatusIcon.ContainsKey((StatusBuffEffectEnum)enumNum))
                _buffStatusIcon[(StatusBuffEffectEnum)enumNum].End();
            else if (_debuffStatusIcon.ContainsKey((StatusDebuffEffectEnum)enumNum))
                _debuffStatusIcon[(StatusDebuffEffectEnum)enumNum].End();
        }
    }
}