using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(SceneName name)
    {
        //����
        SceneManager.LoadScene((int)name);
    }
}

public enum SceneName
{

}
