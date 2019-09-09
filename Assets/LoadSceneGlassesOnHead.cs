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

    private void OnTriggerEnter(Collider other)
    {
       //Debug.Log("just collided with " + other);
       if(other.name == "headset"){
           Debug.Log("this is the headset");
           attachGlassesToHead();
       }
    }

    private void OnTriggerExit(Collider other)
    {
       dettachGlassesFromHead();
    }

    private void attachGlassesToHead()
    {
        Debug.Log("attached from headset");
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.SetParent(headset.transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);        
    }

    private void dettachGlassesFromHead()
    {
        Debug.Log("detached from headset");
        this.GetComponent<Rigidbody>().isKinematic = false;       
    }

    public void LoadScene(string sceneToLoad)
    {
        Debug.Log("sceneName to load: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}