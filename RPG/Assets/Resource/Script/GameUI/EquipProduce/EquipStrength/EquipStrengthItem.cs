using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipStrengthItem : MonoBehaviour
{
    public string keyName;
    public Image itemIcon;
    public Text itemNum;
    public Text rare;
    public Image canUse;
    public int oriNum = 0;
    public int useNum = 0;

    Color cantUseColor = new Color32(0, 0, 0, 150);

    public void init(StrengthItem sItem)
    {
        oriNum = MainData.dataCenter.strengthItemList[sItem.keyName];
        keyName = sItem.keyName;
        itemIcon.sprite = sItem.icon;
        itemIcon.color = sItem.iconColor;
        rare.text = ItemBase.getRareName(sItem.rareType);
        updateUseNum(0);
    }

    public bool updateUseNum(int patch)
    {
        if (oriNum - (useNum + patch) >= 0)
        {
            useNum += patch;
            itemNum.text = (oriNum - useNum).ToString();
            checkCanUse();
            return true;
        }
        
        return false;
    }

    // 檢查道具數量是否足夠所需顯示的顏色
    public void checkCanUse()
    {
        if (oriNum - useNum <= 0)
            canUse.color = cantUseColor;
        else
            canUse.color = Color.clear;
    }
}
