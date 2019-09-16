
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LoadSceneGlassesOnHead : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
    //Debug.Log("just collided with " + other);
    if(other.name == "HeadCollider"){
        Debug.Log("this is the headset");
        if(gameObject.GetComponent<SteamVR_LoadLevel>()!= null){
            this.gameObject.GetComponent<SteamVR_LoadLevel>().Trigger();
            }
        else Debug.LogError("You are missing a SteamVR_LoadLevel component");
        }
    }
}