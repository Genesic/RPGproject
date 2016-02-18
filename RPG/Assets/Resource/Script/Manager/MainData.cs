using UnityEngine;
using System.Collections;

public class MainData : MonoBehaviour {
    public static DataCenter dataCenter;
    public static MainData ins;
    
    void Awake (){
        dataCenter = gameObject.GetComponent<DataCenter>();
        dataCenter.init();
        if( ins == null ){
            ins = this;
            GameObject.DontDestroyOnLoad(gameObject);
        } else if( ins != this ){
            Destroy(gameObject);
        }
    }
}
