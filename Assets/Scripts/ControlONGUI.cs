using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlONGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.Button(new Rect(0, 0, 70, 30), "A button");
    }
}
