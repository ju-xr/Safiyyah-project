using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Linq;
using System;


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

        videoPlayer.loopPointReached += CheckOver;
    }

    public void StartExperiment()
    {

        StartCoroutine(PlayVideoSequence(0));
    }

    IEnumerator PlayVideoSequence(int currentIndex)
    {
        //while (true)
        //{
        //    PlayVideoClip(currentClipIndex);

        //    yield return new WaitForSeconds(videoPlayTime);

        //    StopVideo();

        //    yield return new WaitForSeconds(restPeriod);


        //}
        videoPlayer.clip = videoClips[currentIndex];
        audioSource.clip = UI_Function.audioClips[currentIndex];
        videoPlayer.Play();
        audioSource.Play();

        yield return new WaitForSeconds(3);

        //if trigger video is over, then next video
        //if (currentVideoOver)
        //{
        //    currentIndex++;
        //    NextVideo(currentIndex);
        //    yield return new WaitForSeconds(3);
        //}

    }

    void NextVideo(int currentIndex)
    {
            videoPlayer.clip = videoClips[currentIndex];
            audioSource.clip = UI_Function.audioClips[currentIndex];
            videoPlayer.Play();
            audioSource.Play();
            currentVideoOver = false;
    }


    void CheckOver(VideoPlayer vp)
    {
        print("Video Is Over");
        currentVideoOver = true;
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
        currentClipIndex = (currentClipIndex + 1) % videoClips.Count;
    }

    //public void PlayNextVideo()
    //{
    //    StopCoroutine("PlayVideoSequence");
    //    StopVideo();
    //    StartCoroutine(PlayVideoSequence());
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
