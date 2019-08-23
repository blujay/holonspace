using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RuntimePersistence : MonoBehaviour
{
    [SerializeField]
    RuntimePersistantStorage storage;

    public RuntimePersistantStorage GetStorage(){
        return storage;
    }

    void Start(){
        
    }

    void Update(){
        
    }

    void OnDestroy(){
        Debug.Log("OnDestroy");
    }

    #if UNITY_EDITOR
    
    static RuntimePersistence() {
        UnityEditor.EditorApplication.playModeStateChanged += OnPlayStateChange;
    }
    static void OnPlayStateChange(UnityEditor.PlayModeStateChange stateChange){
        if(stateChange==UnityEditor.PlayModeStateChange.ExitingPlayMode){
            var persistence = Object.FindObjectOfType<RuntimePersistence>();
            if(!persistence)
                return;
            var storage = persistence.GetStorage();
            if(!storage)
                return;

            var holons = Object.FindObjectsOfType<PersistentHolon>();
            storage.ClearHolons();
            foreach(var holon in holons){
                
                storage.AddHolon( holon );
            }

        }
        else if(stateChange==UnityEditor.PlayModeStateChange.ExitingEditMode){
            Debug.Log("exiting editor mode");
            var holons = Object.FindObjectsOfType<PersistentHolon>();
            
        }
        else if(stateChange==UnityEditor.PlayModeStateChange.EnteredEditMode){
            var persistence = Object.FindObjectOfType<RuntimePersistence>();
            if(!persistence)
                return;
            var storage = persistence.GetStorage();
            if(!storage)
                return;
            var holons = Object.FindObjectsOfType<PersistentHolon>();

            foreach(var holon in holons){
                if(holon.transform.parent && holon.transform.parent.gameObject.GetComponentInParent<PersistentHolon>()){
                    continue;
                }
                if(holon) DestroyImmediate(holon.gameObject);
            }

            foreach(var holonData in storage.GetHolons()){
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath( holonData.guid );
                if( string.IsNullOrEmpty(path) ){
                    Debug.LogError("Unable to create Holon, guid not found");
                    continue;
                }

                var asset = UnityEditor.AssetDatabase.LoadMainAssetAtPath(path);
                var prefabInstance = UnityEditor.PrefabUtility.InstantiatePrefab( asset );
                prefabInstance.name = holonData.name;
                var gameObject = prefabInstance as GameObject;
                gameObject.GetComponent<PersistentHolon>().SetDecoratorData( holonData.decoratorData );
            }
        }
        
    }
    #endif 
}
