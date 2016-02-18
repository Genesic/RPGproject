using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterEquipSelect : MonoBehaviour {
    public Image icon;
    public GameObject item;
    
    public void showEquipItem(Sprite pic){
        item.SetActive(true);
        icon.sprite = pic;
    }
    
    public void showUnequipItem(){
        item.SetActive(false);
    }
}
