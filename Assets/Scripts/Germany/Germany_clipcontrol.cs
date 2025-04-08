using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Germany_clipcontrol : MonoBehaviour
{
    [Header("Video Control")]
    public List<VideoClip> videoClips = new List<VideoClip>();
    public VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private int currentVideoIndex = 0;
    bool startExp;
    public int video_PlayTime = 60; // 每个视频播放1分钟

    [Header("Public Variables")]
    public Germany_UI_Function UI_Function;
    public Germany_DataCollection dataCollection;
    public Material skyMat;
    public RenderTexture skybox;
    public RenderTexture black;
    public Texture2D picture;

    public GameObject lslController_Obj;
    private lslController _lslController;

    void Start()
    {
        LoadVideo();
        audioSource = GetComponent<AudioSource>();
        skyMat.SetTexture("_MainTex", black);
        UI_Function.cameraCanvas.SetActive(true);
        videoPlayer.loopPointReached += CheckOver;

        _lslController = lslController_Obj.GetComponent<lslController>();
        _lslController.SendTestTrig("Session Start");
        print("LSL sent at start");
    }

    public void LoadVideo()
    {
        videoClips.Clear();
        var videoClip = Resources.LoadAll("Videos/Germany", typeof(VideoClip));

        for (int i = 0; i < videoClip.Length; i++)
        {
            videoClips.Add((VideoClip)videoClip[i]);
        }

        UI_Function.videoNames.text = string.Empty;
        for (int i = 0; i < videoClips.Count; i++)
        {
            UI_Function.videoNames.text += videoClips[i].name + "\n";
        }
    }

    void VideoCountDown(int currentSecond)
    {
        if (videoPlayer.clockTime >= currentSecond && currentVideoIndex < 19)
        {
            StartCoroutine(PlayNextVideo());
        }

        if (videoPlayer.clockTime >= currentSecond && currentVideoIndex == 19)
        {
            StartCoroutine(StopLastVideo());
        }
    }

    void Update()
    {
        if (startExp)
        {
            VideoCountDown(video_PlayTime);
        }
    }

    public void StartExperiment()
    {
        startExp = true;
        StartCoroutine(PlayFirstVideo());
    }

    IEnumerator PlayFirstVideo()
    {
        MatchPauseUI(0);
        yield return new WaitForSeconds(UI_Function.germany_UI[0].Time);
        UI_Function.cameraCanvas.SetActive(false);
        skyMat.SetTexture("_MainTex", picture);
        yield return new WaitForSeconds(UI_Function.photoTime_UI);
        skyMat.SetTexture("_MainTex", black);
        UI_Function.cameraCanvas.SetActive(true);
        MatchPauseUI(1);
        yield return new WaitForSeconds(UI_Function.germany_UI[1].Time);
        skyMat.SetTexture("_MainTex", skybox);
        UI_Function.cameraCanvas.SetActive(false);

        dataCollection.StartDataCollection();
        currentVideoIndex = 0;
        MatchVideoAudio();
        
        // TODO: 在这里添加LSL信号发送代码
        // SendLSLSignal(videoPlayer.clip.name + "_start");
        
        PlayMatchedVA();
    }

    IEnumerator PlayNextVideo()
    {
        // TODO: 在这里添加LSL信号发送代码
        // SendLSLSignal(videoPlayer.clip.name + "_end");
        
        videoPlayer.Stop();
        audioSource.Stop();
        videoPlayer.clip = null;
        audioSource.clip = null;
        skyMat.SetTexture("_MainTex", black);
        videoPlayer.targetTexture = black;
        MatchPauseUI(2);
        
        yield return new WaitForSeconds(UI_Function.germany_UI[currentVideoIndex + 2].Time);
        
        currentVideoIndex++;
        MatchVideoAudio();
        videoPlayer.targetTexture = skybox;
        skyMat.SetTexture("_MainTex", skybox);
        UI_Function.cameraCanvas.SetActive(false);

        // TODO: 在这里添加LSL信号发送代码
        // SendLSLSignal(videoPlayer.clip.name + "_start");
        _lslController.SendTestTrig(videoPlayer.clip.name + "_start");
        print("LSL sent start");

        PlayMatchedVA();
        yield return new WaitForSeconds(video_PlayTime);
    }

    IEnumerator StopLastVideo()
    {
        // TODO: 在这里添加LSL信号发送代码
        // SendLSLSignal(videoPlayer.clip.name + "_end");
        //lsl testing
        _lslController.SendTestTrig(videoPlayer.clip.name + "_end");
        print("LSL sent ending");

        videoPlayer.Stop();
        audioSource.Stop();
        videoPlayer.clip = null;
        audioSource.clip = null;
        skyMat.SetTexture("_MainTex", black);
        MatchPauseUI(2);
        
        yield return new WaitForSeconds(UI_Function.germany_UI[currentVideoIndex + 2].Time);
        
        MatchPauseUI(3);
        yield return new WaitForSeconds(UI_Function.germany_UI[22].Time);
        Application.Quit();
    }

    void MatchVideoAudio()
    {
        videoPlayer.clip = videoClips[currentVideoIndex];
        audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
    }

    void PlayMatchedVA()
    {
        skyMat.SetTexture("_MainTex", skybox);
        videoPlayer.Play();
        audioSource.Play();
    }

    void MatchPauseUI(int i)
    {
        UI_Function.cameraCanvas.SetActive(true);
        UI_Function.pauseUI.text = UI_Function.germany_UI[currentVideoIndex + i].Text;
    }

    void CheckOver(VideoPlayer vp)
    {
        print("Video Is Over");
    }
} 