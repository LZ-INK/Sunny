using UnityEngine;


public class FeetBackManager : MonoSingleton<FeetBackManager>
{
    public GameObject feedback;


    public void ItemFeedback(Transform transform)
    {
        Instantiate(feedback,transform.position,Quaternion.identity,this.transform);
    }

}
