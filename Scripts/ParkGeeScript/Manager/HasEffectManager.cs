using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasEffectManager : Singleton<HasEffectManager>
{
    [SerializeField] private Image[] _blank;
    [SerializeField] private Image[] _border;

    [SerializeField] private Sprite[] _borderSprites;
    [SerializeField] private Sprite _dashEffectSprite;
    [SerializeField] private Sprite _armorUpEffectSprite;
    [SerializeField] private Sprite _upDamageEffectSprite;

    private List<int> _activeDashIndexes = new List<int>();
    private List<int> _activeArmorIndexes = new List<int>();
    private List<int> _activeDamageIndexes = new List<int>();

    public void DashOn(int a)
    {
        if (_activeDashIndexes.Count + _activeArmorIndexes.Count < _blank.Length)
        {
            int currentIndex = _activeDashIndexes.Count + _activeArmorIndexes.Count;
            _blank[currentIndex].gameObject.SetActive(true);
            _blank[currentIndex].sprite = _dashEffectSprite;
            _border[currentIndex].sprite = _borderSprites[a];
            _activeDashIndexes.Add(currentIndex);
            Debug.Log("DashOn Index: " + currentIndex);
        }
    }

    public void DashOff()
    {
        if (_activeDashIndexes.Count > 0)
        {
            int lastIndex = _activeDashIndexes[_activeDashIndexes.Count - 1];
            ClearEffectAtIndex(lastIndex);
            _activeDashIndexes.RemoveAt(_activeDashIndexes.Count - 1);
            Debug.Log("DashOff Index: " + lastIndex);
        }
    }

    public void ArmorOn(int a)
    {
        if (_activeDashIndexes.Count + _activeArmorIndexes.Count < _blank.Length)
        {
            int currentIndex = _activeDashIndexes.Count + _activeArmorIndexes.Count;
            _blank[currentIndex].gameObject.SetActive(true);
            _blank[currentIndex].sprite = _armorUpEffectSprite;
            _border[currentIndex].sprite = _borderSprites[a];
            _activeArmorIndexes.Add(currentIndex);
            Debug.Log("ArmorOn Index: " + currentIndex);
        }
    }

    public void ArmorOff()
    {
        if (_activeArmorIndexes.Count > 0)
        {
            int lastIndex = _activeArmorIndexes[_activeArmorIndexes.Count - 1];
            ClearEffectAtIndex(lastIndex);
            _activeArmorIndexes.RemoveAt(_activeArmorIndexes.Count - 1);
            Debug.Log("ArmorOff Index: " + lastIndex);
        }
    }

    public void DamageOn(int a)
    {
        //if(_activeDamageIndexes.Count + _activeArmorIndexes.Count)
        //{

        //}
    }

    public void DamageOff()
    {

    }

    private void ClearEffectAtIndex(int index)
    {
        _blank[index].gameObject.SetActive(false);
        _blank[index].sprite = null;
        _border[index].sprite = null;
    }
}