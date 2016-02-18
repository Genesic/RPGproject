using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipList : MonoBehaviour {
    public EquipType equipType;
    public ProcessType processType;
    public Dictionary <string, EquipItem> list;
        
    public void init(){
        list = new Dictionary<string, EquipItem>();
        
        foreach(Transform child in transform){
            var data = child.gameObject.GetComponent<EquipItem>();
            // 如果keyName是空的話, 用物件名稱代替
            if( data.keyName == string.Empty)
                data.keyName = child.name;
                
            if( list.ContainsKey(data.keyName) )                
                Debug.Log("Equip("+equipType+") duplicate:"+data.keyName );
                
            // EquipList下的道具統一使用EquipList的道具類型
            // equipType 判斷道具成長的種類
            // processType 為替道具本身分類的種類
            data.equipType = equipType;
            data.processType = processType;
            list.Add(data.keyName, data);
        }
    }    
}
