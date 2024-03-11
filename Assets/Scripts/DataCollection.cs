using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class DataCollection : MonoBehaviour
{
    public UI_Function inputData_UI;

    string filename = "";

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            WriteSettingCSV();
        }
    } 

    void SettingParameterSaver()
    {
        // Save setting panel data INTO file .csv when start button pressed 

        //partcipant code, audio order, 


    }

    public void WriteSettingCSV()
    {
        Debug.Log("Write csv file data");
        TextWriter tw = new StreamWriter(filename,false);
        tw.WriteLine("Participant: " + "," + inputData_UI.AudioFolderName);
        tw.WriteLine("Audio Order: " + "," );
        tw.Close();

        tw = new StreamWriter(filename, true);

        //write data

        tw.Close ();



    }



}
