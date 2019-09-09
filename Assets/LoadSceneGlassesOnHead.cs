// SceneA.
// SceneA is given the sceneName which will
// load SceneB from the Build Settings

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneGlassesOnHead : MonoBehaviour
{

    public string sceneToLoad;
    public Transform headset;

void Start()
    {
    }

    public void LoadScene(string sceneToLoad)
    {
        Debug.Log("sceneName to load: " + sceneToLoad);
        this.transform.SetParent(headset.transform);
        this.transform.SetPositionAndRotation(headset.transform.position, headset.localRotation);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}