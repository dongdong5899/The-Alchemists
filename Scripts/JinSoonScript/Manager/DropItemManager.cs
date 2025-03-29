using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DropItemManager : Singleton<DropItemManager>
{
    private string _path;
    [SerializeField]private List<int> _itemsHaveBeenGet;

    private void Awake()
    {
        //_path = Path.Combine(Application.dataPath, "GatheredItems.txt");
        //Load();
    }


    public void IndicateItemPanel(ItemSO item)
    {
        //if (_itemsHaveBeenGet.Contains(item.id)) return;

        //ItemGatherPanel panal = UIManager.Instance.GetUI(UIType.ItemGather) as ItemGatherPanel;
        //panal.Init(item);
        //panal.Open();

        //_itemsHaveBeenGet.Add(item.id);
        //Save();
    }

    private void Save()
    {
        StringBuilder sb = new StringBuilder();

        foreach (int item in _itemsHaveBeenGet)
        {
            sb.AppendLine(item.ToString());
        }

        Debug.Log(sb.ToString());
        File.WriteAllText(_path, sb.ToString());
    }

    private void Load()
    {
        _itemsHaveBeenGet = new List<int>();
        if (File.Exists(_path) == false)
            Save();

        string data = File.ReadAllText(_path);
        string[] datas = data.Split('\n');


        foreach (string str in datas)
        {
            if (string.IsNullOrEmpty(str)) continue;

            _itemsHaveBeenGet.Add(int.Parse(str));
        }
    }
}
