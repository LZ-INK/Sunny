

using UnityEngine;

public class Gem : Item
{
    private void Start()
    {
        action = Onv;
    }
    public void Onv()
    {
        Debug.Log("���ⷽ��");
    }
    private void OnDestroy()//��ʱʹ��
    {
        action -= Onv;
    }
    /*    public override void TouchOff()
        {



        }*/
}
