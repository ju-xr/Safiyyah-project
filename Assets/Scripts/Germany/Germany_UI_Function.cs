using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

[System.Serializable]
public class GermanyTextList
{
    public string Text;
    public int Time;

    public GermanyTextList(string text, int time)
    {
        Text = text;
        Time = time;
    }
}

[System.Serializable]
public class GermanyAudioVideoData
{
    public string VideoName;
    public int AudioNum;
    public string AudioName;

    public GermanyAudioVideoData(string videoName, int audioNum, string audioNames)
    {
        VideoName = videoName;
        AudioNum = audioNum;
        AudioName = audioNames;
    }
}

public class Germany_UI_Function : MonoBehaviour
{
    [Header("UI Settings")]
    public int photoTime_UI;
    public List<GermanyTextList> germany_UI = new List<GermanyTextList>();  // 23段UI文字

    [Header("Data Collection")]
    public List<GermanyAudioVideoData> experimentData = new List<GermanyAudioVideoData>();
    string AudioFolderName = "Default"; //Participant Name/Tag

    [Header("Public Variables")]
    public Germany_clipcontrol clipcontrol;
    public TMP_Text currentAudio;
    public GameObject cameraCanvas;
    public TMP_Text pauseUI;

    [Header("Read Audios")]
    public List<AudioClip> audioPlayList = new List<AudioClip>();
    public List<AudioClip> backupClips = new List<AudioClip>();
    public TMP_Dropdown participant_AudioDropdown;
    public TMP_Text videoNames;

    [Header("Folder Setting")]
    [SerializeField] private TMP_InputField AudioFolderName_text;

    void Start()
    {
        LoadAudioBtn();
    }

    public void LoadAudioBtn()
    {
        AudioFolderName = participant_AudioDropdown.captionText.text;
        var audioFile = Resources.LoadAll("Audios/" + AudioFolderName, typeof(AudioClip));

        audioPlayList.Clear();
        backupClips.Clear();

        for (int i = 0; i < audioFile.Length; i++)
        {
            audioPlayList.Add((AudioClip)audioFile[i]);
            backupClips.Add((AudioClip)audioFile[i]);
        }
    }

    public void StartExperiment()
    {
        clipcontrol.StartExperiment();
    }
} 