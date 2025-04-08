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

    [Header("10 Choose audio Dropdown")]
    public List<TMP_Dropdown> audioDropdown = new List<TMP_Dropdown>();
    [SerializeField] private TMP_Dropdown[] emptyAudioDropdown;
    List<string> m_DropOptions = new List<string>();

    void Start()
    {
        LoadAudioBtn();
    }

    //public void LoadAudioBtn()
    //{
    //    AudioFolderName = participant_AudioDropdown.captionText.text;
    //    var audioFile = Resources.LoadAll("Audios/" + AudioFolderName, typeof(AudioClip));

    //    audioPlayList.Clear();
    //    backupClips.Clear();

    //    for (int i = 0; i < audioFile.Length; i++)
    //    {
    //        audioPlayList.Add((AudioClip)audioFile[i]);
    //        backupClips.Add((AudioClip)audioFile[i]);
    //    }
    //}

    public void LoadAudioBtn()
    {
        //if (!randomOrderToggle.isOn)
        //{
        //Find audio Folder, put into audioClip array
        AudioFolderName = participant_AudioDropdown.captionText.text;
        //AudioPath = Application.dataPath + "/Resources/Audios";
        var audioFile = Resources.LoadAll("Audios/" + AudioFolderName, typeof(AudioClip));

        //Clear loaded audio clips and audio names List each button click
        //audioNames.text = string.Empty;
        audioPlayList.Clear();
        backupClips.Clear();
        m_DropOptions.Clear();
        //order.Clear();

        var optData = new List<Dropdown.OptionData>();

        //Loop Audio File in Resources Folder with the ascending order
        for (int i = 0; i < audioFile.Length; i++)
        {
            //Debug.Log(audioClip[i].name);
            //add folder audio into playable audio clips
            audioPlayList.Add((AudioClip)audioFile[i]);
            backupClips.Add((AudioClip)audioFile[i]);
            //audioNames.text += audioClip[i].name + "\n";
            //order.Add(i + 1);                          

            m_DropOptions.Add(audioPlayList[i].name);
        }

        //Choose audio function
        for (int i = 0; i < audioDropdown.Count; i++)
        {
            audioDropdown[i].ClearOptions();
            audioDropdown[i].AddOptions(m_DropOptions);

            audioDropdown[i].value = i;

            foreach (TMP_Dropdown dp in emptyAudioDropdown)
            {
                dp.ClearOptions();
                dp.interactable = false;
            }

            //if (randomOrderToggle.isOn)
            //{
            //    audioDropdown[i].interactable = false;

            //}
            //else
            //{
            audioDropdown[i].interactable = true;
            //}
            //audio_OrderOption[i].Select(m_DropOptions[1]);
            audioDropdown[i].RefreshShownValue();

        }
        #region random function
        //}
        //else
        //{
        //    AudioFolderName = participant_AudioDropdown.captionText.text;
        //    //AudioPath = Application.dataPath + "/Resources/Audios";
        //    var audioFile = Resources.LoadAll("Audios/" + AudioFolderName, typeof(AudioClip));

        //    //Clear loaded audio clips and audio names List each button click
        //    //audioNames.text = string.Empty;
        //    audioPlayList.Clear();
        //    backupClips.Clear();
        //    m_DropOptions.Clear();


        //    var optData = new List<Dropdown.OptionData>();

        //    //Loop Audio File in Resources Folder with the ascending order
        //    for (int i = 0; i < audioFile.Length; i++)
        //    {
        //        //Debug.Log(audioClip[i].name);


        //        //add folder audio into playable audio clips
        //        audioPlayList.Add((AudioClip)audioFile[i]);
        //        backupClips.Add((AudioClip)audioFile[i]);
        //        //audioNames.text += audioClip[i].name + "\n";
        //        //order.Add(i + 1);                          

        //        m_DropOptions.Add(audioPlayList[i].name);
        //    }

        //    //Debug.Log(randomOrderToggle.isOn);
        //    UnityEngine.Random.Range(0, audioPlayList.Count - 1);
        //    audioPlayList = audioPlayList.OrderBy(x => UnityEngine.Random.value).ToList();

        //    //Choose audio function
        //    for (int i = 0; i < audioDropdown.Count; i++)
        //    {
        //        audioDropdown[i].ClearOptions();
        //        audioDropdown[i].AddOptions(m_DropOptions);

        //        audioDropdown[i].value = i;

        //        foreach (TMP_Dropdown dp in emptyAudioDropdown)
        //        {
        //            dp.ClearOptions();
        //            dp.interactable = false;
        //        }

        //        //if (randomOrderToggle.isOn)
        //        //{
        //            audioDropdown[i].interactable = false;

        //        //}
        //        //else
        //        //{
        //        //    audioDropdown[i].interactable = true;
        //        //}
        //        //audio_OrderOption[i].Select(m_DropOptions[1]);
        //        audioDropdown[i].RefreshShownValue();

        //    }


        //}

        #endregion
    }

    public void StartExperiment()
    {
        clipcontrol.StartExperiment();
    }

    public void PressAudioDropdown(TMP_Dropdown currentDropdown)
    {
        //if (!randomOrderToggle.isOn)
        //{
        //audioClips is the list to play
        //Debug.Log("dropdown chagne" + currentDropdown.value);
        //current dropdown button number
        int i = int.Parse(currentDropdown.tag);
        audioPlayList[i] = backupClips[currentDropdown.value];
        //}


    }
} 