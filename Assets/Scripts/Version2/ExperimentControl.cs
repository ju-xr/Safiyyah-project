using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class ExperimentControl : MonoBehaviour
{

    [Header("Scene Transition Text")]
    [SerializeField]
    private TMP_Text cameraText;

    [Header("Player Components")]
    public VideoPlayer videoPlayer;  // 视频播放器组件
    protected AudioSource audioSource;  // 音频源组件
    int currentVideoIndex = 0;

    [Header("Assigned Components")]
    [SerializeField] 
    private TextTime textTimeContainer;
    [SerializeField] 
    private RenderLoader renderLoader;
    [SerializeField]
    private AudioLoader audioLoader;
    [SerializeField]
    private VideoLoader videoLoader;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        videoPlayer = GetComponent<VideoPlayer>();

        renderLoader.skyMat.SetTexture("_MainTex", renderLoader.black);
        videoPlayer.targetTexture = renderLoader.black;

        videoPlayer.loopPointReached += CheckOver;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickStartExperiment()
    {

        StartCoroutine(StartIntroSession());
    }

    IEnumerator StartIntroSession()
    {
        
        
        cameraText.text = textTimeContainer.Text_intro;
        yield return new WaitForSeconds(textTimeContainer.Time_intro); // 60
        cameraText.text = textTimeContainer.Text_introPhoto;
        yield return new WaitForSeconds(textTimeContainer.Time_introPhoto); // 15
        cameraText.text = null;
        renderLoader.skyMat.SetTexture("_MainTex", textTimeContainer.intro_Picture_3307);
        yield return new WaitForSeconds(textTimeContainer.Time_intro_Picture_3307); //60

        StartCoroutine(PlayVideoSession());
    }

    IEnumerator PlayVideoSession()
    {
        print("Next Video");
        ResetVideoSession();

        var textInfo = textTimeContainer.TextTime_session[currentVideoIndex].Text;
        cameraText.text = textInfo;
        yield return new WaitForSeconds(textTimeContainer.TextTime_session[currentVideoIndex].Time);

        cameraText.text = null;
        renderLoader.skyMat.SetTexture("_MainTex", renderLoader.skybox);
        videoPlayer.targetTexture = renderLoader.skybox;

        videoPlayer.clip = videoLoader.videoClips[currentVideoIndex];
        audioSource.clip = audioLoader.audioPlayList[currentVideoIndex];
        videoPlayer.Play();
        audioSource.Play();

    }

    void ResetVideoSession()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        videoPlayer.clip = null;
        audioSource.clip = null;
        renderLoader.skyMat.SetTexture("_MainTex", renderLoader.black);
        videoPlayer.targetTexture = renderLoader.black;
    }

    private void CheckOver(VideoPlayer vp)
    {
        print("Video Is Over");
        currentVideoIndex++;

        if (currentVideoIndex < videoLoader.videoClips.Count)
        {
            StartCoroutine(PlayVideoSession()); // 继续播放下一个
        }
        else
        {
            print("All Video Is Over");
            StartCoroutine(AllVideoEnd());
            // 可切 UI、跳转等操作
        }
    }

    IEnumerator AllVideoEnd()
    {
        cameraText.text = textTimeContainer.Text_endsession;
        yield return new WaitForSeconds(textTimeContainer.Time_endsession); // 15
        cameraText.text = textTimeContainer.Text_over;
        yield return new WaitForSeconds(textTimeContainer.Time_over); // 60
    }

}
