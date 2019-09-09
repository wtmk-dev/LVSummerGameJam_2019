using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatScreen : MonoBehaviour, IScreen 
{
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
        //id = ScreenID.CombatScreen;
        Hide();
    }
}