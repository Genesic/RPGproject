using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterStatus : MonoBehaviour {
    public Image headPic;
    public Text cName;
    public Text lv;
    public Text maxLv;
    public Text exp;
    public Text maxExp;
    public RectTransform maxExpBar;
    public RectTransform expBar;
    public Text hp;
    public Text mp;
    public Text atk;
    public Text def;
    public Text matk;
    public Text mdef;
    public Text spd;
    public Text[] skills;
    public CharacterEquipSelect[] equipItems;
    
    public void showCharacterInfo(CharacterInfo cInfo){
        // 角色資料
        headPic.sprite = cInfo.headPic;
        cName.text = cInfo.cName;
        lv.text = cInfo.lv.ToString();
        maxLv.text = cInfo.maxLv.ToString();
        exp.text = cInfo.exp.ToString();
        maxExp.text = cInfo.maxExp.ToString();
        hp.text = cInfo.hp.ToString();
        mp.text = cInfo.mp.ToString();
        atk.text = cInfo.atk.ToString();
        def.text = cInfo.def.ToString();
        matk.text = cInfo.matk.ToString();
        mdef.text = cInfo.mdef.ToString();
        spd.text = cInfo.spd.ToString();
        
        // 技能資料
        for(int i=0 ; i<cInfo.skills.Count ; i++){
            var skill = skills[i];
            skill.text = cInfo.skills[i];
        }
        
        // 裝備資料
        var character = MainData.dataCenter.characterData.list[cInfo.cName];
        var equipList = character.getEquipItem();
        for(int i=0 ; i<equipItems.Length ; i++){
            if( equipList.ContainsKey(i) ){
                var itemPos = equipList[i];
                var equip = MainData.dataCenter.itemData.equipGroup.getEquipItem(itemPos.equipName);
                equipItems[i].showEquipItem(equip.icon);
            } else {
                equipItems[i].showUnequipItem();
            }
        }
        
        // 經驗值條
        float ori_width = maxExpBar.rect.width;
        float ori_height = maxExpBar.rect.height;
        float percent = cInfo.exp / cInfo.maxExp;
        expBar.sizeDelta = new Vector2(ori_width*percent, ori_height);
    }
}
