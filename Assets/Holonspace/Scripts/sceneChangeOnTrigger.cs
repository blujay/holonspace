using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChangeOnTrigger : MonoBehaviour
{

public string sceneToLoad;
//public string homeScene;
//public Transform headset;



    private void OnTriggerEnter(Collider other){
        Debug.Log("Just been hit by " + other.name);
        if(other.gameObject.name == "HeadCollider"){
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
   
}