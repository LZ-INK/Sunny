using System;
using UnityEngine;

public  class Item :MonoBehaviour
{
    public FeetBackManager manager;

    public int ID;

    public int Count;

    //ί���¼��ӹ���
    public Action action;
    //private void Awake()
    private void Awake()
    {
        manager = FeetBackManager.Instance;
    }
    //��Ʒ
    public virtual void TouchOff() 
    {
        manager.ItemFeedback(transform);
        action.Invoke();
        Destroy(gameObject);
    }

    //����
    public virtual void Generate()
    {

    }


}
