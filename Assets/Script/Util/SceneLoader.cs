using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(SceneName name)
    {
        //╪сть
        SceneManager.LoadScene((int)name);
    }
}

public enum SceneName
{

}
