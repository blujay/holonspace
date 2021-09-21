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
        private Transform parentObj;

        //public GameObject prefab;
        //public Transform spawnPoint;

        private void OnEnable()
        {
            // TODO AndyB
            //cloneAction = SteamVR_Input.GetActions<SteamVR_Action_Boolean>()[0];

            if (hand == null)
            {
                var hands = FindObjectsOfType<Hand>();
                foreach (var h in hands)
                {
                    if (h.handType == SteamVR_Input_Sources.RightHand)
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
                if (hand.hoveringInteractable.gameObject == this.gameObject)
                {

                    GameObject clone = GameObject.Instantiate<GameObject>(gameObject);
                    clone.transform.position = (transform.position);
                    clone.transform.rotation = transform.localRotation;
                    clone.transform.localScale = transform.localScale;

                    // Prevent cloned objects colliding with original.
                    // This assumes that all gameobjects in the clonable's hierarchy are on the default layer
                    // We undo this in Hand.DetachObject()
                    // Hacky! We probably need to keep track of the entire "isTrigger" status for the cloned
                    // objects hierarchy but that seems like a lot of effort.
                    clone.layer = LayerMask.NameToLayer("Cloning");
                    foreach (var go in clone.GetComponentsInChildren<Transform>())
                    {
                        go.gameObject.layer = LayerMask.NameToLayer("Cloning");
                    }

                    //clone.transform.name = gameObject.name;
                    hand.AttachObject(clone, hand.GetBestGrabbingType(GrabTypes.None), Hand.AttachmentFlags.ParentToHand);
                }
            }
        }

    }
}