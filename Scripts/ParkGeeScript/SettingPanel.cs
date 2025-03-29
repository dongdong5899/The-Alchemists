using DG.Tweening;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    //지금 너무 코드 별론데 최대한 머리짜내봄 미안해
    [SerializeField] private GameObject[] uiPrefab;
    [SerializeField] private GameObject canvas;

    private GameObject escPanelInstance;
    private GameObject inventoryInstance;
    private GameObject settingInstance;

    private RectTransform rectTransform;

    private Sequence seq;

    [SerializeField] private bool escPanelState = false;
    [SerializeField] private bool inventoryPanelState = false;

    private void Awake()
    {
        seq = DOTween.Sequence();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escPanelInstance == null && inventoryPanelState == false)
            {
                escPanelInstance = Instantiate(uiPrefab[0]) as GameObject;
                escPanelInstance.transform.SetParent(canvas.transform, false);
                rectTransform = escPanelInstance.GetComponent<RectTransform>();
                seq.Append(rectTransform.DOMove(new Vector3(2000, 450, 0), 1).SetEase(Ease.OutBack));
                escPanelState = true;
                Time.timeScale = 0;
                //Debug.Log(setPanelInstance);
            }
            else
            {
                UIOff(escPanelInstance);
                escPanelState = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryInstance == null && escPanelState == false)
            {
                inventoryInstance = Instantiate(uiPrefab[1]) as GameObject;
                inventoryInstance.transform.SetParent(canvas.transform, false);
                rectTransform = inventoryInstance.GetComponent<RectTransform>();
                seq.Append(rectTransform.DOSizeDelta(new Vector2(1800, 950), 1).SetEase(Ease.OutBack));
                Debug.Log(seq.Append(rectTransform.DOSizeDelta(new Vector2(1800, 950), 1).SetEase(Ease.OutBack)));
                inventoryPanelState = true;
                Time.timeScale = 0;
                //rectTransform.sizeDelta = new Vector2(1600,800);
            }
            else
            {
                UIOff(inventoryInstance);
                inventoryPanelState = false;
            }
        }
    }

    public void SettingOn()
    {
        settingInstance = Instantiate(uiPrefab[2]) as GameObject;
        settingInstance.transform.SetParent(canvas.transform, false);
        rectTransform = escPanelInstance.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(1000, -12, 0);
        Debug.Log(uiPrefab[2]);
    }

    /*private void UIOn(GameObject ui, GameObject[] prefab, int array)
    {
        
    } 일단 살려놓음*/

    private void UIOff(GameObject ui)
    {
        Destroy(ui);
        //state = false;
        Time.timeScale = 1;
    }
}
