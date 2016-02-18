using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType{
    None = 0,   // none type
    Equip,     // 裝備
    Awake,      // 覺醒道具
    Strength,   // 強化道具
}

public enum RareType{
    None = 0,
    Normal,
    Rare,
    SuperRare,
}

public enum ProcessType {
    None = 0,
    Weapon,
    Armor,
    Accessory,    
}

public class ItemBase : MonoBehaviour {
    public int id;
    public string itemName;
    public string keyName;
    public ItemType itemType;
    public RareType rareType;
    public int exp;         // 可以換多少經驗值
    public int money;       // 可以換多少金錢
    public Sprite icon;
    public Color iconColor = Color.white;
    
    public static string getRareName(RareType rType)
    {
        Dictionary<RareType, string> map = new Dictionary<RareType, string>(){
            {RareType.Normal, "N"},
            {RareType.Rare, "R"},
            {RareType.SuperRare, "SR"}
        };
        
        if( map.ContainsKey(rType) )
            return map[rType];
            
        return "";
    }
}
