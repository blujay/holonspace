using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDecorator : MonoBehaviour
{
    [SerializeField]
    Component target;

    void SetData(string data){


        JsonUtility.FromJsonOverwrite(data,target);
    }

    string GetData(){
        
        return JsonUtility.ToJson(target);
    }
}
