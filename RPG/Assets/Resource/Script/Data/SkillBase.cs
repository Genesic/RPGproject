using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SkillType{
    None = 0,
    Atk,        //物理攻擊
    Matk,       //魔法攻擊
    Support,    //支援魔法
    Heal,       //恢復魔法
}

public enum TargetType{
    None = 0,
    Self,
    SelfGroup,
    SelfLowestHpValue,
    SelfLowestHpPer,
    Enemy,
    EnemyGroup,    
}

public enum ElementType{
    None = 0,
    Fire,
    Wind,
    Water,
    Holy,
    Dark,
}
public class SkillBase : MonoBehaviour {
    public int id;
    public string skillName;
    public int cost;    
    public string intro;
    public Skill skill;
    public SkillType skillType;
    public TargetType targetType;
    
    public static float calcElementAddition(ElementType cast, ElementType target){
        Dictionary <ElementType, Dictionary<ElementType, float> > elementMap = new Dictionary<ElementType, Dictionary<ElementType, float>>();
        elementMap[ElementType.Fire] = new Dictionary<ElementType, float>(){{ElementType.Water, 0.5F}, {ElementType.Wind, 1.5F}};
        elementMap[ElementType.Water] = new Dictionary<ElementType, float>(){{ElementType.Wind, 0.5F}, {ElementType.Fire, 1.5F}};
        elementMap[ElementType.Wind] = new Dictionary<ElementType, float>(){{ElementType.Fire, 0.5F}, {ElementType.Water, 1.5F}};
        elementMap[ElementType.Holy] = new Dictionary<ElementType, float>()
        {
            {ElementType.Fire, 0.9F},
            {ElementType.Wind, 0.9F},
            {ElementType.Water, 0.9F},
            {ElementType.Holy, 0.9F}, 
            {ElementType.Dark, 2.0F}
        };
        elementMap[ElementType.Dark] = new Dictionary<ElementType, float>()
        {
            {ElementType.Fire, 0.9F},
            {ElementType.Wind, 0.9F},
            {ElementType.Water, 0.9F},
            {ElementType.Dark, 0.9F}, 
            {ElementType.Holy, 2.0F}
        };
        
        var castDic = elementMap[cast];
        if( castDic.ContainsKey(target) )
            return castDic[target];
                
        return 1.0F;
    }        
}
