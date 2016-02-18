using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGroup : MonoBehaviour {    
    public AwakeGroup awakeGroup;
    public StrengthGroup strengthGroup;
    public EquipGroup equipGroup; 
    
    public void init(){
        awakeGroup.init();
        strengthGroup.init();
        equipGroup.init();
    }
    
    public AwakeItem get_awake_item_by_name(string name){
        if( awakeGroup.list.ContainsKey(name) )
            return awakeGroup.list[name];
            
        return null;
    }
}
