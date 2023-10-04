using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assist : MonoBehaviour
{
    private Text messageText;
    private int currentIndex = 0; 
    string[] messageArray = new string[] {
            "This is a Project develop by crazytrunk......",
            "Hope you like it!",
            "And your mom gay"
            };
    private void Awake()
    {
        messageText = transform.Find("messageText").GetComponent<Text>();
        Button messageButton = transform.GetComponent<Button>();
        messageButton.onClick.AddListener(OnClickMessageButton);

    }

    private void OnClickMessageButton()
    {
        if (currentIndex >= messageArray.Length)
        {
            currentIndex = 0;
            transform.gameObject.SetActive(false);
        }

        TextWriter.AddWriter_Static(messageText, messageArray[currentIndex], 0.05f, true, true);
        currentIndex++;
    }
    private void Start()
    {
        //TextWriter.AddWriter_Static(messageText, "This is a Project develop by crazytrunk......", 0.1f, true, true);
    }

}
