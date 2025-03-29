using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject[] panel;

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("���а��� ������");
    }

    public void LoadGame()
    {
        Debug.Log("�����Ѱ����� �̵�");
    }

    public void GameExit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void ShowSetting()
    {
        panel[0].SetActive(true);
    }

    public void OffSetting()
    {
        panel[0].SetActive(false);
    }
}
