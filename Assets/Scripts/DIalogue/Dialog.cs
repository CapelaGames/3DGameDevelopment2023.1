using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct DialogData
{
    public string name;
    [TextArea(3,10)]
    public List<string> body;
}

public class Dialog : MonoBehaviour
{
    public int pageNumber = 0;
    private DialogData _dialogData;
    
    public TMP_Text nameText;
    public TMP_Text bodyText;
    public Button nextButton;
    public Button prevButton;

    public void DisplayDialog(DialogData dialogData)
    {
        _dialogData = dialogData;
        pageNumber = 0;
        nameText.text = dialogData.name;
        bodyText.text = dialogData.body[pageNumber];
    }

    public void NextPage()
    {
        if (pageNumber + 1 < _dialogData.body.Count)
        {
            pageNumber++;
            prevButton.interactable = true;
        }
        else
        {
            if (nextButton != null)
            {
                nextButton.interactable = false;
            }
        }

        bodyText.text = _dialogData.body[pageNumber];
    }
    
    public void PrevPage()
    {
        if (pageNumber - 1 >= 0)
        {
            pageNumber--;
            nextButton.interactable = true;
        }
        else
        {
            if (prevButton != null)
            {
                prevButton.interactable = false;
            }
        }
        bodyText.text = _dialogData.body[pageNumber];
    }
}
