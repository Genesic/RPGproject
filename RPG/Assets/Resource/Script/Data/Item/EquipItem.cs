using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 裝備成長曲線類型
public enum EquipType
{
    None = 0,
    Sword,          // +ATK
    Wand,           // +MATK
    Armor,          // +DEF, +MDEF
    Robes,          // +MDEF
    Shoes,          // +SPD
    Accessory,      // +SpecialValue +MP2 +MP
}

// 飾品特殊效果
public enum SpecialAbility
{
    None = 0,
    MaxHpPercent,
    MaxMp,
    Mp2,
}

public class EquipItem : ItemBase
{
    public EquipType equipType;
    public ProcessType processType;
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

    public static int getMaxLv(RareType rare)
    {
        var map = new Dictionary<RareType, int>()
        {
            { RareType.Normal, 40 },
            { RareType.Rare, 50 },
            { RareType.SuperRare, 60 }
        };

        if (map.ContainsKey(rare))
            return map[rare];

        return 0;
    }

    public static EquipAbility getEquipAbility(CharacterInfo cInfo, Dictionary<int, ItemData> equip)
    {
        var ability = new EquipAbility();
        Dictionary<SpecialAbility, int> sp = new Dictionary<SpecialAbility, int>();
        foreach (KeyValuePair <int, ItemData> iInfo in equip)
        {
            var itemData = iInfo.Value;
            // var equipItem = MainData.dataCenter.itemData.equipGroup.getEquipItem(itemData.equipName);
            var data = itemData.getEquipData();
            ability.hp += data.hp;
            ability.mp += data.mp;
            ability.mp2 += data.mp2;
            ability.atk += data.atk;
            ability.def += data.def;
            ability.matk += data.matk;
            ability.mdef += data.mdef;
            ability.spd += data.spd;

            // 統計裝備的特殊能力
            if (sp.ContainsKey(data.specialAbility))
            {
                sp.Add(data.specialAbility, data.specialValue);
            }
            else
            {
                sp[data.specialAbility] += data.specialValue;
            }

            // 統計裝備的覺醒能力
            if (sp.ContainsKey(data.awakeAbility))
            {
                sp.Add(data.awakeAbility, data.awakeValue);
            }
            else
            {
                sp[data.awakeAbility] += data.awakeValue;
            }
        }
        
        // 計算特殊能力素質
        foreach (SpecialAbility type in sp.Keys)
        {
            switch (type)
            {
                case SpecialAbility.Mp2:
                    ability.mp2 += sp[type];
                    break;
                case SpecialAbility.MaxMp:
                    ability.mp += sp[type];
                    break;
                case SpecialAbility.MaxHpPercent:
                    float addHp = (sp[type] / 100) * (cInfo.hp + ability.hp);
                    ability.hp += (int)addHp;
                    break;
            }
        }
        
        return ability;
    }
/*
    public EquipData getEquipData(ItemData itemData)
    {
        var equipData = new EquipData();
        equipData.lv = itemData.lv;
        equipData.max_lv = getMaxLv(rareType);
        equipData.exp = itemData.exp;
        equipData.equipType = equipType;
        equipData.hp = hp;
        equipData.mp = mp;
        equipData.atk = atk;
        equipData.def = def;
        equipData.matk = matk;
        equipData.mdef = mdef;
        equipData.mp2 = mp2;
        equipData.spd = spd;
        equipData.specialAbility = specialAbility;
        equipData.specialValue = specialValue;
        equipData.awakeAbility = awakeAbility;
        equipData.awakeValue = awakeValue;

        var equip = MainData.dataCenter.itemData.equipGroup.getEquipItem(itemData.equipName);
        switch (equip.equipType)
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
                equipData.spd += equipData.lv * 0.02F;
                break;
            case EquipType.Accessory:
                equipData.specialValue += equipData.lv * 10;
                break;
        }
        return equipData;
    }
*/
}

public struct EquipAbility
{
    public int hp;
    public int mp;
    public int mp2;
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    public int spd;
    
    public Dictionary<SpecialAbility, int> specialAbility;
}