using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL; //lab streaming

public class lslController : MonoBehaviour
{

    string StreamName = "LSL4Unity.Samples.MusicMemory";
    string StreamType = "Markers";
    private StreamOutlet outlet;
    private string[] sample = {""};

    // Start is called before the first frame update
    void Start()
    {
        var hash = new Hash128();
        hash.Append(StreamName);
        hash.Append(StreamType);
        hash.Append(gameObject.GetInstanceID());
        StreamInfo streamInfo = new StreamInfo(StreamName, StreamType, 1, LSL.LSL.IRREGULAR_RATE, channel_format_t.cf_string, hash.ToString());

        outlet = new StreamOutlet(streamInfo);
        print("LSL setup");
        

    }


    public void SendTestTrig(string message)
    {
        if (outlet != null)
        {
            sample[0] = "601::" + message;
            Debug.Log(sample[0]);
            outlet.push_sample(sample);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    }
