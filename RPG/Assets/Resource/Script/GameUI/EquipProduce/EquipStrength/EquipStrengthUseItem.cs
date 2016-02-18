using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipStrengthUseItem : MonoBehaviour {
    public Image icon;
    public Text itemNum;
    public Animator expBonus;
    public string keyName;
    public GameObject pointCheck;
    
    public void init(string itemKeyName, int num, ProcessType equipProcessType)
    {
        keyName = itemKeyName;
        // Debug.Log(keyName);
        if( MainData.dataCenter.itemData.strengthGroup.list.ContainsKey(keyName) )
        {
            var sItem = MainData.dataCenter.itemData.strengthGroup.list[keyName];
            icon.sprite = sItem.icon;
            icon.color = sItem.iconColor;
            itemNum.text = num.ToString();
            if( equipProcessType == sItem.strengthType )
                expBonus.gameObject.SetActive(true);
        }        
    }
}
