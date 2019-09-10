// SceneA.
// SceneA is given the sceneName which will
// load SceneB from the Build Settings

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneGlassesOnHead : MonoBehaviour
{

    
    public string sceneToLoad;
    
    public string homeScene;

        private void OnTriggerEnter(Collider other)
        {
        //Debug.Log("just collided with " + other);
        if(other.name == "HeadCollider"){
            Debug.Log("this is the headset");
            this.transform.parent = null;
            Destroy(this.gameObject);
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }
        }


        private void OnTriggerExit(Collider other)
    {
            if(other.name == "HeadCollider"){
            Debug.Log("detached from headset");
        }
    }
}