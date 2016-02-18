using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipStrengthUseItemPanel : MonoBehaviour
{
    public EquipStrengthStatus equipStrengthStatus;
    public EquipStrengthItemPanel strengthItemPanel;
    public EquipStrengthUseItem strengthUseItemPrefab;
    public List<EquipStrengthUseItem> strengthUseItemList;
    public GameObject confirmButton;
    public List<string> sItemList;
    public Dictionary<string, int> sItemNum;    
    public void init()
    {                
        int listNum = MainData.dataCenter.strengthItemList.Values.Count;
        sItemList = new List<string>();
        sItemNum = new Dictionary<string, int>();
        strengthUseItemList = new List<EquipStrengthUseItem>();
        RectTransform oriRT = strengthUseItemPrefab.gameObject.GetComponent<RectTransform>();
        float oriX = oriRT.anchoredPosition.x;
        float oriY = oriRT.anchoredPosition.y;
        //float width = oriRT.rect.width;
        float height = oriRT.rect.height;

        // 產生prefab並且把值放進去
        for (int i = 0; i < listNum; i++)
        {
            // 產生instance
            var strengthUseItem = Instantiate(strengthUseItemPrefab) as EquipStrengthUseItem;
            strengthUseItem.transform.SetParent(transform, false);
            var pointCheck = strengthUseItem.pointCheck.GetComponent<EquipStrengthUseItemPointCheck>();
            pointCheck.strengthItemPanel = strengthItemPanel;
            pointCheck.strengthUseItemPanel = this;

            // 計算位置
            RectTransform itemRt = strengthUseItem.gameObject.GetComponent<RectTransform>();
            itemRt.anchoredPosition = new Vector2(oriX, oriY - (height + 10) * i);

            // 把產生出的instance放進prefab
            strengthUseItemList.Add(strengthUseItem);
        }

        updateStrengthUsesItem();
    }

    public bool patchStrengthUseItem(string keyName, int patch)
    {
        // 先找找看sItemList是不是已經有這個item, 有的話直接更新
        for (int i = 0; i < sItemList.Count; i++)
            if (sItemList[i].CompareTo(keyName) == 0)
            {
                // 取得要強化的裝備的裝備種類
                ProcessType pType =  MainData.dataCenter.equipStrengthStatus.pType;
                strengthUseItemList[i].init(keyName, patch, pType);
                if (sItemNum[keyName] + patch >= 0)
                {
                    sItemNum[keyName] += patch;
                    updateStrengthUsesItem();
                    return true;
                }
            }

        // 沒有的話就塞
        if (patch > 0)
        {
            sItemList.Add(keyName);
            sItemNum.Add(keyName, patch);
            updateStrengthUsesItem();
            return true;
        }

        return false;
    }

    // 取得目前選擇強化道具可以獲得多少經驗值
    public int getSItemExp()
    {
        int exp = 0;
        for (int i = 0; i < sItemList.Count; i++)
        {
            string keyName = sItemList[i];
            int num = sItemNum[keyName];
            exp += MainData.dataCenter.itemData.strengthGroup.list[keyName].exp * num;
        }
        
        return exp;        
    }
    
    public Dictionary<string, int> getSItemNum()
    {
        return sItemNum;
    }
    
    // 選擇強化道具後的更新
    public void updateStrengthUsesItem()
    {   // 先清除數量為0的道具
        foreach (KeyValuePair<string, int> sItem in sItemNum)
            if (sItem.Value <= 0)
            {
                sItemList.Remove(sItem.Key);
                sItemNum.Remove(sItem.Key);
                break;
            }

        // 更新顯示列表
        for (int i = 0; i < sItemList.Count; i++)
        {
            string keyName = sItemList[i];
            int num = sItemNum[keyName];
            // 取得要強化的裝備的裝備種類
            ProcessType pType =  MainData.dataCenter.equipStrengthStatus.pType;            
            strengthUseItemList[i].init(keyName, num, pType);
            strengthUseItemList[i].gameObject.SetActive(true);
        }        

        // 關閉剩餘的顯示列表
        for (int j = sItemList.Count; j < strengthUseItemList.Count; j++)
            strengthUseItemList[j].gameObject.SetActive(false);

        // 更新道具資料
        int exp = getSItemExp();
        equipStrengthStatus.updateEquipData(exp);
        confirmButton.SetActive((exp > 0) ? false : true);
    }
    
    public void clearUseSItem()
    {
        sItemList.Clear();
        sItemNum.Clear();
    }
}
