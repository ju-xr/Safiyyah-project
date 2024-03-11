using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;


public class clipcontrol : MonoBehaviour
{
    public UI_Function UI_Function;

    [Header("Video Control")]
    public string VideoFolderName = "Default";
    public string VideoPath;
    [SerializeField] private TMP_InputField VideoFolderName_text;

    //Video Play Control
    [SerializeField] private TMP_Text VideoTimeTextBox;
    private const float videoPlayTime = 30f; // Play each video for 60 seconds

    // Start is called before the first frame update
    public List<VideoClip> videoClips = new List<VideoClip>();

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private int currentVideoIndex = 0;

    private const float restPeriod = 10f; // Rest period of 15 seconds

    public bool lastVideo = false;
    public GameObject[] WPgoGroupList = new GameObject[9];

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        //StartCoroutine(PlayVideoSequence()); 

        //Load Video to clips
        VideoFolderName = VideoFolderName_text.text;
        VideoPath = Application.dataPath + "/Resources/Videos";
        var videoClip = Resources.LoadAll("Videos/" + VideoFolderName, typeof(VideoClip));

        for (int i = 0; i < videoClip.Length; i++)
        {
            Debug.Log(videoClip[i].name);
            videoClips.Add((VideoClip)videoClip[i]);

            //audioOrder.Add(new AudioOrder(i + 1, (AudioClip)audioClip[i]));
            //audioNames.text += audioClip[i].name + "\n";
        }

        UI_Function.videoNames.text = string.Empty;
        for (int i = 0; i < videoClips.Count; i++)
        {
            UI_Function.videoNames.text += videoClips[i].name + "\n";
        }

        //videoClips = videoClips.OrderBy(go => float.Parse(go.name)).ToList();
        //videoClips = videoClips.OrderBy(go => int.Parse(go.name.Substring(videoClip.Length))).ToArray();
        //videoPlayer.loopPointReached += CheckOver;
    }

    void Update()
    {
        VideoCountDown(30);
    }

    public void StartExperiment()
    {
        currentVideoIndex = 0;
        videoPlayer.clip = videoClips[currentVideoIndex];
        audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
        videoPlayer.Play();
        audioSource.Play();
        UI_Function.currentAudio.text = UI_Function.audioPlayList[currentVideoIndex].name;
    }

    IEnumerator PlayNextVideo()
    {
        currentVideoIndex++;

        MatchVideoToAudio();
                //UI_Function.currentAudio.text = UI_Function.audioPlayList[currentVideoIndex].name;

        yield return new WaitForSeconds(3);
        //currentVideoOver = false;

        //if trigger video is over, then next video

    }

    void MatchVideoToAudio()
    {
        switch (currentVideoIndex)
        {
            case 0:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                break;
            case 1:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                break;
            case 2:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                break;
            case 3:
                videoPlayer.clip = videoClips[currentVideoIndex];
                videoPlayer.Play();
                audioSource.mute = true;
                //mute video
                break;
            case 4:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                break;
            case 5:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                break;
            case 6:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                break;
            case 7:
                videoPlayer.clip = videoClips[currentVideoIndex];
                videoPlayer.Play();
                audioSource.mute = true;
                break;
            case 8:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 2];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                lastVideo = true;
                break;
            case 9:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 2];
                audioSource.mute = false;
                videoPlayer.Play();
                audioSource.Play();
                break;
        }
    }

    void VideoCountDown(int currentSecond)
    {
            //print("is playing" + videoPlayer.clockTime);
            if (videoPlayer.clockTime >= currentSecond)
            {
                print("Current time is 30, play the next time");
                StartCoroutine(PlayNextVideo());
            } 
    }

    public void CutClip()
    {
        currentVideoIndex++;
        videoPlayer.clip = videoClips[currentVideoIndex];

        MatchVideoToAudio();
        //UI_Function.currentAudio.text = videoPlayer.clip.name + " = " + audioSource.clip.name;

    }


    //void CheckOver(VideoPlayer vp)
    //{
    //    print("Video Is Over");
    //    //StartCoroutine(PlayNextVideo());
    //}

    //void PlayVideoClip(int index)
    //{
    //    if (index >= 0 && index < videoClips.Count)
    //    {
    //        videoPlayer.clip = videoClips[index];
    //        //audioSource.clip = audioClips.Length > index ? audioClips[index] : null;
    //        audioSource.clip = audioClips[index];
    //        videoPlayer.Play();
    //        if (audioSource.clip != null) audioSource.Play();
    //    }
    //}

    public void StopVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        currentVideoIndex = (currentVideoIndex + 1) % videoClips.Count;
    }

    //public void PlayNextVideo()
    //{
    //    StopCoroutine("PlayVideoSequence");
    //    StopVideo();
    //    StartCoroutine(PlayVideoSequence());
    //}

    // Update is called once per frame
}
