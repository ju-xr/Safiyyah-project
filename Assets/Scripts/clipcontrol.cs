using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class clipcontrol : MonoBehaviour
{
    //Video Play Control
    [SerializeField] private TMP_Text VideoTimeTextBox;
    private const float videoPlayTime = 30f; // Play each video for 60 seconds

    // Start is called before the first frame update
    public VideoClip[] videoClips = new VideoClip[10];
    public AudioClip[] audioClips = new AudioClip[10];

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private int maxClipIndex = 9;


    
    private const float restPeriod = 10f; // Rest period of 15 seconds


    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayVideoSequence());
    }

    IEnumerator PlayVideoSequence()
    {
        while (true)
        {
            PlayVideoClip(currentClipIndex);

            yield return new WaitForSeconds(videoPlayTime);

            StopVideo();

            yield return new WaitForSeconds(restPeriod);

            
        }
    }

    void PlayVideoClip(int index)
    {
        if (index >= 0 && index < videoClips.Length)
        {
            videoPlayer.clip = videoClips[index];
            //audioSource.clip = audioClips.Length > index ? audioClips[index] : null;
            audioSource.clip = audioClips[index];
            videoPlayer.Play();
            if (audioSource.clip != null) audioSource.Play();
        }
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        currentClipIndex = (currentClipIndex + 1) % videoClips.Length;
    }

    public void PlayNextVideo()
    {
        StopCoroutine("PlayVideoSequence");
        StopVideo();
        StartCoroutine(PlayVideoSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
