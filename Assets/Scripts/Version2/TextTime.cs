using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class Scene_TextTime
{
    public string Text;
    public int Time;

    public Scene_TextTime(string text, int time)
    {
        //Video order is fixed
        Text = text;
        Time = time;
    }
}


public class TextTime : MonoBehaviour
{
    [Header("Intro Session")]
    public string Text_intro;
    public int Time_intro;
    public string Text_introPhoto;
    public int Time_introPhoto;
    public Texture2D intro_Picture_3307;
    public int Time_intro_Picture_3307;

    [Header("Video Session")]
    public List<Scene_TextTime> TextTime_session = new List<Scene_TextTime>();
    public string Text_break;
    public int Time_break;

    [Header("End Session")]
    public string Text_endsession;
    public int Time_endsession;
    public string Text_over;
    public int Time_over;

}
