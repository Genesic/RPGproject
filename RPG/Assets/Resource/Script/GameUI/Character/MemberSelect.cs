using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemberSelect : MonoBehaviour
{
    public GameObject haveCharacter;
    public GameObject haveMember;
    public Image head;
    public Image backColor;
    public Button choose;
    public string setName;
    public Text alreadyTeam;
    Color chooseColor = new Color32(255, 187, 0, 255);
    Color notChooseColor = new Color32(50, 50, 50, 255);
    public void updatePosition(string cName, bool selectPos, int teamPos)
    {
        setName = cName;
        // 檢查有沒有這個角色
        if (!MainData.dataCenter.characterData.list.ContainsKey(cName))
        {
            haveCharacter.SetActive(false);
            return;
        } else {            
            haveCharacter.SetActive(true);
        }

        // 是否為目前選取的位置
        if (selectPos)
            backColor.color = chooseColor;            
        else
            backColor.color = notChooseColor;

        // 處理頭像顯示
        var character = MainData.dataCenter.characterData.list[cName];
        if (character.open)
        {          
            haveMember.SetActive(true);
            head.sprite = character.headPic;
        } else {
            haveMember.SetActive(false);
        }
        
        // 處理是否有隊伍的顯示 (teamPos = 在隊伍中的位置, -1=沒有隊伍)
        if( teamPos >= 0 ){
            alreadyTeam.gameObject.SetActive(true);
            alreadyTeam.text = (teamPos+1).ToString();
            head.color = new Color32(255,255,255,100);
        } else{
            alreadyTeam.gameObject.SetActive(false);
            head.color = new Color32(255,255,255,255);
        }
    }     
}
