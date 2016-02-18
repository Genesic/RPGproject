using UnityEngine;
using System.Collections;

public class SelectMember : MonoBehaviour
{
    public MemberList memberList;
    public Animator selectMemberSwitch;
    public GameObject teamStatus;
    int actHash = Animator.StringToHash("act");
    public void cancel()
    {
        selectMemberSwitch.SetTrigger(actHash);
    }
    
    public void confirm()
    {
        teamStatus.GetComponent<TeamStatus>().setSelectMember(memberList.selectName);
        selectMemberSwitch.SetTrigger(actHash);
    }

    public void startSwitchSelectMember() {
        teamStatus.GetComponent<TeamStatus>().updateTeamPosition(-1);
        teamStatus.SetActive(true);
        gameObject.SetActive(false);
    }
}
