using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CopyJsonFile : MonoBehaviour
{
    void Start()
    {
        // Resources ���� ���� ��� JSON ������ �����ɴϴ�.
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("JsonFiles");

        // JSON ������ ������ ���� ��θ� �����մϴ�.
        string persistentDataPath = Application.persistentDataPath + "/";

        // JSON ������ Application.persistentDataPath�� �����մϴ�.
        foreach (TextAsset jsonFile in jsonFiles)
        {
            string filePath = Path.Combine(persistentDataPath, jsonFile.name + ".json");

            // ������ �̹� �����ϴ��� Ȯ���ϰ�, �������� ������ �����մϴ�.
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, jsonFile.text);
                Debug.Log("JSON ���� �����: " + jsonFile.name);
            }
            else
            {
                Debug.Log("JSON ���� �̹� ������: " + jsonFile.name);
            }
        }
    }
}