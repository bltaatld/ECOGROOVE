using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CopyJsonFile : MonoBehaviour
{
    void Start()
    {
        // Resources 폴더 내의 모든 JSON 파일을 가져옵니다.
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("JsonFiles");

        // JSON 파일을 저장할 폴더 경로를 지정합니다.
        string persistentDataPath = Application.persistentDataPath + "/";

        // JSON 파일을 Application.persistentDataPath로 복사합니다.
        foreach (TextAsset jsonFile in jsonFiles)
        {
            string filePath = Path.Combine(persistentDataPath, jsonFile.name + ".json");

            // 파일이 이미 존재하는지 확인하고, 존재하지 않으면 복사합니다.
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, jsonFile.text);
                Debug.Log("JSON 파일 복사됨: " + jsonFile.name);
            }
            else
            {
                Debug.Log("JSON 파일 이미 존재함: " + jsonFile.name);
            }
        }
    }
}