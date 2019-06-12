using UnityEngine;
using System.Collections;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ClonePrefab : MonoBehaviour
    {

        public GameObject prefab;
        private Hand hand;
        
        public void Clone()
        {
            GameObject clone = GameObject.Instantiate<GameObject>(prefab);
            clone.transform.position = this.transform.position;
            clone.transform.rotation = this.transform.localRotation;
            clone.transform.name = prefab.name + "-Clone" + Time.time;
        }
    }
}