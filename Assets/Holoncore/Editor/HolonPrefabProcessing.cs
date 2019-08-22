using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HolonPrefabProcessing : UnityEditor.AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        Debug.Log("asset moved");
        foreach(var a in movedAssets) Debug.Log(a);

        Debug.Log("asset imported");
        foreach(var a in importedAssets) {
            Debug.Log(a);
            var guid = AssetDatabase.AssetPathToGUID(a);
            var asset = AssetDatabase.LoadMainAssetAtPath(a);
            var assetType = PrefabUtility.GetPrefabAssetType(asset);
            if(assetType==PrefabAssetType.Regular){
                var gameObjectAsset = asset as GameObject;
                var holon = gameObjectAsset.GetComponent<PersistentHolon>();
                if(holon){
                    Debug.Log("setting guid");
                    holon.SetPrefabGUID(guid);
                }
                AssetDatabase.SaveAssets();
            }
        }
    }
}
