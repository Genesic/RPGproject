using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrengthGroup : MonoBehaviour
{
    public Dictionary<string, StrengthItem> list;

    public void init()
    {
        list = new Dictionary<string, StrengthItem>();

        foreach (Transform child in transform)
        {
            var data = child.gameObject.GetComponent<StrengthItem>();
            // 如果keyName是空的話, 用物件名稱代替
            if (data.keyName == string.Empty)
                data.keyName = child.name;
            if (list.ContainsKey(data.keyName))
                Debug.Log("StrengthItem duplicate:" + data.keyName);

            list.Add(data.keyName, data);
        }
    }

    public StrengthItem getStrengthItem(ProcessType pType, RareType rType)
    {
        foreach (StrengthItem sItem in list.Values)
        {
            // Debug.Log("pType:"+pType+" sItemType:"+sItem.strengthType+" rType:"+rType+" sItemType:"+sItem.rareType );
            if (sItem.strengthType == pType && sItem.rareType == rType)
                return sItem;
        }

        return null;
    }
}
