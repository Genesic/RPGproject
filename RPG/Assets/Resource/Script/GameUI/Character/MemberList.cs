using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MemberList : MonoBehaviour
{
    public List<MemberSelect> memberPosition;
    public const int PAGE_PER_MEMBER = 8;
    public int selectPos = 0;
    public int selectPage = 0;
    public string selectName;
    public CharacterStatus characterStatus;

    public void init()
    {
        memberPosition = new List<MemberSelect>();

        foreach (Transform child in transform)
        {
            var member = child.gameObject.GetComponent<MemberSelect>();
            if( child.gameObject.activeSelf )            
                memberPosition.Add(member);
        }
    }

    // 更新選擇角色列表
    void updateMember(int choose)
    {
        if( choose >= 0)
            selectPos = choose;
            
        // 更新角色列表
        for (int pos = 0; pos < PAGE_PER_MEMBER && pos < memberPosition.Count; pos++)
        {
            int seq = pos + selectPage * PAGE_PER_MEMBER;
            var character = MainData.dataCenter.characterData.getCharacterBySeq(seq);
            var cName = (character != null) ? character.characterName : string.Empty;
            bool isSelect = (pos == selectPos) ? true : false;
            
            // 取得這個角色是否已在隊伍中
            int teamIdx = MainData.dataCenter.teamStatus.selectTeam;
            int isHaveTeam = MainData.dataCenter.isHaveTeam(teamIdx, cName);
            
            // 更新角色列表顯示的狀態
            memberPosition[pos].updatePosition(cName, isSelect, isHaveTeam);
        }
        
        // 更新角色狀態視窗
        string chName = memberPosition[selectPos].setName;
        var cInfo = MainData.dataCenter.characterData.list[chName].getCharacter();
        selectName = chName;
        characterStatus.showCharacterInfo(cInfo);
    }
    
    // 取得可以當作預設選項的角色(尚未在隊伍中)
    public int getAvailableMemberList(int teamIdx)
    {
        for(int pos = 0 ; pos < PAGE_PER_MEMBER ; pos++)
        {
            int seq = pos+selectPage*PAGE_PER_MEMBER;
            var character = MainData.dataCenter.characterData.getCharacterBySeq(seq);
            if( character != null)
            {
                int isHaveTeam = MainData.dataCenter.isHaveTeam(teamIdx, character.characterName);
                if( isHaveTeam >= 0)
                    return pos;
            }
        }
        
        return 0;
    }

    public void initSelect(int page, int choose)
    {
        selectPage = page;
        updateMember(choose);
        for (int pos = 0; pos < memberPosition.Count; pos++)
        {
            int tmp = pos;
            Button button = memberPosition[pos].choose;
            button.onClick.AddListener(() => { selectMember(tmp); });
        }
    }

    // 選擇角色
    public void selectMember(int choose)
    {        
        updateMember(choose);
    }
}