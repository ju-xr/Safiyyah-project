using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.Video;

public class Germany_DataCollection : MonoBehaviour
{
    public Germany_UI_Function UI_Function;
    public Germany_clipcontrol clipcontrol;

    string folderpath;
    bool startCollect;

    string participantFile = "";
    string participantName;
    string videoGroup = "Germany";

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    public Transform headRotation;
    TextWriter tw;

    void Start()
    {
        // 初始化
    }

    void FixedUpdate()
    {
        WriteUpdateData();
    } 

    public void StartDataCollection()
    {
        participantName = UI_Function.participant_AudioDropdown.captionText.text;
        folderpath = Application.dataPath + "/" + "ParticipantData" + "/" + participantName;
        Directory.CreateDirectory(folderpath);

        WriteParcipantCSV();
    }

    void WriteParcipantCSV()
    {
        participantFile = folderpath + "/" + participantName + "_Germany_" + DateTime.UtcNow.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv";

        Debug.Log("Write Germany Data csv file: " + participantFile);

        tw = new StreamWriter(participantFile, false);
        tw.WriteLine("Participant(Audio Folder): " + "," + participantName);
        tw.WriteLine("Video Folder:" + "," + videoGroup);
        tw.WriteLine("Audio Order");
        for (int i = 0; i < UI_Function.audioPlayList.Count; i++)
        {
            tw.WriteLine(UI_Function.audioPlayList[i].name + ",");
        }

        // Germany版本只收集头部旋转数据
        tw.WriteLine("Time" + "," + "Millisecond" + "," + "VideoClip" + "," + "AudioClip" + "," + "Headrotation X" + "," + "Headrotation Y" + "," + "Headrotation Z");

        tw.Close(); 
        tw = new StreamWriter(participantFile, true);
        startCollect = true;
    }

    void WriteUpdateData()
    {
        if (startCollect)
        {
            if (videoPlayer.clip == null || audioSource.clip == null)
            {
                tw.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + "," 
                    + DateTime.UtcNow.Millisecond.ToString() + "," 
                    + "Not play video" + "," + "Not play audio" + "," 
                    + headRotation.rotation.x + "," 
                    + headRotation.rotation.y + "," 
                    + headRotation.rotation.z);
            }
            else
            {
                tw.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + "," 
                    + DateTime.UtcNow.Millisecond.ToString() + "," 
                    + videoPlayer.clip.name + "," 
                    + audioSource.clip.name + "," 
                    + headRotation.rotation.x + "," 
                    + headRotation.rotation.y + "," 
                    + headRotation.rotation.z);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (tw != null)
        {
            Debug.Log("Close Germany data writer");
            startCollect = false;
            tw.Close();
        }
    }
} 