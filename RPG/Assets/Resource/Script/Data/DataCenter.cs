using UnityEngine;
using System;
using System.Collections.Generic;

// 隊伍資料
public class TeamInfo
{
    public string[] cNameList; // 隊員名稱
}

public class DataCenter : MonoBehaviour
{
    // 資料
    public CharacterGroup characterData;
    public CareerGroup careerData;
    public ItemGroup itemData;
    public SkillGroup skillData;
    public List<TeamInfo> teamInfo;
    public List<ItemData> equipList;
    public Dictionary<string, int> awakeItemList = new Dictionary<string, int>();
    public Dictionary<string, int> strengthItemList = new Dictionary<string, int>();

    // 常數
    public const int TEAM_NUM = 1;      // 可編成的隊伍數
    public const int MEMBER_NUM = 4;    // 每隊伍的成員數

    // 介面
    public TeamStatus teamStatus;
    public MemberList memberList;
    public EquipSelectList equipSelectList;
    public EquipStrengthItemPanel equipStrengthItemPanel;
    public EquipStrengthUseItemPanel strengthUseItemPanel;
    public EquipStrengthStatus equipStrengthStatus;

    public void init()
    {
        // 檔案初始化
        characterData.init();
        careerData.init();
        itemData.init();
        skillData.init();

        // 介面初始化
        memberList.init();
        teamStatus.init();
        equipSelectList.init();

        // 初始化隊伍
        teamInfo = new List<TeamInfo>();
        for (int i = 0; i < TEAM_NUM; i++)
        {
            TeamInfo tInfo = new TeamInfo();
            tInfo.cNameList = new string[MEMBER_NUM] { "", "", "", "" };
            teamInfo.Add(tInfo);
        }

        // 初始化裝備位置 
        equipList = new List<ItemData>();

        // 初始化覺醒道具
        awakeItemList = new Dictionary<string, int>();
        foreach (AwakeItem awakeItem in itemData.awakeGroup.list.Values)
            awakeItemList.Add(awakeItem.keyName, 0);

        // 初始化強化道具
        strengthItemList = new Dictionary<string, int>();
        foreach (StrengthItem strengthItem in itemData.strengthGroup.list.Values)
            strengthItemList.Add(strengthItem.keyName, 10);
        // strengthItemList.Add(strengthItem.keyName, 0);

        // 塞裝備（test)
        addEquipItem("BrozenSword");
        addEquipItem("WoodWand");
        addEquipItem("BrozenArmor");

        // 介面(test)
        // teamStatus.updateTeamPosition(0);
        // memberList.initSelect(0, -1);
        equipSelectList.init();
        equipSelectList.updateEquipList(0, ProcessType.None, EquipProcessSortType.CreateTime);
    }

    // 增加裝備道具
    public void addEquipItem(string itemName)
    {
        if (itemData.equipGroup.getEquipItem(itemName) != null)
        {
            var item = new ItemData();
            item.lv = 1;
            item.exp = 0;
            item.awake = false;
            item.equiper = String.Empty;
            item.equipSeq = 0;
            item.creatTime = DateTime.Now;
            item.equipName = itemName;
            equipList.Add(item);
        }
    }

    public bool patchStrengthItem(string keyName, int patch)
    {
        if( !strengthItemList.ContainsKey(keyName) )
            return false;
            
        if( strengthItemList[keyName] + patch > 0 ){
            strengthItemList[keyName] += patch;
            return true;
        }
        
        return false;                        
    }

    public int isHaveTeam(int teamIdx, string cName)
    {
        var tInfo = teamInfo[teamIdx];
        for (int i = 0; i < tInfo.cNameList.Length; i++)
        {
            if (cName == tInfo.cNameList[i])
                return i;
        }

        return -1;
    }
}