using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Transform credits;
    public Transform Enter;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    private void Start()
    {
        sprite =  Enter.GetComponent<SpriteRenderer>();
        credits.DOScale(3, 2);
        Enter.DOMoveY(-1,2);
        Tween myTween = sprite.DOColor(Color.yellow, 1);
        sprite.DOColor(Color.red, 1).From();
        
        myTween.SetLoops(100,LoopType.Yoyo);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
