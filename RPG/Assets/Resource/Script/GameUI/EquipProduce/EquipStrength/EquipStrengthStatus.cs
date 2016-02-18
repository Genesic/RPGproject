using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EquipStrengthStatus : MonoBehaviour
{
    public EquipStrengthUseItemPanel equipStrengthUseItemPanel;
    public EquipStrengthItemPanel equipStrengthItemPanel;
    public Image icon;
    public Text itemName;
    public Text itemLv;
    public Text itemAddLv;
    public Text exp;
    public Text expMax;
    public Text hp;
    public Text hpAdd;
    public Text atk;
    public Text atkAdd;
    public Text def;
    public Text defAdd;
    public Text spd;
    public Text spdAdd;
    public Text matk;
    public Text matkAdd;
    public Text mdef;
    public Text mdefAdd;
    public RectTransform expBar;
    public RectTransform expMaxBar;
    public Text spAbility;
    public Text awakeAbility;

    private ItemData itemData;
    public string keyName;
    public ProcessType pType;

    public void init(ItemData data)
    {
        oriWidth = expMaxBar.rect.width;
        oriHeight = expMaxBar.rect.height;
        itemData = data;
        
        keyName = itemData.equipName;
        pType = MainData.dataCenter.itemData.equipGroup.list[keyName].processType;
        updateEquipData(0);
    }
    
    // 進行強化
    public void equipStrength()
    {
        int exp = equipStrengthUseItemPanel.getSItemExp();
        var sItemNum = equipStrengthUseItemPanel.getSItemNum();
        
        // 扣除道具
        foreach(KeyValuePair <string, int> sItem in sItemNum )
        {
            if( !MainData.dataCenter.patchStrengthItem(sItem.Key, sItem.Value ) )
                Debug.Log("patch "+sItem.Key+" ("+sItem.Value+" failed!!");
        }
        
        // 清空使用的強化道具
        equipStrengthUseItemPanel.clearUseSItem();
        
        // 把exp加進道具中
        itemData.patchExp(exp);
        
        // 重新更新介面
        updateEquipData(0);
        equipStrengthUseItemPanel.updateStrengthUsesItem();
    }
    
    // 回去選擇裝備頁
    public void backToEquipSelect()
    {
         int closeHash = Animator.StringToHash("close");
         int page = MainData.dataCenter.equipSelectList.oriPage;
         ProcessType pType = MainData.dataCenter.equipSelectList.pType;
         EquipProcessSortType sType = MainData.dataCenter.equipSelectList.sType;
         // 更新裝備選擇畫面的資料
         MainData.dataCenter.equipSelectList.updateEquipList(page, pType, sType);
         MainData.dataCenter.equipSelectList.transAnime.SetTrigger(closeHash);
    }

    // 預先顯示使用的強化道具強化後的資料
    public void updateEquipData(int patchExp)
    {
        // 還沒初始化前就直接return
        if (itemData == null)
            return;

        var oriEquip = itemData.getEquipData();
        itemName.text = oriEquip.showName;

        // 計算exp顯示
        int oriLv = oriEquip.lv;
        int newLv = oriLv;
        int newExp = oriEquip.exp + patchExp;
        int newExpMax = oriEquip.expMax;
        while (newExp >= newExpMax)
        {
            newLv += 1;
            newExp = newExp - newExpMax;
            newExpMax = ItemData.getMaxExp(newLv);
        }

        // 取得增加exp後的itemData
        var newItem = itemData.getItemDataClone();
        newItem.lv = newLv;
        newItem.exp = newExp;
        var newEquip = newItem.getEquipData();

        // icon
        icon.sprite = newEquip.icon;
        icon.color = newEquip.iconColor;

        // exp
        exp.text = newExp.ToString();
        expMax.text = newExpMax.ToString();

        // exp bar
        // 如果有升級，經驗條動畫從0開始
        initExpAnime(newExp, newExpMax);

        // lv hp atk def spd matk mdef
        Text[] column = new Text[] { itemLv, hp, atk, def, spd, matk, mdef };
        string[] whiteValue = new string[] {
            newLv.ToString(),
            newEquip.hp.ToString(),
            newEquip.atk.ToString(),
            newEquip.def.ToString(),
            newEquip.spd.ToString(),
            newEquip.matk.ToString(),
            newEquip.mdef.ToString()
        };
        Text[] addColumn = new Text[] { itemAddLv, hpAdd, atkAdd, defAdd, spdAdd, matkAdd, mdefAdd };
        string[] greenValue = new string[] {
            (newLv - oriLv).ToString(),
            (newEquip.hp - oriEquip.hp).ToString(),
            (newEquip.atk - oriEquip.atk).ToString(),
            (newEquip.def - oriEquip.def).ToString(),
            (newEquip.spd - oriEquip.spd).ToString(),
            (newEquip.matk - oriEquip.matk).ToString(),
            (newEquip.mdef - newEquip.mdef).ToString()
        };
        for (int i = 0; i < column.Length; i++)
        {
            column[i].text = whiteValue[i];
            if (int.Parse(greenValue[i]) > 0)
                addColumn[i].text = "(+" + (greenValue[i]) + ")";
            else
                addColumn[i].text = "";
        }
    }

    // 製作exp條前進動畫
    float oriWidth;
    float oriHeight;
    int nowExp;
    int targetExp;
    int newExpMax;
    public void initExpAnime( int tExp, int nExpMax)
    {
        nowExp = 0;
        targetExp = tExp;
        newExpMax = nExpMax;
    }
    void Update()
    {
        float percent = (float)nowExp / (float)newExpMax;
        expBar.sizeDelta = new Vector2(oriWidth * percent, oriHeight);
        if (nowExp < targetExp)
            nowExp += targetExp / 20;
    }
}
