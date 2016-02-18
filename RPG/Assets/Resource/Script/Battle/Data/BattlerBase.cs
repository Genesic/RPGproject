using UnityEngine;
using System.Collections;

public struct BattlerData{
    public int id;
    public string chName;
    public int hp;
    public int maxHp;
    public int mxMp;
    public int mp2;
    public int spd;
    public float maxAct;
    
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    public SkillBase[] skillGroup;
    public Sprite headPic;
}

public class BattlerBase : MonoBehaviour {
    BattlerData battleInfo;
    public float act;
    public int useSkillIdx;
    public BattleGroup enemyGroup;

}
