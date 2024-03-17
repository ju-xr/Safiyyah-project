using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using Unity.VisualScripting;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SocialPlatforms;
using System.Linq;


//Data collection

public class AudioVideoData
{
    public string VideoName;
    public int AudioNum;
    public string AudioName;

    public AudioVideoData(string videoName, int audioNum, string audioNames)
    {
        //Video order is fixed
        VideoName = videoName;
        AudioNum = audioNum;
        AudioName = audioNames;
    }
}

[System.Serializable]
public class UI_TextList
{
    public string Text;
    public int Time;

    public UI_TextList(string text, int time)
    {
        //Video order is fixed
        Text = text;
        Time = time;
    }
}


public class UI_Function : MonoBehaviour
{
    [Header("Data Collection")]
    public List<AudioVideoData> experimentData = new List<AudioVideoData>();
    public string AudioFolderName = "Default"; //Participant Name/Tag


    [Header("Audio File")]
    //public string AudioPath;

    [Header("Audio and Video External Control")]
    public clipcontrol clipcontrol;
    public TMP_Text currentAudio;
    public GameObject cameraCanvas;
    public TMP_Text pauseUI;
    //public TMP_Dropdown videoFolder;

    [Header("Read Audios")]
    public List<AudioClip> audioPlayList = new List<AudioClip>();
    public List<AudioClip> backupClips = new List<AudioClip>();
    public TMP_Dropdown participant_AudioDropdown;
    public Toggle randomOrderToggle;
    //public List<int> order = new List<int>();

    [Header("Folder Setting")]
    public TMP_Text audioNames;
    public TMP_Text videoNames;
    [SerializeField] private TMP_InputField AudioFolderName_text;

    [Header("10 Choose audio Dropdown")]
    public List<TMP_Dropdown> audioDropdown = new List<TMP_Dropdown>();
    [SerializeField] private TMP_Dropdown[] emptyAudioDropdown;
    List<string> m_DropOptions = new List<string>();

    public GameObject nextPanel;

    [Header("Pause UI")]
    public List<UI_TextList> amsterdam_UI = new List<UI_TextList>();
    public string[] amsterdamUI;
    public string[] newyorkUI;

    //private List<string> fileNamesCSV = new List<string>();


    //public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {
        foreach (TMP_Dropdown dp in emptyAudioDropdown)
        {
            dp.interactable = false;
        }

        LoadAudioBtn();
    }

    public void LoadAudio()
    {
        
    }

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

    public void StartExperiment()
    {
        clipcontrol.StartExperiment();

        // TODO: Start Record Data


    }

    public void PressCutClip()
    {
        clipcontrol.CutClip();
        if(clipcontrol.lastVideo)
        {
            nextPanel.SetActive(false);
        }
    }

}
