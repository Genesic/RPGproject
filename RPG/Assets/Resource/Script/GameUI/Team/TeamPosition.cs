using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamPosition : MonoBehaviour {
    public Image headPic;
    public Text cName;
    public Text lv;
    public Text hp;
    public Text atk;
    public Text def;
    public Text spd;
    public Text matk;
    public Text mdef;
    // 有玩家時顯示的物件
    public GameObject haveMember;
    // 沒玩家時顯示的物件
    public Image posColor;
    
    // 有玩家時顯示的背景顏色
    Color haveMemberColor = new Color32(255, 255, 255, 255);
    // 沒玩家時顯示的背景顏色
    Color noMemberColor = new Color32(50,50,50,255);
    
    public void clearPosition(){
        haveMember.SetActive(false);
        posColor.color = noMemberColor;     
    }
    
    public void setPosition(CharacterInfo cInfo){
        // 背景處理
        haveMember.SetActive(true);
        posColor.color = haveMemberColor;
        
        // 角色資料顯示
        cName.text = cInfo.cName;
        headPic.sprite = cInfo.headPic;
        lv.text = cInfo.lv.ToString();
        hp.text = cInfo.hp.ToString();
        atk.text = cInfo.atk.ToString();
        def.text = cInfo.def.ToString();
        spd.text = cInfo.spd.ToString();
        matk.text = cInfo.matk.ToString();
        mdef.text = cInfo.mdef.ToString();        
    }
}
