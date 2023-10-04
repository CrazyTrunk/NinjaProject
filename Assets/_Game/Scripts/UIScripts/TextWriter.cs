using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TextWriter;

public class TextWriter : MonoBehaviour
{
    private List<TextWriterSingle> textWriterSingleList;
    private static TextWriter instance;
    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }
    public static void AddWriter_Static(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd)
        {
            instance.RemoveWriter(uiText);
        }
        instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters);
    }
    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        textWriterSingleList.Add(new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters));
    }
    private void RemoveWriter(Text uiText)
    {
        for(int i = 0; i < textWriterSingleList.Count; i++)
        {
            if (textWriterSingleList[i].GetUIText() == uiText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }
    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }

    }

}
public class TextWriterSingle
{
    private Text _uiText;
    private string _textToWrite;
    private float _timePerCharacter;
    private int characterIndex;
    private float timer;
    private bool _invisibleCharacters;
    public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        _uiText = uiText;
        _textToWrite = textToWrite;
        _timePerCharacter = timePerCharacter;
        _invisibleCharacters = invisibleCharacters;
    }
    public bool Update()
    {
        timer -= Time.deltaTime;
        while (timer <= 0f)
        {
            //Display next character
            timer += _timePerCharacter;
            characterIndex++;
            string text = _textToWrite.Substring(0, characterIndex);
            if (_invisibleCharacters)
            {
                text += "<color=#00000000>" + _textToWrite.Substring(characterIndex) + "</color>";
            }
            _uiText.text = text;

            if (characterIndex >= _textToWrite.Length)
            {
                return true;
            }
        }
        return false;
    }
    public Text GetUIText()
    {
        return _uiText;
    }
}
