using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CareerGroup : MonoBehaviour {
    public Dictionary<CharacterType, Career> list;
    
    public void init(){
        list = new Dictionary<CharacterType, Career>();
        
        foreach(Transform child in transform){
            var data = child.gameObject.GetComponent<Career>();
            if( list.ContainsKey(data.characterType) )
                Debug.Log("Career duplicate:"+data.characterType );
                
            list.Add(data.characterType, data );
        }
    }
}
