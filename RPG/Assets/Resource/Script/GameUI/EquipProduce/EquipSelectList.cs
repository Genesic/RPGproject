using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EquipSelectList : MonoBehaviour
{
    public Animator transAnime;
    public EquipSelectItem equipSelectItemPrefab;
    public Dictionary<int, EquipSelectItem> equipList;
    public const int NUM_PER_PAGE = 25;

    // Use this for initialization
    public void init()
    {
        equipList = new Dictionary<int, EquipSelectItem>();
        RectTransform oriRT = equipSelectItemPrefab.gameObject.GetComponent<RectTransform>();
        float ori_x = oriRT.anchoredPosition.x;
        float ori_y = oriRT.anchoredPosition.y;
        float width = oriRT.rect.width;
        float height = oriRT.rect.height;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int seq = i * 5 + j;
                var equipSelectItem = Instantiate(equipSelectItemPrefab) as EquipSelectItem;
                equipSelectItem.transform.SetParent(transform, false);
                equipSelectItem.seq = seq;
                // 計算位置
                RectTransform itemRT = equipSelectItem.gameObject.GetComponent<RectTransform>();
                itemRT.anchoredPosition = new Vector2(ori_x + (width+10) * j, ori_y - (height+10) * i);
                equipList.Add(seq, equipSelectItem);
                equipSelectItem.gameObject.SetActive(false);
            }
        }
    }

    public List<ItemData> getSortEquip(ProcessType processType, EquipProcessSortType sortType)
    {
        List<ItemData> sortList = new List<ItemData>();
        List<ItemData> sortRes = null;

        // 取出指定的種類
        if (processType == ProcessType.None)
        {   // None代表不分種類(就是使用全部)
            sortList = MainData.dataCenter.equipList;
        }
        else
        {
            foreach (ItemData itemData in MainData.dataCenter.equipList)
            {
                var equip = MainData.dataCenter.itemData.equipGroup.getEquipItem(itemData.equipName);
                if (equip.processType == processType)
                    sortList.Add(itemData);
            }
        }

        switch (sortType)
        {
            case EquipProcessSortType.CreateTime: // 入手時間排序
                sortRes = sortList.OrderByDescending(ItemData => ItemData.creatTime).ToList();
                break;
            case EquipProcessSortType.Lv: // 等級排序
                sortRes = sortList.OrderByDescending(ItemData => ItemData.lv).ThenBy(ItemData => ItemData.creatTime).ToList();
                break;
            case EquipProcessSortType.Rare: // 稀有度排序
                var orderMap = new Dictionary<RareType, int>(){
                    { RareType.Normal, 0},
                    { RareType.Rare, 1},
                    { RareType.SuperRare, 2}
                };
                var equipGroup = MainData.dataCenter.itemData.equipGroup;
                sortRes = sortList.OrderByDescending(ItemData => orderMap[equipGroup.getEquipItem(ItemData.equipName).rareType]).ThenBy(ItemData => ItemData.creatTime).ToList();
                break;
        }

        return sortRes;
    }
    
    public int oriPage;
    public ProcessType pType;
    public EquipProcessSortType sType;

    public void updateEquipList(int page, ProcessType processType, EquipProcessSortType sortType)
    {
        oriPage = page;
        pType = processType;
        sType = sortType;
        var sortList = getSortEquip(processType, sortType);
        for (int i = 0; i < NUM_PER_PAGE; i++)
        {
            int seq = page * NUM_PER_PAGE + i;
            var equipButton = equipList[i];
            if (sortList.Count <= seq)
            {
                equipButton.gameObject.SetActive(false);
            }
            else
            {
                equipButton.gameObject.SetActive(true);
                var item = sortList[seq];
                var equip = MainData.dataCenter.itemData.equipGroup.getEquipItem(item.equipName);
                equipButton.icon.sprite = equip.icon;
                equipButton.icon.color = equip.iconColor;
                equipButton.lv.text = "LV" + item.lv;
                equipButton.itemData = item;
            }
        }
    }
}
