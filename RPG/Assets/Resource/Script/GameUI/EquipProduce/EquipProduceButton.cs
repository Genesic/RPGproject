using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum ProduceType{
    Strength,
    Awake,
}

public class EquipProduceButton : MonoBehaviour {
    
    public Image[] buttonBorder;
    public Image[] buttonBack;
    public Text[] buttonText;
    
    Color32 isSelectBack = new Color32(255,187,0,255);
    Color32 isSelectText = new Color32(50,50,50,255);
    Color32 notSelectBack = new Color32(100,100,100,255);
    Color32 notSelectText = new Color32(255,255,255,255);
    
    public Dictionary<int, ProduceType> produceMap = new Dictionary<int, ProduceType>()
    {
        {0, ProduceType.Strength},
        {1, ProduceType.Awake}
    };
    
    public ProduceType selectType;
    
    public void chooseEquipProduce(int pType){
        selectType = produceMap[pType];
        for(int i=0; i<buttonBorder.Length; i++){
            if( pType == i){
                buttonBorder[i].color = Color.white;
                buttonBack[i].color = isSelectBack;
                buttonText[i].color = isSelectText;
            } else {
                buttonBorder[i].color = Color.clear;
                buttonBack[i].color = notSelectBack;
                buttonText[i].color = notSelectText;
            }
        }
    }
}
