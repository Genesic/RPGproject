using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeamStatus : MonoBehaviour
{
    public List<TeamPosition> teamPosition;

    //  轉換隊伍狀態去選擇角色狀態的動畫 
    public Animator switchCharacterStatus;
    public GameObject selectMember;
    //  選的隊伍位置
    public int selectLoc;
    public int selectTeam;

    int actHash = Animator.StringToHash("act");

    public void init()
    {
        teamPosition = new List<TeamPosition>();
        int location = 0;
        foreach (Transform child in transform)
        {
            var position = child.gameObject.GetComponent<TeamPosition>();
            if (position != null)
            {
                teamPosition.Add(position);

                // 幫選擇位置的按鈕連接到clickTeamPos
                int tmp = location;
                Button button1 = child.gameObject.GetComponent<Button>();
                Button button2 = position.haveMember.GetComponent<Button>();
                button1.onClick.AddListener(() => { clickTeamPos(tmp); });
                button2.onClick.AddListener(() => { clickTeamPos(tmp); });
                location++;
            }
        }

        // switchCharacterStatus = gameObject.GetComponent<Animator>();

    }

    public void updateTeamPosition(int teamIdx)
    {
        // 判斷要不要保留selectTeam
        if (teamIdx >= 0)
            selectTeam = teamIdx;

        // 更新每個位置的資料
        var teamInfo = MainData.dataCenter.teamInfo[selectTeam];
        for (int i = 0; i < teamPosition.Count && i < teamInfo.cNameList.Length; i++)
        {
            string cName = teamInfo.cNameList[i];
            if (MainData.dataCenter.characterData.list.ContainsKey(cName))
            {
                var character = MainData.dataCenter.characterData.list[cName];
                var cInfo = character.getCharacter();
                teamPosition[i].setPosition(cInfo);
            }
            else
            {
                teamPosition[i].clearPosition();
            }
        }
    }

    // 點擊隊伍成員時播放轉換動畫和紀錄點擊位置
    public void clickTeamPos(int pos)
    {
        selectLoc = pos;
        Debug.Log(switchCharacterStatus);
        switchCharacterStatus.SetTrigger(actHash);
    }

    // 動畫播放完後轉入選擇成員頁面
    public void startSwitchCharacterStatus()
    {
        var teamInfo = MainData.dataCenter.teamInfo[selectTeam];
        string cName = teamInfo.cNameList[selectLoc];
        if (MainData.dataCenter.characterData.list.ContainsKey(cName))
        {
            // 有選擇的角色時取得目前選擇的角色
            var character = MainData.dataCenter.characterData.list[cName];
            int page = character.seq / MemberList.PAGE_PER_MEMBER;
            int pos = character.seq % MemberList.PAGE_PER_MEMBER;
            MainData.dataCenter.memberList.initSelect(page, pos);
        }
        else
        {
            // 沒選擇的角色時取得可以作為預設選擇的角色(不在隊伍中)
            List<int> seqs = MainData.dataCenter.characterData.getCharacterSeqs();
            foreach (int seq in seqs)
            {
                var character = MainData.dataCenter.characterData.getCharacterBySeq(seq);
                if (character != null)
                {
                    int isHaveTeam = MainData.dataCenter.isHaveTeam(selectTeam, character.characterName);
                    if (isHaveTeam < 0)
                    {
                        int page = seq / MemberList.PAGE_PER_MEMBER;
                        int pos = seq % MemberList.PAGE_PER_MEMBER;
                        MainData.dataCenter.memberList.initSelect(page, pos);
                        break;
                    }
                }
            }
        }

        // 畫面轉換過去關閉這個頁面的顯示然後開啟選角色的顯示
        selectMember.SetActive(true);
        gameObject.SetActive(false);
    }

    public void setSelectMember(string cName)
    {
        var teamInfo = MainData.dataCenter.teamInfo[selectTeam];
        teamInfo.cNameList[selectLoc] = cName;
    }
}
