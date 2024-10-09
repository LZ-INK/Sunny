using System;
using UnityEngine;

public  class Item :MonoBehaviour
{
    public FeetBackManager manager;

    public int ID;

    public int Count;

    //委托事件接功能
    public Action action;
    //private void Awake()
    private void Awake()
    {
        manager = FeetBackManager.Instance;
    }
    //物品
    public virtual void TouchOff() 
    {
        manager.ItemFeedback(transform);
        action.Invoke();
        Destroy(gameObject);
    }

    //生成
    public virtual void Generate()
    {

    }


}
