using UnityEngine;
using System.IO;
using System;

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

        Debug.Log("NoteInfo가 파일에 저장되었습니다: " + filePath);
    }

    public void LoadData(string fileName, ref string songName, ref Vector3[] notePosition, ref Vector3[] spikePosition)
    {
        string filePath = "Assets/Data/" + fileName;

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            NoteInfo loadedData = JsonUtility.FromJson<NoteInfo>(jsonData);

            // songName 값을 복사
            songName = loadedData.songName;

            // notePosition 배열의 내용을 복사
            if (loadedData.positionInfo != null)
            {
                Array.Resize(ref notePosition, loadedData.positionInfo.Length);
                Array.Copy(loadedData.positionInfo, notePosition, loadedData.positionInfo.Length);
            }

            // spikePosition 배열의 내용을 복사
            if (loadedData.spikePositionInfo != null)
            {
                Array.Resize(ref spikePosition, loadedData.spikePositionInfo.Length);
                Array.Copy(loadedData.spikePositionInfo, spikePosition, loadedData.spikePositionInfo.Length);
            }
        }
        else
        {
            Debug.Log(filePath + " Nothing Found");
        }
    }
}