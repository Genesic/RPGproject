using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipStrengthUseItemPointCheck : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public EquipStrengthUseItemPanel strengthUseItemPanel;
    public EquipStrengthItemPanel strengthItemPanel;
    public EquipStrengthUseItem strengthUseItem;
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (strengthUseItemPanel.patchStrengthUseItem(strengthUseItem.keyName, -1))
        {
            strengthItemPanel.patchStrengthItem(strengthUseItem.keyName, 1);
        }
    }
}
