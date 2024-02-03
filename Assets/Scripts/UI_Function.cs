using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;


//Data collection

public class UI_Function : MonoBehaviour
{
    public string PartcipantCode = "Empty";
    public string AudioPath = "C:\\Users\\fangm\\Desktop\\Audios";
    public string[] audioOrder;

    [SerializeField] private TMP_Text ParcitipantTextBox;
    [SerializeField] private TMP_Text AudioPathTextBox;

    //public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {

        AudioPathTextBox.text = AudioPath;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton() 
    {
        //TODO Record the parameters into clipcontrol script
        PartcipantCode = ParcitipantTextBox.text;
        AudioPath = AudioPathTextBox.text;
        Debug.Log(AudioPath);
        LoadAudioBtn();
        // TODO start video

    }

    public void LoadAudioBtn()
    {
        //DirectoryInfo dir = new DirectoryInfo(AudioPathTextBox.text);
        //FileInfo[] info = dir.GetFiles("*.*");
        //foreach (FileInfo f in info)
        //{
        //   Debug.Log(f.FullName);
        //}
        

        DirectoryInfo dir = new DirectoryInfo(AudioPath);
        //string filePath = Path.GetFullPath(AudioPathTextBox.text);

        foreach (var file in dir.GetFiles("*.wav"))
        {
            Debug.Log(file);
        }

        //StreamReader sr = new StreamReader(filePath);
    }




}
