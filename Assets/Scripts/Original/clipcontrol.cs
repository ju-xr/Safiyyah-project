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

/// <summary>
/// 简化版的数据收集类，用于实验数据收集
/// </summary>
public class Original_DataCollection : MonoBehaviour
{
    public void StartDataCollection() { }
}

/// <summary>
/// 视频播放控制类，负责管理视频播放、音频同步、UI显示等实验流程
/// </summary>
public class clipcontrol : MonoBehaviour
{
    [Header("Experimentor Edit Parameters")]
    public int video_PlayTime;  // 视频播放时长设置

    [Header("Video Control")]
    public GameObject[] colliderTags;  // 碰撞器标签数组，用于不同视频场景的交互
    public List<VideoClip> videoClips = new List<VideoClip>();  // 存储所有视频片段

    public VideoPlayer videoPlayer;  // 视频播放器组件
    protected AudioSource audioSource;  // 音频源组件
    protected int currentVideoIndex = 0;  // 当前播放的视频索引
    protected bool startExp;  // 实验开始标志
    protected string VideoFolderName;  // 视频文件夹名称

    [Header("Public Variables")]
    public UI_Function UI_Function;  // UI功能控制组件
    public Original_DataCollection dataCollection;  // 数据收集组件
    public bool lastVideo = false;  // 是否为最后一个视频
    public TMP_Dropdown videoFolderDropdown;  // 视频文件夹下拉选择框
    public Material skyMat;  // 天空盒材质
    public RenderTexture skybox;  // 天空盒渲染纹理
    public RenderTexture black;  // 黑色渲染纹理
    public Texture2D picture;  // 图片纹理

    /// <summary>
    /// 初始化组件和设置
    /// </summary>
    void Start()
    {
        LoadVideo();
        audioSource = GetComponent<AudioSource>();
        skyMat.SetTexture("_MainTex", black);
        UI_Function.cameraCanvas.SetActive(true);
        videoPlayer.loopPointReached += CheckOver;
    }

    /// <summary>
    /// 加载指定文件夹中的所有视频
    /// </summary>
    public void LoadVideo()
    {
        videoClips.Clear();
        VideoFolderName = videoFolderDropdown.captionText.text;
        var videoClip = Resources.LoadAll("Videos/" + VideoFolderName, typeof(VideoClip));

        for (int i = 0; i < videoClip.Length; i++)
        {
            videoClips.Add((VideoClip)videoClip[i]);
        }

        // 更新UI显示视频名称列表
        UI_Function.videoNames.text = string.Empty;
        for (int i = 0; i < videoClips.Count; i++)
        {
            UI_Function.videoNames.text += videoClips[i].name + "\n";
        }
    }

    /// <summary>
    /// 视频倒计时控制，管理视频切换
    /// </summary>
    protected virtual void VideoCountDown(int currentSecond)
    {
        if (videoPlayer.clockTime >= currentSecond && currentVideoIndex < videoClips.Count - 1)
        {
            StartCoroutine(PlayNextVideo());
        }

        if (videoPlayer.clockTime >= currentSecond && currentVideoIndex == videoClips.Count - 1)
        {
            print("The last video played");
            StartCoroutine(StopLastVideo());
        }
    }

    /// <summary>
    /// 每帧更新，检查视频播放状态
    /// </summary>
    protected virtual void Update()
    {
        if (startExp)
        {
            VideoCountDown(video_PlayTime);
        }
    }

    /// <summary>
    /// 开始实验
    /// </summary>
    public void StartExperiment()
    {
        startExp = true;
        StartCoroutine(PlayFirstVideo());
    }

    /// <summary>
    /// 播放第一个视频的协程
    /// </summary>
    IEnumerator PlayFirstVideo()
    {
        MatchPauseUI(0);
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[0].Time);
        UI_Function.cameraCanvas.SetActive(false);
        skyMat.SetTexture("_MainTex", picture);
        yield return new WaitForSeconds(UI_Function.photoTime_UI);
        skyMat.SetTexture("_MainTex", black);
        UI_Function.cameraCanvas.SetActive(true);
        MatchPauseUI(1);
        yield return new WaitForSeconds(UI_Function.amsterdam_UI[1].Time);
        skyMat.SetTexture("_MainTex", skybox);
        UI_Function.cameraCanvas.SetActive(false);

        dataCollection.StartDataCollection();
        currentVideoIndex = 0;
        MatchVideoAndAudio();
        PlayMatchedVA();
    }

    /// <summary>
    /// 播放下一个视频的协程
    /// </summary>
    protected virtual IEnumerator PlayNextVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        videoPlayer.clip = null;
        audioSource.clip = null;
        skyMat.SetTexture("_MainTex", black);
        videoPlayer.targetTexture = black;
        MatchPauseUI(2);
        
        // 根据视频组别选择对应的UI时间
        if (VideoFolderName == "New York")
        {
            yield return new WaitForSeconds(UI_Function.newyork_UI[currentVideoIndex + 2].Time);
        }
        else if (VideoFolderName == "Germany")
        {
            yield return new WaitForSeconds(UI_Function.germany_UI[currentVideoIndex + 2].Time);
        }
        else
        {
            yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex + 2].Time);
        }
        
        currentVideoIndex++;
        MatchVideoAndAudio();
        videoPlayer.targetTexture = skybox;
        skyMat.SetTexture("_MainTex", skybox);
        UI_Function.cameraCanvas.SetActive(false);
        PlayMatchedVA();
        yield return new WaitForSeconds(video_PlayTime);
    }

    /// <summary>
    /// 停止最后一个视频的协程
    /// </summary>
    protected virtual IEnumerator StopLastVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        videoPlayer.clip = null;
        audioSource.clip = null;
        skyMat.SetTexture("_MainTex", black);
        MatchPauseUI(2);
        
        // 根据视频组别选择对应的UI时间
        if (VideoFolderName == "New York")
        {
            yield return new WaitForSeconds(UI_Function.newyork_UI[currentVideoIndex + 2].Time);
        }
        else if (VideoFolderName == "Germany")
        {
            yield return new WaitForSeconds(UI_Function.germany_UI[22].Time);
        }
        else
        {
            yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex + 3].Time);
        }
        MatchPauseUI(3);
        if (VideoFolderName == "New York")
        {
            yield return new WaitForSeconds(UI_Function.newyork_UI[currentVideoIndex + 3].Time);
        }
        else if (VideoFolderName == "Germany")
        {
            yield return new WaitForSeconds(UI_Function.germany_UI[22].Time);
        }
        else
        {
            yield return new WaitForSeconds(UI_Function.amsterdam_UI[currentVideoIndex + 3].Time);
        }
        Application.Quit();
    }

    /// <summary>
    /// 匹配视频和音频，设置对应的碰撞器
    /// </summary>
    protected virtual void MatchVideoAndAudio()
    {
        // 重置所有碰撞器状态
        foreach (GameObject go in colliderTags)
        {
            go.SetActive(false);
        }

        // 根据视频组别选择对应的视频和音频
        if (VideoFolderName == "Germany")
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
        }
        else
        {
            // 根据不同视频索引设置对应的碰撞器和音频
            if (currentVideoIndex < 10)
            {
                // 原有的0-9视频处理逻辑
                switch (currentVideoIndex)
                {
                    case 0:
                        colliderTags[0].SetActive(true);
                        videoPlayer.clip = videoClips[currentVideoIndex];
                        audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                        break;
                    case 1:
                        colliderTags[1].SetActive(true);
                        colliderTags[14].SetActive(true);
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
            else
            {
                videoPlayer.clip = videoClips[currentVideoIndex];
                // 对于额外的视频，使用默认的音频匹配逻辑
                if (currentVideoIndex < UI_Function.audioPlayList.Count)
                {
                    audioSource.clip = UI_Function.audioPlayList[currentVideoIndex];
                }
            }
        }
    }

    /// <summary>
    /// 播放匹配的视频和音频
    /// </summary>
    protected virtual void PlayMatchedVA()
    {
        skyMat.SetTexture("_MainTex", skybox);
        if (currentVideoIndex < 10)
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
        else
        {
            videoPlayer.Play();
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }

    /// <summary>
    /// 匹配暂停UI显示
    /// </summary>
    protected virtual void MatchPauseUI(int i)
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
    }

    /// <summary>
    /// 切换到下一个视频片段
    /// </summary>
    public virtual void CutClip()
    {
        currentVideoIndex++;
        videoPlayer.clip = videoClips[currentVideoIndex];
        MatchVideoAndAudio();
    }

    /// <summary>
    /// 视频播放完成的回调函数
    /// </summary>
    protected virtual void CheckOver(VideoPlayer vp)
    {
        print("Video Is Over");
    }

    /// <summary>
    /// 停止当前视频播放
    /// </summary>
    public virtual void StopVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        currentVideoIndex = (currentVideoIndex + 1) % videoClips.Count;
    }
}
