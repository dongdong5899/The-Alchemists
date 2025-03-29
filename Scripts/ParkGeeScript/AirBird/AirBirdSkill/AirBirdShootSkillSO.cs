using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/AirBird/AirBirdShoot")]
public class AirBirdShootSkillSO : SkillSO
{
    [Header("AirBirdInfo")]
    public Stat shootSpeed;
    public Stat damage;

    private void OnEnable()
    {
        skill = new AirBirdShootSkill();
    }
}
