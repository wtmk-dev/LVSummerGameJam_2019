using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour, IScreen 
{
    [SerializeField]
    private ScreenID id;

    public ScreenID GetID()
    {
        return id;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Init()
    {
        Hide();
    }


}