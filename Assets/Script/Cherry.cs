

using UnityEngine;

public class Cherry : Item
{
    private void Start()
    {
        action = Onv;
    }
    public void Onv()
    {
        Debug.Log("Cherry特殊方法");
    }
    private void OnDestroy()//暂时使用
    {
        action -= Onv;
    }
    /*    public override void TouchOff()
        {



        }*/
}
