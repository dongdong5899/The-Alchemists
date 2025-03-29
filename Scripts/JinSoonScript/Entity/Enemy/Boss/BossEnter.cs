using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    public Entity boss;
    public GameObject frontWall;
    public GameObject backWall;

    private void HandleDie(Vector2 vector)
    {
        EndBoss();
    }

    public void StartBoss()
    {
        backWall.SetActive(true);
        boss.gameObject.SetActive(true);
        boss.healthCompo.OnDie += HandleDie;
        UIManager.Instance.GetUI(UIType.BossHp).Open();
    }

    public void EndBoss()
    {
        frontWall.SetActive(false);
        UIManager.Instance.GetUI(UIType.BossHp).Close();
    }
}
