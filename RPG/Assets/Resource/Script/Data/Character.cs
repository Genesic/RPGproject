using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum CharacterType
{
    None,
    Tank,       // 坦克
    Mage,       // 魔法輸出
    Warrior,    // 物理輸出
    Healer,     // 補師     
}

public class Character : MonoBehaviour
{
    public const int BASE_MP = 1000;
    public const int EQUIP_NUM = 4;
    public const int SKILL_NUM_MAX = 6;
    public const int LV_LIMIT = 99;
    public string characterName;
    public CharacterType career;
    public int baseHp;
    public int baseMp2;
    public int baseAtk;
    public int baseDef;
    public int baseMatk;
    public int baseMdef;
    public int baseSpd;
    public float baseMaxAct;
    public int lv;
    public int maxLv;
    public int exp;
    public int useSkill;
    public bool open;
    public Sprite headPic;

    public string[] skills;
    public int[] skillLevels;
    public int seq;

    public Dictionary<int, ItemData> getEquipItem()
    {
        Dictionary<int, ItemData> equip = new Dictionary<int, ItemData>();
        foreach (ItemData itemData in MainData.dataCenter.equipList)
        {
            if (itemData.equiper.CompareTo(characterName) == 0)
            {
                if (equip.Count >= EQUIP_NUM)
                    break;

                equip.Add(itemData.equipSeq, itemData);
            }
        }
        return equip;
    }

    public static int getMaxExp(int lv)
    {
        return lv * 100;
    }

    public CharacterInfo getCharacter()
    {
        var careerObj = MainData.dataCenter.careerData.list[career];
        // 取得角色本身能力
        var ability = careerObj.getGrowAbility(lv);

        // 計算角色本身能力
        var data = new CharacterInfo();
        data.cName = characterName;
        data.lv = lv;
        data.maxLv = maxLv;
        data.hp = baseHp + ability.hp;
        data.mp = BASE_MP;
        data.mp2 = baseMp2 + ability.mp2;
        data.atk = baseAtk + ability.atk;
        data.def = baseDef + ability.def;
        data.matk = baseMatk + ability.matk;
        data.mdef = baseMdef + ability.mdef;
        data.spd = baseSpd;
        data.maxAct = baseMaxAct;
        data.exp = exp;
        data.maxExp = getMaxExp(lv);
        data.useSkill = useSkill;
        data.headPic = headPic;
        for (int i = 0; i < skills.Length && i < SKILL_NUM_MAX; i++)
        {
            if (lv >= skillLevels[i])
            {
                string sName = skills[i];
                if (MainData.dataCenter.skillData.list.ContainsKey(sName))
                {
                    data.skills.Add(skills[i]);
                }
            }
            else
            {   // 不到開放等級的技能
                data.skills.Add("?");
            }
        }

        for (int j = skills.Length; j < SKILL_NUM_MAX; j++)
        {   // 沒有更多的技能
            data.skills.Add("-");
        }

        // 取得裝備能力(一人只能裝備四個)
        Dictionary<int, ItemData> equip = getEquipItem();
        var eInfo = EquipItem.getEquipAbility(data, equip);
        data.hp += eInfo.hp;
        data.mp += eInfo.mp;
        data.mp2 += eInfo.mp2;
        data.atk += eInfo.atk;
        data.def += eInfo.def;
        data.matk += eInfo.matk;
        data.mdef += eInfo.mdef;
        data.spd += eInfo.spd;
        data.equipSpAbility = eInfo.specialAbility;
        return data;
    }
}

public class CharacterInfo
{
    public string cName;
    public int lv;
    public int maxLv;
    public int hp;
    public int mp;
    public int mp2;
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    public int spd;
    public float maxAct;
    public int exp;
    public int maxExp;
    public int useSkill;
    public List<string> skills;
    public Sprite headPic;
    public Dictionary<SpecialAbility, int> equipSpAbility; // 裝備特殊能力
    
    public CharacterInfo(){
        skills = new List<string>();
        equipSpAbility = new Dictionary<SpecialAbility, int>();
    }
}
