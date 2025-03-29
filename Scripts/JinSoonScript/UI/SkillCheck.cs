using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    [SerializeField] private GameObject skillCheckObj;
    [SerializeField] private float speed = 10f;

    private RectTransform skillCheckArrowRect;
    private RectTransform skillCheckSucessAreaRect;

    private float skillCheckStartDelay = 0.5f;
    private float maxPos = 500;
    private bool isStartSkillCheck = false;
    private bool isEnd = false;
    private bool result;

    public bool IsEnd => isEnd;
    public bool Result => result;

    private void Awake()
    {
        skillCheckArrowRect = skillCheckObj.transform.Find("Arrow").GetComponent<RectTransform>();
        skillCheckSucessAreaRect = skillCheckObj.transform.Find("Success").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isStartSkillCheck)
        {
            skillCheckArrowRect.anchoredPosition += Vector2.right * speed;

            if (Input.GetKeyDown(KeyCode.Space) || skillCheckArrowRect.anchoredPosition.x > maxPos)
            {
                StartCoroutine(EndSkillCheckRoutine());
            }
        }
    }

    public void StartSkillCheck()
    {
        isEnd = false;
        StartCoroutine(DelayStartSkillCheck());
    }

    private IEnumerator DelayStartSkillCheck()
    {
        if (isStartSkillCheck)
            yield break;

        skillCheckObj.SetActive(true);
        yield return new WaitForSeconds(skillCheckStartDelay);
        isStartSkillCheck = true;
    }

    private IEnumerator EndSkillCheckRoutine()
    {
        isStartSkillCheck = false;

        float min = skillCheckSucessAreaRect.anchoredPosition.x - skillCheckSucessAreaRect.rect.width / 2;
        float max = skillCheckSucessAreaRect.anchoredPosition.x + skillCheckSucessAreaRect.rect.width / 2;
        float pos = skillCheckArrowRect.anchoredPosition.x;

        result = (pos >= min && pos <= max);

        if (pos >= min && pos <= max)
        {
            Debug.Log("성공이요");
        }
        else
        {
            Debug.Log("실패요");
        }

        yield return new WaitForSeconds(skillCheckStartDelay);

        skillCheckObj.SetActive(false);
        skillCheckArrowRect.anchoredPosition = new Vector2(0, skillCheckArrowRect.anchoredPosition.y);
        isEnd = true;
    }

    internal void Init()
    {
        isEnd = false;
    }
}
