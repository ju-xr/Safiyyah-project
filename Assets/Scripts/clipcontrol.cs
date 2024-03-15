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
    public DataCollection dataCollection;

    [Header("Video Control")]
    public string VideoFolderName = "Default";
    public string VideoPath;
    [SerializeField] private TMP_InputField VideoFolderName_text;
    public int videoLength;
    [SerializeField] private TMP_Text VideoTimeTextBox;
    private const float videoPlayTime = 30f; // Play each video for 60 seconds

    // Start is called before the first frame update
    public List<VideoClip> videoClips = new List<VideoClip>();

    public VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private int currentVideoIndex = 0;

    private const float restPeriod = 10f; // Rest period of 15 seconds

    public bool lastVideo = false;
    public GameObject[] WPgoGroupList = new GameObject[9];
    public GameObject[] colliderTags;


    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
        //videoLength = 3;
        //StartCoroutine(PlayVideoSequence()); 

        //Load Video to clips
        VideoFolderName = VideoFolderName_text.text;
        VideoPath = Application.dataPath + "/Resources/Videos";
        var videoClip = Resources.LoadAll("Videos/" + VideoFolderName, typeof(VideoClip));

        for (int i = 0; i < videoClip.Length; i++)
        {
            //Debug.Log(videoClip[i].name);
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
        videoPlayer.loopPointReached += CheckOver;
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
        VideoCountDown(videoLength);//videoLength
    }

    public void StartExperiment()
    {
        StartCoroutine(PlayFirstVideo());
    }

    IEnumerator PlayFirstVideo()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        UI_Function.cameraCanvas.SetActive(true);

        UI_Function.pauseUI.text = UI_Function.amsterdam_UI[0].Text;
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[0].Time);
        UI_Function.pauseUI.text = UI_Function.amsterdam_UI[1].Text;
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
        MatchPauseUI(); //current video index
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex + 2].Time);
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        UI_Function.cameraCanvas.SetActive(false);

        currentVideoIndex++;
        MatchVideoAudio();
        PlayMatchedVA(); // videoPlayer.Play();
        yield return new WaitForSeconds(videoLength); //videoLength
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
        MatchPauseUI(); //current video index
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex +2].Time);
        UI_Function.pauseUI.text = UI_Function.amsterdam_UI[currentVideoIndex + 3].Text;
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex + 3].Time);
        Application.Quit();

    }

    void MatchVideoAudio()
    {
        foreach ()
        {
            if (currentVideoIndex >= videoLength)
        }


        switch (currentVideoIndex)
        {
            case 0:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                break;
            case 1:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                break;
            case 2:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                break;
            case 3:
                videoPlayer.clip = videoClips[currentVideoIndex];
                break;
            case 4:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                break;
            case 5:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                break;
            case 6:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 1];
                break;
            case 7:
                videoPlayer.clip = videoClips[currentVideoIndex];
                break;
            case 8:
                videoPlayer.clip = videoClips[currentVideoIndex];
                audioSource.clip = UI_Function.audioPlayList[currentVideoIndex - 2];
                lastVideo = true;
                break;
            case 9:
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

    void MatchPauseUI()
    {
        UI_Function.cameraCanvas.SetActive(true);
        UI_Function.pauseUI.text = UI_Function.newyorkUI[currentVideoIndex + 2];

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
