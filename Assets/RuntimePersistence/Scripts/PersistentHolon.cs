using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PersistentHolon : MonoBehaviour {

    [SerializeField]
    string guid;

    [SerializeField]
    Component[] decoratorComponents;

    private void OnValidate()
    {
        //Debug.Log("validate");
        //var prefab = UnityEditor.PrefabUtility.GetPrefabInstanceHandle( gameObject );
        //Debug.Log(prefab);
        //Debug.Log( UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject) );
        
    }

    public void SetPrefabGUID(string GUID){
        this.guid = GUID;
    }

    public string GetPrefabGUID(){
        return guid;
    }

    public string[] GetDecoratorData(){
        string[] data = new string[decoratorComponents.Length];
        for(int i=0;i<decoratorComponents.Length;i++){
            var target = decoratorComponents[i];
            if(target is Transform){
                data[i] = JsonUtility.ToJson( new TransformData(){
                    position = transform.position,
                    rotation = transform.rotation,
                    localScale = transform.localScale                    
                } );
            }
            else {
                data[i] = JsonUtility.ToJson(target);
            }
        }
        return data;
    }

    public void SetDecoratorData(string[] data){
        for(int i=0;i<data.Length;i++){
            var target = decoratorComponents[i];
            var entry = data[i];
            if(target is Transform){
                var transformData = JsonUtility.FromJson<TransformData>(entry);
                var transform = target as Transform;
                transform.position = transformData.position;
                transform.rotation = transformData.rotation;
                transform.localScale = transformData.localScale;
            }
            else {
                JsonUtility.FromJsonOverwrite(entry,target);
            }
        }
    }
}

[System.Serializable]
public class TransformData {
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 localScale;
}
