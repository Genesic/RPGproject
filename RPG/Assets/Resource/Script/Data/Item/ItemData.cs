using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//  裝備道具的動態資料
public class ItemData
{
    public int lv;
    public int exp;
    public bool awake;
    public string equipName;
    public string equiper;  // 道具裝備者
    public int equipSeq;
    public DateTime creatTime;
    
    public static int getMaxExp(int lv)
    {
        /*
        var map = new Dictionary<int, int>()
        {
                        
        };
        */
        return lv*100;
    }
    
    public void patchExp (int patch)
    {
        exp += patch;
        int expMax = getMaxExp(lv);
        while(exp >= expMax)
        {
            lv += 1;
            exp -= expMax;
            expMax = getMaxExp(lv);
        } 
    }
    
    public ItemData getItemDataClone()
    {
        var itemData = new ItemData();
        itemData.lv = lv;
        itemData.exp = exp;
        itemData.awake = awake;
        itemData.equipName = equipName;
        itemData.equiper = equiper;
        itemData.equipSeq = equipSeq;
        itemData.creatTime = creatTime;
        return itemData;
    }

    public EquipData getEquipData()
    {
        var equipItem = MainData.dataCenter.itemData.equipGroup.getEquipItem(equipName);
        var equipData = new EquipData();
        equipData.icon = equipItem.icon;
        equipData.iconColor = equipItem.iconColor;
        equipData.showName = equipItem.itemName;
        equipData.lv = lv;
        equipData.max_lv = EquipItem.getMaxLv(equipItem.rareType);
        equipData.exp = exp;
        equipData.expMax = getMaxExp(lv);
        equipData.equipType = equipItem.equipType;
        equipData.hp = equipItem.hp;
        equipData.mp = equipItem.mp;
        equipData.atk = equipItem.atk;
        equipData.def = equipItem.def;
        equipData.matk = equipItem.matk;
        equipData.mdef = equipItem.mdef;
        equipData.mp2 = equipItem.mp2;
        equipData.spd = equipItem.spd;
        equipData.specialAbility = equipItem.specialAbility;
        equipData.specialValue = equipItem.specialValue;
        equipData.awakeAbility = equipItem.awakeAbility;
        equipData.awakeValue = equipItem.awakeValue;
        
        switch (equipItem.equipType)
        {
            case EquipType.Sword:
                equipData.atk += equipData.lv * 10;
                break;
            case EquipType.Wand:
                equipData.matk += equipData.lv * 10;
                break;
            case EquipType.Armor:
                equipData.def += equipData.lv * 10;
                equipData.mdef += equipData.lv * 4;
                break;
            case EquipType.Robes:
                equipData.mdef += equipData.lv * 10;
                break;
            case EquipType.Shoes:
                equipData.spd += equipData.lv;
                break;
            case EquipType.Accessory:
                equipData.specialValue += equipData.lv * 10;
                break;
        }
        return equipData;
    }
}

public struct EquipData
{
    public Sprite icon;
    public Color iconColor;
    public string showName;
    public EquipType equipType;
    public int lv;
    public int max_lv;
    public int exp;
    public int expMax;
    public int hp;
    public int mp;
    public int mp2;
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    public int spd;
    // public float max_act;
    public SpecialAbility specialAbility;
    public int specialValue;
    public SpecialAbility awakeAbility;
    public int awakeValue;
}