using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResetSaveData : MonoBehaviour
{
    private string _localPath;

    private void Start()
    {
        _localPath = Path.Combine(Application.persistentDataPath, "SaveDatas/bins");

        if (Directory.Exists(_localPath)) //폴더 탐색
        {
            File.SetAttributes(_localPath, FileAttributes.Normal); //폴더 읽기 전용 해제
            Directory.Delete(_localPath, true);
        }
    }
}
