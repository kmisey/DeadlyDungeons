using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemDescription : MonoBehaviour {

    public Text itemName;
    public Text itemDesc;
    public GameObject button;
    public static ItemDescription Instance { get; set; } 

    private void Awake() {
        Instance = this;
        resetText();
    }

	public void updateDescription(BaseItem item, string desc) {
        itemName.text = item.title;
        itemDesc.text = desc;
        if(button != null) {
            if (item.GetType().Equals(typeof(TradeItem)) || item.GetType().Equals(typeof(CraftItem))) {
                button.SetActive(false);
            }
            else {
                button.SetActive(true);
            }
        }
    }

    public void resetText() {
        itemName.text = "";
        itemDesc.text = "Press an item to view its stats.";
        if(button != null) {
            button.SetActive(false);
        }
    }
}
