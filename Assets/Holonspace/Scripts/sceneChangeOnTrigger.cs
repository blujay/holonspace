using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChangeOnTrigger : MonoBehaviour
{

public string sceneToLoad;
public string activeScene;
//public Transform headset;


    private void OnTriggerEnter(Collider other){
        Debug.Log("Just been hit by " + other.name);
        Debug.Log("active scene = " + activeScene);
        if(other.gameObject.name == "HeadCollider"){
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(activeScene);
        }
    }
   
}