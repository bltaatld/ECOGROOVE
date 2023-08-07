using UnityEngine;
using System.IO;

public class NoteInfoSaver : MonoBehaviour
{
    public void SaveData(NoteInfo data, string fileName)
    {
        string folderPath = Application.dataPath + "/Data/";
        string filePath = Path.Combine(folderPath, fileName);
        string jsonData = JsonUtility.ToJson(data, true);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(filePath, jsonData);

        Debug.Log("NoteInfo�� ���Ͽ� ����Ǿ����ϴ�: " + filePath);
    }
}