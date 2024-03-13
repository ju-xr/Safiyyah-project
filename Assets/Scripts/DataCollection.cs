using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Globalization;
using UnityEngine.PlayerLoop;
using UnityEngine.Video;

public class DataCollection : MonoBehaviour
{
    public UI_Function UI_Function;
    public clipcontrol clipcontrol;

    string folderpath;
    bool startCollect;

    string participantFile = "";
    string participantName;
    string videoGroup;

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    public Transform headRotation;
    TextWriter tw;

    // Start is called before the first frame update
    void Start()
    {
        //filename = Application.dataPath + "/" + "ParticipantData" + "/" + UI_Function.AudioFolderName + ".csv";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            WriteParcipantCSV(); // in clipcontrol too
        }

        WriteUpdateData();
    } 

    //After start experiment
    public void StartDataCollection()
    {
        
        if (UI_Function.AudioFolderName == string.Empty)
        {
            participantName = "Default";
        }
        else
        {
            participantName = UI_Function.AudioFolderName;

        }

        if (clipcontrol.VideoFolderName == string.Empty)
        {
            videoGroup = "Default";
        }
        else
        {
            videoGroup = clipcontrol.VideoFolderName;

        }
        // Save setting panel data INTO file .csv when start button pressed 
        folderpath = Application.dataPath + "/" + "ParticipantData" + "/" + participantName;
        Directory.CreateDirectory(folderpath); // returns a DirectoryInfo object

        WriteParcipantCSV();

    }


    void WriteParcipantCSV()
    {
        //participantFile = folderpath + "/" + DateTime.UtcNow.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv";
        participantFile = folderpath + "/" + participantName + "_" + DateTime.UtcNow.ToString("yyyy-MM-dd") + ".csv";

        Debug.Log("Write _Data csv file data " + participantFile);

        if (!Directory.Exists(participantFile))
        {        
            //Write titles
            tw = new StreamWriter(participantFile, false);
            tw.WriteLine("Participant(Audio Folder): " + "," + participantName);
            tw.WriteLine("Video Folder:" + "," + videoGroup);
            tw.WriteLine("Audio Order");
            for (int i = 0; i < UI_Function.audioPlayList.Count; i++)
            {
                //UI_Function.audioNames.text += UI_Function.audioPlayList[i].name + "\n";
                tw.WriteLine(UI_Function.audioPlayList[i].name + ",");
            }
            tw.WriteLine("Time" + "," + "Millisecond" + "," + "VideoClip" + "," + "AudioClip" + "," + "Headrotatin X" + "," + "Headrotatin Y" + "," + "Headrotatin Y" + "," + "GazeObject");

            tw.Close(); 

            tw = new StreamWriter(participantFile, true);

            startCollect = true;
            //write data

            
        }
    }

    void WriteUpdateData()
    {
        if (startCollect)
        {
            //string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                           
            tw.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + "," + DateTime.UtcNow.Millisecond.ToString() + "," + videoPlayer.clip.name + ","+ audioSource.clip.name + "," + headRotation.rotation.x + "," + headRotation.rotation.y + "," + headRotation.rotation.z);

        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("colse writer");
        startCollect = false;
        tw.Close();
    }

}
