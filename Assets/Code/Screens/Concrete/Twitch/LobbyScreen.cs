using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyScreen : MonoBehaviour, IScreen 
{

    [SerializeField]
    private List<TextMeshProUGUI> textFields;

    private Dictionary<string,TextMeshProUGUI> textFieldsDisctionary;

    private ScreenID id;

    public ScreenID GetID()
    {
        return id;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Init()
    {
        textFieldsDisctionary = new Dictionary<string, TextMeshProUGUI>();
        id = ScreenID.LobbyScreen;

        foreach(TextMeshProUGUI tmp in textFields)
        {
            textFieldsDisctionary.Add(tmp.gameObject.name, tmp);
        }

        Hide();
    }
}

// public enum LobbyScreenTextFields
// {
//     JoinText = "JoinText",
//     TitleText = "TitleText",
//     QueueText = "QueueText",
//     MatchText = "MatchText",
//     CountDownText = "CountDownText"
// }