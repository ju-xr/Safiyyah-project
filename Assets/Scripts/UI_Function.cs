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

public class AudioOrder
{
    public int Order;
    public AudioClip AudioClips;

    public AudioOrder(int order, AudioClip audioClips)
    {
        Order = order;
        AudioClips = audioClips;
    }
}

public class UI_Function : MonoBehaviour
{
    public clipcontrol clipcontrol;

    public string AudioFolderName = "Default";
    public string AudioPath;

    [SerializeField] private TMP_InputField AudioFolderName_text;
    [SerializeField] private TMP_Dropdown[] audioDropdown;
    //[SerializeField] private List<List<TMP_Dropdown.OptionData>> optionList = new List<List<TMP_Dropdown.OptionData>>();

    private List<string> fileNamesCSV = new List<string>();
    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<AudioClip> backupClips = new List<AudioClip>();
    public List<int> order = new List<int>();

    public List<AudioOrder> audioOrder = new List<AudioOrder>();

    public TMP_Text audioNames;
    public TMP_Text videoNames;
    
    public Toggle randomOrderToggle;
    public TMP_Text currentAudio;

    public List<TMP_Dropdown> audio_OrderOption = new List<TMP_Dropdown>();
    List<string> m_DropOptions = new List<string>();
    //public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {


    }


    public void LoadAudioBtn()
    {

        m_DropOptions.Clear();
        if (AudioFolderName != null)
        {
            AudioFolderName = AudioFolderName_text.text;
            AudioPath = Application.dataPath + "/Resources/Audios";

            var audioClip = Resources.LoadAll("Audios/" + AudioFolderName, typeof(AudioClip));

            //foreach (var t in audioClip)
            //{
            //    Debug.Log(t.name);
            //    audioClips.Add((AudioClip)t);

            //    audioNames.text += audioClips. + t.name + "\n";
            //}
            audioClips.Clear();
            backupClips.Clear();
            order.Clear();
            audioNames.text = string.Empty;

            var optData = new List<Dropdown.OptionData>();

            for (int i = 0; i < audioClip.Length; i++)
            {

                Debug.Log(audioClip[i].name);

                //Load Audio
                audioClips.Add((AudioClip)audioClip[i]);
                backupClips.Add(audioClips[i]);
                order.Add(i + 1);           
                
                //audioNames.text += audioClip[i].name + "\n";
            }

            if (randomOrderToggle.isOn)
            {
                Debug.Log(randomOrderToggle.isOn);
                UnityEngine.Random.Range(0, audioClips.Count - 1);
                audioClips = audioClips.OrderBy(x => UnityEngine.Random.value).ToList();

            }

            for (int i = 0; i < audioClips.Count; i++)
            {
                m_DropOptions.Add(audioClips[i].name);
            }

            for (int i = 0; i < audio_OrderOption.Count; i++)
            {
                audio_OrderOption[i].ClearOptions();
                audio_OrderOption[i].AddOptions(m_DropOptions);

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
                        audio_OrderOption[i].value = i-1;
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

                if (randomOrderToggle.isOn)
                {
                    audio_OrderOption[i].interactable = false;

                }
                else
                {
                    if(i!=3 || i != 7)
                    {
                        audio_OrderOption[i].interactable = true;
                    }
                    else
                    {
                        audio_OrderOption[i].interactable = false;
                    }


                }
                //audio_OrderOption[i].Select(m_DropOptions[1]);
                audio_OrderOption[i].RefreshShownValue();

            }



            for (int i = 0; i < audioClips.Count; i++)
            {
                audioNames.text += audioClips[i].name + "\n";
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
            int i = int.Parse(currentDropdown.tag);


            switch (i)
            {
                case 0:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;
                case 1:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;
                case 2:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;
                case 4:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;
                case 5:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;
                case 6:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;
                case 8:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;
                case 9:
                    audioClips[i] = backupClips[currentDropdown.value];
                    break;

            }


            //for (int i = 0; i < audio_OrderOption.Count; i++)
            //{
        }


    }

    public void StartExperiment()
    {
        clipcontrol.StartExperiment();       
    }

    public void PressCutClip()
    {
        clipcontrol.CutClip();
    }

}
