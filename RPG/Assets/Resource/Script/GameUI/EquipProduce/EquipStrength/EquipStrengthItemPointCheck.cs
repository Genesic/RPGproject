using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipStrengthItemPointCheck : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public EquipStrengthUseItemPanel equipStrengthUseItemPanel;
    public EquipStrengthItem equipStrengthItem;
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (equipStrengthItem.updateUseNum(1))
            equipStrengthUseItemPanel.patchStrengthUseItem(equipStrengthItem.keyName, 1);
    }
}
