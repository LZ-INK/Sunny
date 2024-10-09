
using System;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public delegate void CloseHandler(UIWindow sender, WindowResult result);
    public event CloseHandler OnClose;

    public GameObject Root;
    public virtual Type Type
    {
        get { return this.GetType(); }
    }
    public enum WindowResult
    {
        None,
        yes,
        no
    }
    public void Close(WindowResult result = WindowResult.None)
    {
        //UIManager.Instance.Close(this.Type);
        if (this.OnClose != null)
        {
            this.OnClose(this, result);
        }
        this.OnClose = null;
    }

    public virtual void OnCloseClick()
    {
        this.Close();
    }

    public virtual void OnYesClick()
    {
        this.Close(WindowResult.yes);
    }

    public virtual void OnNoClick()
    {
        this.Close(WindowResult.no);
    }
    void OnMouseDown()
    {
        Debug.LogFormat("µã»÷´¥·¢:", this.name);
    }



}
