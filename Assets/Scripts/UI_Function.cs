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
    public List<int> order = new List<int>();

    public List<AudioOrder> audioOrder = new List<AudioOrder>();

    public TMP_Text audioNames;
    public Toggle randomOrderToggle;

    //public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {
        //var audioClip = Resources.LoadAll("Audios", typeof(AudioClip));

        //foreach (var t in audioClip)
        //{
        //    Debug.Log(t.name);
        //    audioClips.Add((AudioClip)t);
        //}
    }


    // Update is called once per frame
    void Update()
    {

    }



    public void LoadAudioBtn()
    {


        if(AudioFolderName != null)
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
            order.Clear();
            audioNames.text = string.Empty;

            for (int i = 0; i < audioClip.Length; i++)
            {

                Debug.Log(audioClip[i].name);

                    audioClips.Add((AudioClip)audioClip[i]);
                    order.Add(i + 1);
                //audioOrder.Add(new AudioOrder(i + 1, (AudioClip)audioClip[i]));

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
                audioNames.text += audioClips[i].name + "\n";
            }



        }




    }

    public void StartExperiment()
    {
        clipcontrol.StartExperiment();
    }
}
