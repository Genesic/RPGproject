using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillGroup : MonoBehaviour {
    public Dictionary<string, SkillBase> list;
    public void init(){
        list = new Dictionary<string, SkillBase>();
        
        foreach(Transform child in transform ){
            var data = child.gameObject.GetComponent<SkillBase>();
            if( list.ContainsKey(data.skillName) )
                Debug.Log("Skill duplicate:"+data.skillName);
                
            list.Add(data.skillName, data);
        }
    }
}
