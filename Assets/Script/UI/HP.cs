using System;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [Range(1, 10)]
    public int HpUpperLimit = 3;

    public HpIcon icon;

    public Action deadAct;

    List<HpIcon> list = new List<HpIcon>();
    int Hp;
    
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < HpUpperLimit; i++)
        {
            HpIcon Hp = Instantiate(icon,this.transform);
            list.Add(Hp);
        }
        Hp = HpUpperLimit;
    }

    public void ChangeHp(HpType val)
    {
        switch (val)
        {
            case HpType.ADD:
                AddHP();
                break;
            case HpType.SUB:
                SubHP();
                break;
            default:
                break;
        }
    }
    void AddHP()
    {
        if (Hp >= HpUpperLimit)
        {
            return;
        }
        list[Hp].Full.SetActive(true);
        Hp++;

    }
    public void SubHP()
    {
        if (Hp<=0)
        {
            return ;
        }
       
        list[--Hp].Full.SetActive(false);
        
        if (Hp <= 0)
        {
            deadAct?.Invoke();
            Debug.Log("dead");
        }
      
    }
    public void AddHpUpperLimit(int val)
    {

    }


}

public enum HpType
{
    ADD,
    SUB
}
