using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class VideoControl_v2 : MonoBehaviour
{
    [Header("Video Components")]
    public VideoPlayer videoPlayer;  // 视频播放器组件
    protected AudioSource audioSource;  // 音频源组件

    [Header("Video Clips - Germany")]
    [SerializeField]
    public List<VideoClip> videoClips = new List<VideoClip>();  // 存储所有视频片段

    [Header("Video Play")]
    protected int currentVideoIndex = 0;  // 当前播放的视频索引

    [Header("Video Renderer")]
    public Material skyMat;  // 天空盒材质
    public RenderTexture skybox;  // 天空盒渲染纹理
    public RenderTexture black;  // 黑色渲染纹理
    public Texture2D intro_Picture_3307;  // 图片纹理

    [Header("Video Transition Text")]
    [SerializeField]
    private TMP_Text cameraText;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        RenderCameraTexture();

        videoPlayer.loopPointReached += CheckOver;
    }

    private void RenderCameraTexture()
    {
        skyMat.SetTexture("_MainTex", black);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator StartIntroSession()
    {
        cameraText.text = "Please wait, the study will begin soon. Music should begin playing, so please take this time to adjust the volume to a comfortable level and keep from changing the volume during the experience. Let the experimenter know once your volume is set.";
        yield return new WaitForSeconds(60f);
        cameraText.text = "";

    }

    private void CheckOver(VideoPlayer vp)
    {
        print("Video Is Over");
    }

}
