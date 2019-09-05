using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ClonePrefabOnGrip : MonoBehaviour
    
    {   
        public SteamVR_Action_Boolean cloneAction;

        private Hand hand;
        
        //public GameObject prefab;
        //public Transform spawnPoint;
        
        private void OnEnable()
        {
            if (hand == null)
            {
                var hands = FindObjectsOfType<Hand>();
                foreach (var h in hands)
                {
                    if (h.handType==SteamVR_Input_Sources.RightHand)
                    {
                        hand = h;
                        break;
                    }
                }


            }

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
                if (hand.hoveringInteractable.gameObject==this.gameObject)
                {
                    GameObject clone = GameObject.Instantiate<GameObject>(gameObject);
                    clone.transform.position = (transform.position);
                    clone.transform.rotation = transform.localRotation;
                    clone.transform.localScale = transform.localScale;
                    clone.transform.name = gameObject.name + "(Clone)";
                }
            }
        }

    }
}