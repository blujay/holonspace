using UnityEngine;
using System.Collections;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ClonePrefabRigid : MonoBehaviour
    {

        public GameObject prefab;
        public Transform spawnPoint;
        private Hand hand;
        
        public void Clone()
        {
            GameObject clone = GameObject.Instantiate<GameObject>(prefab);
            clone.transform.position = (spawnPoint.position);
            clone.transform.rotation = spawnPoint.transform.localRotation;
            clone.transform.localScale = prefab.transform.localScale;
            clone.transform.name = prefab.name + "-Clone" + Time.time;
            clone.GetComponent<Rigidbody>().isKinematic = false;
            clone.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}