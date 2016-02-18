using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipGroup : MonoBehaviour {
    public Dictionary<EquipType, EquipList> equip;
    public Dictionary<string, EquipItem> list;
    
    public void init (){
        equip = new Dictionary<EquipType, EquipList>();
        list = new Dictionary<string, EquipItem>();
        
        foreach(Transform child in transform){
            var data = child.gameObject.GetComponent<EquipList>();
            data.init();
            if( equip.ContainsKey(data.equipType) )
                Debug.Log("EquipGroup duplicate:"+data.equipType);
                
            equip.Add(data.equipType, data);
            
            foreach( KeyValuePair <string, EquipItem> item in data.list ){
                if( list.ContainsKey(item.Key) )
                    Debug.Log("EquipGroup add list duplicate:"+item.Key );
                    
                list.Add(item.Key, item.Value);
            }                            
        }
    }
    
    public EquipItem getEquipItem(string equipName)
    {
        if( list.ContainsKey(equipName))
            return list[equipName];
            
        return null;
    }
}
