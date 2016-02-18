using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EquipProcessSortType{
    CreateTime,
    Lv,
    Rare,
}

public class SortByEquipType : MonoBehaviour {
    public Animator openTypeAnime;
    public Animator openSortAnime;
    int openHash = Animator.StringToHash("open");
    int closeHash = Animator.StringToHash("close");
    
    // int轉ProcessType
    Dictionary<int, ProcessType> equipTypeMap = new Dictionary<int, ProcessType>(){
        {0, ProcessType.None},
        {1, ProcessType.Weapon},
        {2, ProcessType.Armor},
        {3, ProcessType.Accessory}
    };
    
    public void openTypeList(){        
        // 展開種類按鈕的動畫
        openTypeAnime.SetTrigger(openHash);
    }
    
    public void selectType(int selectType){        
        // 收起種類按鈕的動畫
        openTypeAnime.SetTrigger(closeHash);
    }
}
