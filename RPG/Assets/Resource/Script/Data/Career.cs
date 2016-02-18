using UnityEngine;
using System.Collections;

public struct GrowAbility{
    public int hp;
    public int mp2;
    public int atk;
    public int def;
    public int matk;
    public int mdef;    
}
public class Career : MonoBehaviour {
    public CharacterType characterType;
    public int[] addHp;
    public int[] addMp2;
    public int[] addAtk;
    public int[] addDef;
    public int[] addMatk;
    public int[] addMdef;

    public GrowAbility getGrowAbility (int lv){    
        var ability = new GrowAbility();        
        for(int i=0 ; i<lv ; i++){
            if( addHp.Length > 0 )      
                ability.hp += addHp[i / addHp.Length];
            //ability.mp2 = addMp2[i / addMp2.Length];
            ability.mp2 = 0;
            if( addAtk.Length > 0)
                ability.atk = addAtk[i / addAtk.Length];
            if( addDef.Length > 0)
                ability.def = addDef[i/ addDef.Length];
            if( addMatk.Length > 0)
                ability.matk = addMatk[i / addMatk.Length];
            if( addMdef.Length > 0)
                ability.mdef = addMdef[i / addMdef.Length];                    
        }                              
        return ability;
    }    
}