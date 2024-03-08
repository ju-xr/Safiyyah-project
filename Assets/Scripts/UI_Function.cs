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

public class UI_Function : MonoBehaviour
{
    [Header("Data Collection")]
    public List<AudioVideoData> experimentData = new List<AudioVideoData>();
    public string AudioFolderName = "Default"; //Participant Name/Tag


    [Header("Audio File")]
    public string AudioPath;

    [Header("Audio and Video External Control")]
    public clipcontrol clipcontrol;
    public TMP_Text currentAudio;

    [Header("Read Audios")]
    public List<AudioClip> audioPlayList = new List<AudioClip>();
    public List<AudioClip> backupClips = new List<AudioClip>();
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

    //private List<string> fileNamesCSV = new List<string>();


    //public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {
        foreach (TMP_Dropdown dp in emptyAudioDropdown)
        {
            dp.interactable = false;
        }

    }


    public void LoadAudioBtn()
    {


        if (AudioFolderName != null)
        {
            //Find audio Folder, put into audioClip array
            AudioFolderName = AudioFolderName_text.text;
            AudioPath = Application.dataPath + "/Resources/Audios";
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

            if (randomOrderToggle.isOn)
            {
                //Debug.Log(randomOrderToggle.isOn);
                UnityEngine.Random.Range(0, audioPlayList.Count - 1);
                audioPlayList = audioPlayList.OrderBy(x => UnityEngine.Random.value).ToList();

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

                /*
                switch (i)
                {
                    case 0:
                        audio_OrderOption[i].value = i;
                        break;
                    case 1:
                        audio_OrderOption[i].value = i;
                        break;
                    case 2:
                        audio_OrderOption[i].value = i;
                        break;
                    case 3:
                        audio_OrderOption[i].ClearOptions();
                        audio_OrderOption[i].interactable = false;
                        break;
                    case 4:
                        audio_OrderOption[i].value = i - 1;
                        break;
                    case 5:
                        audio_OrderOption[i].value = i - 1;
                        break;
                    case 6:
                        audio_OrderOption[i].value = i - 1;
                        break;
                    case 7:
                        audio_OrderOption[i].ClearOptions();
                        audio_OrderOption[i].interactable = false;
                        break;
                    case 8:
                        audio_OrderOption[i].value = i - 2;
                        break;
                    case 9:
                        audio_OrderOption[i].value = i - 2;
                        break;

                }
                */

                if (randomOrderToggle.isOn)
                {
                    audioDropdown[i].interactable = false;

                }
                else
                {
                    audioDropdown[i].interactable = true;

                    //if (i!=3 || i != 7)
                    //{
                    //    audio_OrderOption[i].interactable = true;
                    //}
                    //else
                    //{
                    //    audio_OrderOption[i].interactable = false;
                    //}


                }
                //audio_OrderOption[i].Select(m_DropOptions[1]);
                audioDropdown[i].RefreshShownValue();

            }



        }




    }

    public void PressAudioDropdown(TMP_Dropdown currentDropdown)
    {
        if (!randomOrderToggle.isOn)
        {
            //audioClips[i] = audioClips[i+1];
            //audioClips is the list to play
            Debug.Log("dropdown chagne" + currentDropdown.value);
            //current dropdown button number
            int i = int.Parse(currentDropdown.tag);
            audioPlayList[i] = backupClips[currentDropdown.value];
            //loop the current dropdown button number
            //for (int i = 0; i < audio_OrderOption.Count; i++)
            //{
        }


    }

    public void StartExperiment()
    {
        clipcontrol.StartExperiment();

        // TODO: Start Record Data

        //for (int i = 0; i < audioPlayList.Count; i++)
        //{
        //    audioNames.text += audioPlayList[i].name + "\n";
        //}
    }

    public void PressCutClip()
    {
        clipcontrol.CutClip();
    }

}
