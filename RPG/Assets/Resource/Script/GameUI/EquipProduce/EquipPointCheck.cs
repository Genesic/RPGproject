using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipPointCheck : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler {
    int openHash = Animator.StringToHash("open");    
    private Vector2 pointDown;
	public void OnPointerDown(PointerEventData eventData){
        eventData.pointerEnter
		pointDown = eventData.position;
	}

	public void OnPointerUp(PointerEventData eventData){
		float diff = eventData.position.x - pointDown.x;
        //Debug.Log(diff);
		if (diff > 20F) {
			Debug.Log (" ->");
		} else if (diff < -20F) {
			Debug.Log (" <-");
		} else {
            // string keyName = gameObject.GetComponent<EquipStrengthItem>().keyName;
            // equipStrengthUseItemPanel.patchStrengthUseItem(keyName, 1);
            var equipSelectItem = gameObject.GetComponent<EquipSelectItem>();
            Debug.Log("click me!!"+equipSelectItem);
            if( equipSelectItem != null){
                // 初始化強化道具選項                
                MainData.dataCenter.equipStrengthItemPanel.init();
                // 初始化選擇的強化道具選項
                MainData.dataCenter.strengthUseItemPanel.init();
                // 初始化強化裝備的狀態
                MainData.dataCenter.equipStrengthStatus.init(equipSelectItem.itemData);
                MainData.dataCenter.equipSelectList.transAnime.SetTrigger(openHash);
            }            
        }
	}
	
	public void OnPointerClick(PointerEventData eventData){
        // Debug.Log("its me:"+name);
	}
}
