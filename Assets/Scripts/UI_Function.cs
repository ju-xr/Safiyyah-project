using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Data collection

public class UI_Function : MonoBehaviour
{
    public string PartcipantCode;
    public string[] audioOrder;

    [SerializeField] private TMP_Text ParcitipantTextBox;

    public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton() 
    {
        //TODO Record the parameters into clipcontrol script
        PartcipantCode = ParcitipantTextBox.text;


        // TODO start video
    }
}
