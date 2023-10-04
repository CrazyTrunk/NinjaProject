using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assist : MonoBehaviour
{
    private Text messageText;
    private void Awake()
    {
        messageText = transform.Find("messageText").GetComponent<Text>();
    }
    private void Start()
    {
        TextWriter.AddWriter_Static(messageText, "This is a Project develop by crazytrunk......", 0.5f, true);
    }

}
