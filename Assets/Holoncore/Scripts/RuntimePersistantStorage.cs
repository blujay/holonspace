using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu]
public class RuntimePersistantStorage : ScriptableObject {
    [SerializeField]
    List<HolonData> holons;

#if UNITY_EDITOR 
    public void AddHolon(PersistentHolon holon){
        holons.Add( new HolonData(){
            name = holon.name,
            guid = holon.GetPrefabGUID(),
            decoratorData = holon.GetDecoratorData(),
        });
    }

    public HolonData[] GetHolons(){
        return holons.ToArray();
    }

    public void ClearHolons(){
        holons.Clear();
    }

#endif
}

[System.Serializable]
public class HolonData {
    public string name;
    public string guid;

    public string[] decoratorData;

}
