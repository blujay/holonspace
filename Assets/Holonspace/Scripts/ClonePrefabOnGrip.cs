using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ClonePrefabOnGrip : MonoBehaviour
    
    {   
        public SteamVR_Action_Boolean cloneAction;

        public Hand hand;
        
        public GameObject prefab;
        public Transform spawnPoint;
        
        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (cloneAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> Don't clone");
                return;
            }

            cloneAction.AddOnChangeListener(OnCloneActionChange, hand.handType);
        }

        private void OnCloneActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                Clone();
            }
        }


        public void Clone()
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();
            if (startingGrabType == GrabTypes.Grip)
            {
                Debug.Log("gripping");
                GameObject clone = GameObject.Instantiate<GameObject>(prefab);
                clone.transform.position = (spawnPoint.position);
                clone.transform.rotation = spawnPoint.transform.localRotation;
                clone.transform.localScale = prefab.transform.localScale;
                clone.transform.name = prefab.name + "(Clone)" + Time.time;
            }
        }

    }
}