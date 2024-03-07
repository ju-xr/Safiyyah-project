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

    //Load Video
    public string VideoFolderName = "Default";
    public string VideoPath;
    [SerializeField] private TMP_InputField VideoFolderName_text;

    //Video Play Control
    [SerializeField] private TMP_Text VideoTimeTextBox;
    private const float videoPlayTime = 30f; // Play each video for 60 seconds


    // Start is called before the first frame update
    //public VideoClip[] videoClips = new VideoClip[10];
    public List<VideoClip> videoClips = new List<VideoClip>();
    //public AudioClip[] audioClips = new AudioClip[10];

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private int maxClipIndex = 9;

    private const float restPeriod = 10f; // Rest period of 15 seconds

    private bool currentVideoOver = false;
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

        //videoClip = videoClip.OrderBy(go => int.Parse(go.name.Substring(videoClip.Length))).ToArray();

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
        currentClipIndex = 0;
        videoPlayer.clip = videoClips[currentClipIndex];
        audioSource.clip = UI_Function.audioClips[currentClipIndex];
        videoPlayer.Play();
        audioSource.Play();
        UI_Function.currentAudio.text = UI_Function.audioClips[currentClipIndex].name;
    }

    IEnumerator PlayNextVideo()
    {
        //while (true)
        //{
        //    PlayVideoClip(currentClipIndex);

        //    yield return new WaitForSeconds(videoPlayTime);

        //    StopVideo();

        //    yield return new WaitForSeconds(restPeriod);


        //}

        currentClipIndex++;

        Debug.Log("next video: " + !currentVideoOver);

        switch (currentClipIndex)
        {
            case 4:
                videoPlayer.clip = videoClips[currentClipIndex];
                videoPlayer.Play();
                break;
            case 8:
                videoPlayer.clip = videoClips[currentClipIndex];
                videoPlayer.Play();
                break;
            default:
                videoPlayer.clip = videoClips[currentClipIndex];
                audioSource.clip = UI_Function.audioClips[currentClipIndex];
                videoPlayer.Play();
                audioSource.Play();
                break;
        }
        UI_Function.currentAudio.text = UI_Function.audioClips[currentClipIndex].name;

        yield return new WaitForSeconds(3);
        //currentVideoOver = false;

        //if trigger video is over, then next video

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
        currentClipIndex++;
        videoPlayer.clip = videoClips[currentClipIndex];
        audioSource.clip = UI_Function.audioClips[currentClipIndex];
        videoPlayer.Play();
        audioSource.Play();
        UI_Function.currentAudio.text = UI_Function.audioClips[currentClipIndex].name;
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
        currentClipIndex = (currentClipIndex + 1) % videoClips.Count;
    }

    //public void PlayNextVideo()
    //{
    //    StopCoroutine("PlayVideoSequence");
    //    StopVideo();
    //    StartCoroutine(PlayVideoSequence());
    //}

    // Update is called once per frame
}
