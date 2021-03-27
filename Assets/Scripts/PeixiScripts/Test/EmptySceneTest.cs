using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptySceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(1);
        print(1);
    }
    private void OnEnable()
    {
        SceneManager.LoadScene(1);
        print(2);
    }
}
