using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AwakeGroup : MonoBehaviour
{
    public Dictionary<string, AwakeItem> list;

    public void init()
    {
        list = new Dictionary<string, AwakeItem>();

        foreach (Transform child in transform)
        {
            var data = child.gameObject.GetComponent<AwakeItem>();
            // 如果keyName是空的話, 用物件名稱代替
            if (data.keyName == string.Empty)
                data.keyName = child.name;
                
            if (list.ContainsKey(data.keyName))
                Debug.Log("AwakeItem duplicate:" + data.keyName);

            list.Add(data.keyName, data);
        }
    }
}
