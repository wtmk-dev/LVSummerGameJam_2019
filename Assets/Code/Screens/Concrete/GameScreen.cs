using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour, IScreen 
{
    [SerializeField]
    private ScreenID id;
    [SerializeField]
    private GameObject goMain;
    private Main main;

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
        main = goMain.GetComponent<Main>();
        Hide();
    }

    private void StartGame()
    {
        main.SetCurrentScreen(ScreenID.GameScreen);
        //Hide();
    }


}