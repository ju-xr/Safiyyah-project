using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class DataCollection : MonoBehaviour
{
    public UI_Function settingData;

    string filename = "";

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SettingParameterSaver()
    {
        // Save setting panel data INTO file .csv when start button pressed 

        //partcipant code, audio order, 


    }

    public void WriteCSV()
    {
        TextWriter tw = new StreamWriter(filename,false);
        tw.WriteLine("Participant: " + "," + settingData.PartcipantCode);
        tw.Close();

        tw = new StreamWriter(filename, true);

        //write data

        tw.Close ();



    }



}
