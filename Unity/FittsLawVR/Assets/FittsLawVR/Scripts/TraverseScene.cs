using UnityEngine;
using UnityEngine.SceneManagement;

public class TraverseScene : MonoBehaviour
{
    public void ChangeScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
