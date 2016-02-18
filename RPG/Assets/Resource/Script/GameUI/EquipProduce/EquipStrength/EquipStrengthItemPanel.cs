using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EquipStrengthItemPanel : MonoBehaviour
{
    public EquipStrengthItem equipStrengthItemPrefab;
    public EquipStrengthUseItemPanel equipStrengthUseItemPanel;
    public Dictionary<string, EquipStrengthItem> list;

    public void init()
    {
        list = new Dictionary<string, EquipStrengthItem>();
        RectTransform oriRT = equipStrengthItemPrefab.gameObject.GetComponent<RectTransform>();
        float oriX = oriRT.anchoredPosition.x;
        float oriY = oriRT.anchoredPosition.y;
        float width = oriRT.rect.width;
        float height = oriRT.rect.height;

        // 產生強化道具prefab並且把值放進去
        int i = 0;
        foreach (ProcessType pType in System.Enum.GetValues(typeof(ProcessType)))
        {
            int j = 0;
            foreach (RareType rType in System.Enum.GetValues(typeof(RareType)))
            {
                var sItem = MainData.dataCenter.itemData.strengthGroup.getStrengthItem(pType, rType);
                if (sItem != null)
                {
                    //int itemNum = MainData.dataCenter.strengthItemList[sItem.keyName];
                    var equipStrengthItem = Instantiate(equipStrengthItemPrefab) as EquipStrengthItem;
                    equipStrengthItem.transform.SetParent(transform, false);
                    equipStrengthItem.init(sItem);
                    var pointChecker = equipStrengthItem.canUse.gameObject.GetComponent<EquipStrengthItemPointCheck>();
                    pointChecker.equipStrengthUseItemPanel = equipStrengthUseItemPanel;

                    // 計算位置
                    RectTransform itemRt = equipStrengthItem.gameObject.GetComponent<RectTransform>();                    
                    itemRt.anchoredPosition = new Vector2(oriX + (width+5)*j, oriY - (height+10)*i);

                    // 把產生出的prefab放進list內
                    list.Add(sItem.keyName, equipStrengthItem);
                    j++;
                }
            }
            // 如果這一排rareType有道具的話才換行
            if (j > 0)
                i++;
        }
    }
    
    public void patchStrengthItem(string keyName, int patch)
    {
        if( list.ContainsKey(keyName) )
        {
            var sItemIns = list[keyName];
            sItemIns.updateUseNum(-patch);
        }
    }
}
