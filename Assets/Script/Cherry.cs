

using UnityEngine;

public class Cherry : Item
{
    private void Start()
    {
        action = Onv;
    }
    public void Onv()
    {
        Debug.Log("Cherry���ⷽ��");
    }
    private void OnDestroy()//��ʱʹ��
    {
        action -= Onv;
    }
    /*    public override void TouchOff()
        {



        }*/
}
