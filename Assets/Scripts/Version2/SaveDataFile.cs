using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.Video;

public class SaveDataFile : MonoBehaviour
{
    [Header("Scripts Components")]
    [SerializeField]
    private TextTime textTimeContainer;
    [SerializeField]
    private RenderLoader renderLoader;
    [SerializeField]
    private AudioLoader audioLoader;
    [SerializeField]
    private VideoLoader videoLoader;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private VideoPlayer videoPlayer;

    string participantName;
    string folderpath;

    string participantFile = "";
    TextWriter tw;

    public Transform headRotation;

    bool startCollect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WriteUpdateData();
    }

    public void StartDataCollection()
    {
        participantName = audioLoader.participantFolder_Dropdown.captionText.text;
        folderpath = Application.dataPath + "/" + "ParticipantData" + "/" + participantName;
        Directory.CreateDirectory(folderpath);

        WriteParcipantCSV();
    }


    void WriteParcipantCSV()
    {
        participantFile = folderpath + "/" + participantName + DateTime.UtcNow.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv";

        Debug.Log("Write Germany Data csv file: " + participantFile);

        tw = new StreamWriter(participantFile, false);
        tw.WriteLine("Participant(Audio Folder): " + "," + participantName);
        tw.WriteLine("Video Folder:" + "," + "_Germany_");
        tw.WriteLine("Audio Order");
        for (int i = 0; i < audioLoader.audioPlayList.Count; i++)
        {
            tw.WriteLine(audioLoader.audioPlayList[i].name + ",");
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
