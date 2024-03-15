using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Globalization;
using UnityEngine.PlayerLoop;
using UnityEngine.Video;
using UnityEngine.SocialPlatforms;

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

    float range = 100;
    string gazeTag;

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
        participantFile = folderpath + "/" + participantName + "_" + DateTime.UtcNow.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv";

        Debug.Log("Write _Data csv file data " + participantFile);

        //if (!Directory.Exists(participantFile))
        //{        
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

            
        //}
    }

    void WriteUpdateData()
    {
        if (startCollect)
        {
            //string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",

            GazeRay();

            //tw.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + "," + DateTime.UtcNow.Millisecond.ToString() + "," + videoPlayer.clip.name + "," + audioSource.clip.name + "," + headRotation.rotation.x + "," + headRotation.rotation.y + "," + headRotation.rotation.z + "," + gazeTag);


            if (videoPlayer.clip == null || audioSource.clip == null)
            {
                tw.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + "," 
                    + DateTime.UtcNow.Millisecond.ToString() + "," 
                    + "Not play video" + "," + "Not play audio" + "," 
                    + headRotation.rotation.x + "," 
                    + headRotation.rotation.y + "," 
                    + headRotation.rotation.z + "," 
                    + gazeTag);
            }
            else
            {
                tw.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + "," 
                    + DateTime.UtcNow.Millisecond.ToString() + "," 
                    + videoPlayer.clip.name + "," 
                    + audioSource.clip.name + "," 
                    + headRotation.rotation.x + "," 
                    + headRotation.rotation.y + "," 
                    + headRotation.rotation.z + "," 
                    + gazeTag);

            }

        }
    }


     void GazeRay()
    {
        Vector3 direction = Vector3.forward;
        Ray gaze = new Ray(Camera.main.transform.position, Camera.main.transform.TransformDirection(direction * range));
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(direction * range));

        if (Physics.Raycast(gaze, out RaycastHit hit, range))
        {
            gazeTag = hit.collider.tag;
        }
        else
        {
            gazeTag = "Not Look At";
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("colse writer");
        startCollect = false;
        tw.Close();
    }

}
