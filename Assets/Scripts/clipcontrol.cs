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
    [Header("Experimentor Edit Parameters")]
    public int video_PlayTime;

    [Header("Video Control")]
    public GameObject[] colliderTags;
    public List<VideoClip> videoClips = new List<VideoClip>();

    public VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private int currentVideoIndex = 0;
    bool startExp;
    string VideoFolderName;

    [Header("Public Variables")]
    public UI_Function UI_Function;
    public DataCollection dataCollection;
    public bool lastVideo = false;
    public TMP_Dropdown videoFolderDropdown;

    void Start()
    {
        //videoPlayer = GetComponent<VideoPlayer>();
        LoadVideo();
        audioSource = GetComponent<AudioSource>();
        //Load Video to clips
        videoPlayer.loopPointReached += CheckOver;
    }

    public void LoadVideo()
    {
        videoClips.Clear();
        VideoFolderName = videoFolderDropdown.captionText.text;
        //Debug.Log(videoFolderDropdown.captionText.text);
        //VideoPath = Application.dataPath + "/Resources/Videos";
        var videoClip = Resources.LoadAll("Videos/" + VideoFolderName, typeof(VideoClip));

        for (int i = 0; i < videoClip.Length; i++)
        {
            //Debug.Log(videoClip[i].name);
            videoClips.Add((VideoClip)videoClip[i]);
            //audioNames.text += audioClip[i].name + "\n";
        }

        UI_Function.videoNames.text = string.Empty;
        for (int i = 0; i < videoClips.Count; i++)
        {
            UI_Function.videoNames.text += videoClips[i].name + "\n";
        }
    }

    void VideoCountDown(int currentSecond)
    {
        //Debug.Log(videoPlayer.clockTime);
        //print("is playing" + videoPlayer.clockTime);
        if (videoPlayer.clockTime >= currentSecond && currentVideoIndex<9)
        {
            print("Current time is 30, play the next time");
            StartCoroutine(PlayNextVideo());
        }

        if (videoPlayer.clockTime >= currentSecond && currentVideoIndex == 9)
        {
            print("The last video played");
            StartCoroutine(StopLastVideo());
        }
    }

    void Update()
    {
        if (startExp)
        {
            VideoCountDown(video_PlayTime);//videoLength
        }

    }

    public void StartExperiment()
    {
        startExp = true;
        StartCoroutine(PlayFirstVideo());
    }

    IEnumerator PlayFirstVideo()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        UI_Function.cameraCanvas.SetActive(true);

        MatchPauseUI(0);
        //UI_Function.pauseUI.text = UI_Function.amsterdam_UI[0].Text;
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[0].Time);
        MatchPauseUI(1);
        //UI_Function.pauseUI.text = UI_Function.amsterdam_UI[1].Text;
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[1].Time);

        Camera.main.clearFlags = CameraClearFlags.Skybox;
        UI_Function.cameraCanvas.SetActive(false);

        dataCollection.StartDataCollection();
        currentVideoIndex = 0;
        MatchVideoAudio();
        PlayMatchedVA();

    }

    IEnumerator PlayNextVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        videoPlayer.clip = null;
        audioSource.clip = null;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        MatchPauseUI(2); //current video index
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex + 2].Time);
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        UI_Function.cameraCanvas.SetActive(false);

        currentVideoIndex++;
        MatchVideoAudio();
        PlayMatchedVA(); // videoPlayer.Play();
        yield return new WaitForSeconds(video_PlayTime); //videoLength
        //UI_Function.currentAudio.text = UI_Function.audioPlayList[currentVideoIndex].name;

        //currentVideoOver = false;

        //if trigger video is over, then next video

    }

    IEnumerator StopLastVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        videoPlayer.clip = null;
        audioSource.clip = null;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        MatchPauseUI(2); //current video index
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex +2].Time);
        UI_Function.pauseUI.text = UI_Function.amsterdam_UI[currentVideoIndex + 3].Text;
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex + 3].Time);
        Application.Quit();

    }

    void MatchVideoAudio()
    {
        foreach (GameObject go in colliderTags)
        {
            go.SetActive(false);
        }


        switch (currentVideoIndex)
        {
            case 0:
                colliderTags[0].SetActive(true);
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                break;
            case 1:
                colliderTags[1].SetActive(true);
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                break;
            case 2:
                if (VideoFolderName == "New York")
                {
                    colliderTags[2].SetActive(true);
                }
                else
                {
                    colliderTags[3].SetActive(true);
                }
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                break;
            case 3:
                colliderTags[4].SetActive(true);
                videoPlayer.clip = videoClips[currentVideoIndex];
                break;
            case 4:
                colliderTags[5].SetActive(true);
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                break;
            case 5:
                if (VideoFolderName == "New York")
                {
                    colliderTags[6].SetActive(true);
                }
                else
                {
                    colliderTags[7].SetActive(true);
                }
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                break;
            case 6:
                colliderTags[8].SetActive(true);
                colliderTags[9].SetActive(true);
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                break;
            case 7:
                if (VideoFolderName == "New York")
                {
                    colliderTags[10].SetActive(true);
                }
                else
                {
                    colliderTags[11].SetActive(true);
                }
                videoPlayer.clip = videoClips[currentVideoIndex];
                break;
            case 8:
                colliderTags[12].SetActive(true);
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 2];
                lastVideo = true;
                break;
            case 9:
                colliderTags[13].SetActive(true);
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 2];
                break;
        }
    }

    void PlayMatchedVA()
    {
        switch (currentVideoIndex)
        {
            case 3:
                videoPlayer.Play();
                audioSource.Stop();
                break;
            case 7:
                videoPlayer.Play();
                audioSource.Stop();
                break;
            case 9:
                videoPlayer.Play();
                audioSource.Play();

                break;
            default:
                videoPlayer.Play();
                audioSource.Play();
                break;
        }
    }

    void MatchPauseUI(int i)
    {
        UI_Function.cameraCanvas.SetActive(true);
        if(VideoFolderName == "New York")
        {
            UI_Function.pauseUI.text = UI_Function.newyork_UI[currentVideoIndex + i].Text;
        }
        else
        {
            UI_Function.pauseUI.text = UI_Function.amsterdam_UI[currentVideoIndex + i].Text;
        }
        

        //switch (currentVideoIndex)
        //{
        //    case 0:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = UI_Function.newyorkUI[currentVideoIndex+2];
        //            //"video 1-2";
        //        break;
        //    case 1:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 2-3";
        //        break;
        //    case 2:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 3-4";
        //        break;
        //    case 3:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 4-5";
        //        break;
        //    case 4:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 5-6";
        //        break;
        //    case 5:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 6-7";
        //        break;
        //    case 6:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 7-8";
        //        break;
        //    case 7:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 8-9";
        //        break;
        //    case 8:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 9-10";
        //        break;
        //    case 9:
        //        UI_Function.cameraCanvas.SetActive(true);
        //        UI_Function.pauseUI.text = "video 10-";
        //        break;
        //}
    }


    public void CutClip()
    {
        currentVideoIndex++;
        videoPlayer.clip = videoClips[currentVideoIndex];

        MatchVideoAudio();
        //UI_Function.currentAudio.text = videoPlayer.clip.name + " = " + audioSource.clip.name;

    }


    void CheckOver(VideoPlayer vp)
    {
        print("Video Is Over");
        //StartCoroutine(PlayNextVideo());
    }

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
